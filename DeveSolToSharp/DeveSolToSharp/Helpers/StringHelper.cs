using System.Text.RegularExpressions;

namespace DeveSolToSharp.Helpers
{
    public static class StringHelper
    {
        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string NormalizeLineEndings(string input)
        {
            return Regex.Replace(input, @"\r\n|\n\r|\n|\r", "\r\n");
        }
    }
}
