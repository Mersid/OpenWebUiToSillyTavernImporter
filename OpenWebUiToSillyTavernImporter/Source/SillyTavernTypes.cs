using System.Text.Json.Serialization;

namespace OpenWebUiToSillyTavernImporter;


public sealed class ChatMetadataLine
{
    public ChatMetadata? ChatMetadata { get; set; }
    public string? UserName { get; set; }
    public string? CharacterName { get; set; }
}

public sealed class ChatMetadata
{
    public string? Integrity { get; set; }
    public string? NotePrompt { get; set; }
    public int? NoteInterval { get; set; }
    public int? NotePosition { get; set; }
    public int? NoteDepth { get; set; }
    public int? NoteRole { get; set; }
    public TimedWorldInfo? TimedWorldInfo { get; set; }
    public bool? Tainted { get; set; }
    public int? LastInContextMessageId { get; set; }
    public List<object>? Attachments { get; set; }
    public STMemoryBooks? STMemoryBooks { get; set; }
}

/// <summary>
/// Both appear to be empty.
/// </summary>
public sealed class TimedWorldInfo
{
    public Dictionary<string, object>? Sticky { get; set; }
    public Dictionary<string, object>? Cooldown { get; set; }
}

public sealed class STMemoryBooks
{
    public int? SceneStart { get; set; }
    public int? SceneEnd { get; set; }
}

public sealed class ChatMessage
{
    public string? Name { get; set; }
    public bool? IsUser { get; set; }
    public bool? IsSystem { get; set; }
    public string? SendDate { get; set; }
    public string? Mes { get; set; }
    public string? Title { get; set; }
    public string? ForceAvatar { get; set; }

    public string? GenStarted { get; set; }
    public string? GenFinished { get; set; }

    public ChatMessageExtra? Extra { get; set; }

    public List<string>? Swipes { get; set; }
    public int? SwipeId { get; set; }
    public List<SwipeInfo>? SwipeInfo { get; set; }
}

public sealed class ChatMessageExtra
{
    // Common to both user and AI messages
    public string? Api { get; set; }
    public string? Model { get; set; }
    public string? Reasoning { get; set; }
    public double? ReasoningDuration { get; set; }
    public string? ReasoningSignature { get; set; }
    public double? TimeToFirstToken { get; set; }

    // User-message specific
    public bool? IsSmallSys { get; set; }
}

public sealed class SwipeInfo
{
    public string? SendDate { get; set; }
    public string? GenStarted { get; set; }
    public string? GenFinished { get; set; }
    public ChatMessageExtra? Extra { get; set; }
}