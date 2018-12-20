using CommandLine;
using DeveSolToSharp.Config;

namespace DeveSolToSharp
{
    public class CommandLineArguments
    {
        [Option('n', "namespace", Required = false, HelpText = "The namespace to generate the objects in (Default: It tries to be smart)")]
        public string DesiredNameSpace { get; set; }

        [Option('i', "inputdir", Required = false, HelpText = "The directory to find the .abi files in (Default: currentdirectory)")]
        public string InputDirectory { get; set; }

        [Option('o', "outputdir", Required = false, HelpText = @"The directory to generate the C# files in (Default: inputdirectory\" + Constants.DefaultOutDirName + @"\)")]
        public string OutputDirectory { get; set; }
    }
}
