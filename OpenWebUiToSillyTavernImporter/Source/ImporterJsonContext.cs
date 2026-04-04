using System.Text.Json.Serialization;

namespace OpenWebUiToSillyTavernImporter;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    UseStringEnumConverter = true
)]
[JsonSerializable(typeof(ChatExportRoot))]
[JsonSerializable(typeof(List<ChatExportRoot>))]
public partial class ImporterJsonContext : JsonSerializerContext;