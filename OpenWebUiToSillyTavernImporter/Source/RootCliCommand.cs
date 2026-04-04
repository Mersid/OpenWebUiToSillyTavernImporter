using DotMake.CommandLine;

namespace OpenWebUiToSillyTavernImporter;

[CliCommand(Description = "Converts Open WebUI export formats to SillyTavern import format.")]
public class RootCliCommand
{
    [CliOption(Description = "File path of the Open WebUI export. This should be a JSON file.", ValidationRules = CliValidationRules.ExistingFile)]
    public required FileInfo InputFile { get; set; }

    [CliOption(Description = "File location to write the SillyTavern export JSONL. Omit to print to terminal.", Required = false)]
    public FileInfo? OutputFile { get; set; }

    [CliOption(Description = "Name of the agent when imported into SillyTavern.")]
    public string AgentName { get; set; } = "Agent";

    [CliOption(Description = "Name of the user when imported into SillyTavern.")]
    public string UserName { get; set; } = "User";

    [CliOption(Description = "Name of the API to use.")]
    public string ApiName { get; set; } = "none";

    public void Run()
    {
        Application application = new Application(this);
        application.Run();
    }
}