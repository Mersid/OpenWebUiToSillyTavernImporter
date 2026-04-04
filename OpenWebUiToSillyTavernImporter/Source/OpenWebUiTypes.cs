namespace OpenWebUiToSillyTavernImporter;

public class ChatExportRoot
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public Chat Chat { get; set; }
    public long UpdatedAt { get; set; }
    public long CreatedAt { get; set; }
    public object ShareId { get; set; }
    public bool Archived { get; set; }
    public bool Pinned { get; set; }
    public Meta Meta { get; set; }
    public object FolderId { get; set; }
}

public class Chat
{
    /// <summary>
    /// Technically a UUID, but apparently sometimes it can be empty!
    /// </summary>
    public string Id { get; set; }
    public string Title { get; set; }
    public string[] Models { get; set; }
    public Meta Params { get; set; }
    public History History { get; set; }
    public Message[] Messages { get; set; }
    public long Timestamp { get; set; }
}

public class History
{
    public Dictionary<string, Message> Messages { get; set; }
    public Guid CurrentId { get; set; }
}

public class Message
{
    public Guid? ParentId { get; set; }
    public Guid Id { get; set; }
    public Guid[] ChildrenIds { get; set; }
    public Role Role { get; set; }
    public string Content { get; set; }
    public string Model { get; set; }
    public string ModelName { get; set; }
    public long? ModelIdx { get; set; }
    public long Timestamp { get; set; }
    public Usage Usage { get; set; }
    public Output[] Output { get; set; }
    public bool? Done { get; set; }
    public string[] FollowUps { get; set; }
    public string[] Models { get; set; }
}

public class Output
{
    public string Type { get; set; }
    public string Id { get; set; }
    public string Status { get; set; }
    public Role Role { get; set; }
    public Content[] Content { get; set; }
}

public class Content
{
    public string Type { get; set; }
    public string Text { get; set; }
}

public class Usage
{
    public long PromptTokens { get; set; }
    public long CompletionTokens { get; set; }
    public long TotalTokens { get; set; }
    public double Cost { get; set; }
    public bool IsByok { get; set; }
    public PromptTokensDetails PromptTokensDetails { get; set; }
    public CostDetails CostDetails { get; set; }
    public CompletionTokensDetails CompletionTokensDetails { get; set; }
}

public class CompletionTokensDetails
{
    public long ReasoningTokens { get; set; }
    public long ImageTokens { get; set; }
    public long AudioTokens { get; set; }
}

public class CostDetails
{
    public double UpstreamInferenceCost { get; set; }
    public double UpstreamInferencePromptCost { get; set; }
    public double UpstreamInferenceCompletionsCost { get; set; }
}

public class PromptTokensDetails
{
    public long CachedTokens { get; set; }
    public long CacheWriteTokens { get; set; }
    public long AudioTokens { get; set; }
    public long VideoTokens { get; set; }
}

public class Meta
{
}

public enum Role { Assistant, User };