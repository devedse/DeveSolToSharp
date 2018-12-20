using CommandLine;
using DeveSolToSharp.SolToSharpLogic;
using System.Threading;

namespace DeveSolToSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineArguments>(args)
              .WithParsed(opts => Start(opts));
            //.WithNotParsed<CommandLineArguments>((errs) => HandleParseError(errs));

        }

        public static void Start(CommandLineArguments commandLineArguments)
        {
            var completedSuccessfully = CsharpGen.Go(commandLineArguments);
            if (!completedSuccessfully)
            {

                Thread.Sleep(5000);
            }
        }
    }
}
