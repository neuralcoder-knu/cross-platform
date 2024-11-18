using Lab4;
using McMaster.Extensions.CommandLineUtils;

[Command(Name = "Lab4", Description = "Console app for labs")]
[Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
internal class Program
{
    static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private void OnExecute()
    {
        PrintInfo();
    }

    private void OnUnknownCommand(CommandLineApplication app)
    {
        PrintInfo();
    }

    private void PrintInfo()
    {
        Console.WriteLine("dotnet run {labName[lab1/lab2/lab3]} - run lab");
        Console.WriteLine("dotnet run version - information");
        Console.WriteLine("dotnet run set-path - set the default path to the lab directory");
    }
}

[Command(Name = "version", Description = "Displays app version and author")]
class VersionCommand
{
    private void OnExecute()
    {
        Console.WriteLine("Author: Koval Mykola IPZ-32");
        Console.WriteLine("Version: 0.0.1");
    }
}

[Command(Name = "run", Description = "Run a specific lab")]
class RunCommand
{

    private const string DEFAULT_INPUT_FILE_NAME = "INPUT.txt";
    private const string DEFAULT_OUTPUT_FILE_NAME = "OUTPUT.txt";

    
    [Argument(0, "lab", "Specify lab to run (lab1)")]
    public string? Lab { get; set; }

    [Option("-I|--input", "Input file", CommandOptionType.SingleValue)]
    public string? InputFile { get; set; }

    [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
    public string? OutputFile { get; set; }


    private void OnExecute()
    {
        var labExecutor = new Runner(Lab,
            SelectPath(Lab, InputFile, DEFAULT_INPUT_FILE_NAME),
            SelectPath(Lab, OutputFile, DEFAULT_OUTPUT_FILE_NAME));
        labExecutor.Run();
    }

    private string SelectPath(string? lab, string? definedPath, string fileName)
    {
        if (definedPath != null)
        {
            if (!File.Exists(definedPath))
            {
                throw new IOException($"File not found {definedPath}");
            }
            
            return definedPath;
        }
        
        var env = Environment.GetEnvironmentVariable("LAB_PATH");
        if (env != null && Directory.Exists(env))
        {
            var fullPath = Path.Combine(env, fileName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        var projectRoot = Directory.GetCurrentDirectory();

        var homeFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), fileName);

        if (File.Exists(homeFolder))
        {
            return homeFolder;
        }
        
        throw new IOException($"File not found {homeFolder}");
    }
    
    //TODO: mb refactor, looks not fine, idk
    private string CapitalizeFirstLetter(string input)
    {
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}

[Command(Name = "set-path", Description = "Set input/output path")]
class SetPathCommand
{
    [Option("-p|--path", "Setting path for input/output files", CommandOptionType.SingleValue)]
    public required string Path { get; set; }

    private void OnExecute()
    {
        Environment.SetEnvironmentVariable("LAB_PATH", Path);
        Console.WriteLine($"New Path: {Path}");
        Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
    }
}