using System.IO;
using System.Linq;

namespace DeveSolToSharp.Helpers
{
    public static class NamespaceFinder
    {
        public static string DetermineNamespace(string csproj, string outputDir)
        {
            var csprojWithoutExtension = Path.GetFileNameWithoutExtension(csproj);
            var dirHere = Path.GetDirectoryName(csproj);

            // \DeveBlockStacker\BusinessLogic\blah1\1
            var differenceInDir = outputDir.Substring(dirHere.Length);
            // .DeveBlockStacker.BusinessLogic.blah1.1
            var differenceInDirDots = differenceInDir.Replace(@"\", ".").Replace("/", ".");
            // DeveBlockStacker.BusinessLogic.blah1.1
            var differenceInDirDotsTrimmed = differenceInDirDots.Trim('.');
            // Splitted
            var splittedDiff = differenceInDirDotsTrimmed.Split('.');
            // Remove only digits
            var filteredSplit = splittedDiff.Where(t => !char.IsDigit(t.First()));
            // Capitalize first letter
            var firstLetterCapitalized = filteredSplit.Select(t => StringHelper.FirstCharToUpper(t));
            // Rejoin string
            var rejoined = string.Join(".", firstLetterCapitalized);



            var namespaceDetermined = $"{csprojWithoutExtension}.{rejoined}";

            return namespaceDetermined;
        }
    }
}
