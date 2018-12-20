using System;
using System.IO;
using System.Linq;

namespace DeveSolToSharp.Helpers
{
    public static class FileFinder
    {
        public static string FoundCsprojFile(string startDir)
        {
            var allFiles = Directory.GetFiles(startDir);
            var firstFoundCsprojFile = allFiles.Where(t => Path.GetExtension(t).Equals(".csproj", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(firstFoundCsprojFile))
            {
                return firstFoundCsprojFile;
            }
            else
            {
                var parentDir = Path.GetDirectoryName(startDir);
                if (parentDir == null)
                {
                    return null;
                }
                return FoundCsprojFile(parentDir);
            }
        }
    }
}
