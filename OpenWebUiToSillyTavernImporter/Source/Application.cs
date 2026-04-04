using System.Text;
using System.Text.Json;

namespace OpenWebUiToSillyTavernImporter;

public class Application(RootCliCommand options)
{
    private RootCliCommand Options { get; } = options;

    public void Run()
    {
        string data = File.ReadAllText(Options.InputFile.FullName);

        List<ChatExportRoot>? chats = JsonSerializer.Deserialize(data, ImporterJsonContext.Default.ListChatExportRoot);
        List<ChatMessage> sillyTavernMessages = ExportToSillyTavernJsonl(chats[0]);
        string jsonl = GenerateSillyTavernJsonl(sillyTavernMessages);

        if (Options.OutputFile is not null)
        {
            File.WriteAllText(Options.OutputFile.FullName, jsonl);
        }
        else
        {
            Console.WriteLine(jsonl);
        }
    }

    /// <summary>
    /// Returns the path of the main chat, starting top-down.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    private List<Guid> MainChatPath(ChatExportRoot root)
    {
        History history = root.Chat.History;

        List<Guid> mainChatPath = [];
        Guid currentId = history.CurrentId;

        while (true)
        {
            mainChatPath.Add(currentId);

            Guid? parentId = history.Messages[currentId.ToString()].ParentId;
            if (parentId == null)
            {
                mainChatPath.Reverse();
                return mainChatPath;
            }

            currentId = parentId.Value;
        }
    }

    private List<ChatMessage> ExportToSillyTavernJsonl(ChatExportRoot root)
    {
        List<Guid> mainChatPath = MainChatPath(root);
        History history = root.Chat.History;
        List<Message> messages = mainChatPath.Select(t => history.Messages[t.ToString()]).ToList();

        List<ChatMessage> sillyTavernMessages = [];

        string agentName = Options.AgentName;
        string userName = Options.UserName;
        string apiName = Options.ApiName;

        foreach (Message message in messages)
        {
            string sendDate = DateTimeOffset.FromUnixTimeSeconds(message.Timestamp).UtcDateTime.ToString("o");
            ChatMessage stm = new ChatMessage
            {
                Name = message.Role == Role.Assistant ? agentName : userName,
                IsUser = message.Role == Role.User,
                IsSystem = false,
                SendDate = sendDate,
                Mes = message.Content,
                Extra = message.Role == Role.Assistant
                    ? new ChatMessageExtra
                    {
                        Api = apiName,
                        Model = root.Chat.Models[0],
                        Reasoning = "",
                        // The following doesn't exist on the Open WebUI side.
                        ReasoningDuration = null,
                        ReasoningSignature = null,
                        TimeToFirstToken = 0
                    }
                    : new ChatMessageExtra
                    {
                        IsSmallSys = false,
                        Reasoning = ""
                    }
            };

            if (message.Role == Role.Assistant)
            {
                stm.Title = "";
                stm.GenStarted = sendDate;
                stm.GenFinished = sendDate;
                stm.SwipeId = 0;
                stm.Swipes = [message.Content];
                stm.SwipeInfo =
                [
                    new SwipeInfo
                    {
                        SendDate = sendDate,
                        GenStarted = sendDate,
                        GenFinished = sendDate,
                        Extra = stm.Extra
                    }
                ];
            }

            sillyTavernMessages.Add(stm);
        }

        return sillyTavernMessages;
    }

    private string GenerateSillyTavernJsonl(List<ChatMessage> chatMessages)
    {
        StringBuilder sb = new StringBuilder();

        // SillyTavern can generate the rest.
        ChatMetadataLine cm = new ChatMetadataLine
        {
            ChatMetadata = new ChatMetadata
            {
                Integrity = "", // This is normally a UUID, but we don't need it to be; it will regenerate when the user sends a message.
                NotePrompt = "",
            },
            CharacterName = "unused",
            UserName = "unused"
        };

        sb.AppendLine(JsonSerializer.Serialize(cm, ExporterJsonContext.Default.ChatMetadataLine));

        foreach (ChatMessage chatMessage in chatMessages)
        {
            string s = JsonSerializer.Serialize(chatMessage, ExporterJsonContext.Default.ChatMessage);
            sb.AppendLine(s);
        }

        return sb.ToString();
    }
}