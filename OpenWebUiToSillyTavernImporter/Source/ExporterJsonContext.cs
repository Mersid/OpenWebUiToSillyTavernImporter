using System.Text.Json.Serialization;

namespace OpenWebUiToSillyTavernImporter;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    UseStringEnumConverter = true
)]
[JsonSerializable(typeof(ChatMessage))]
[JsonSerializable(typeof(ChatMetadataLine))]
public partial class ExporterJsonContext : JsonSerializerContext;