using System;
using System.Collections.Generic;
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

        public static IEnumerable<string> EnumerateAllFilesInDirectory(string dir)
        {
            var files = Directory.GetFiles(dir);
            foreach(var file in files)
            {
                yield return file;
            }

            var dirs = Directory.GetDirectories(dir);
            foreach(var subdir in dirs)
            {
                var allSubFiles = EnumerateAllFilesInDirectory(subdir);
                foreach(var subFile in allSubFiles)
                {
                    yield return subFile;
                }
            }
        }
    }
}
