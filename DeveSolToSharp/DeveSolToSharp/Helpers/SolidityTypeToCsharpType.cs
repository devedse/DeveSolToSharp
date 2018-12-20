using System.Text.RegularExpressions;

namespace DeveSolToSharp.Helpers
{
    public static class SolidityTypeToCsharpType
    {
        private static Regex IsArrayRegex = new Regex(@"\[\d*\]$", RegexOptions.Compiled);

        public static string ConvertToCsharpType(string solidityType)
        {
            bool isArray = false;
            string actualType = solidityType;
            var arrayMatch = IsArrayRegex.Match(actualType);
            if (arrayMatch.Success)
            {
                actualType = actualType.Substring(0, actualType.Length - arrayMatch.Length);
                isArray = true;
            }

            string csharpType = $"?????, sol type: {actualType}";
            if (actualType.StartsWith("uint") || actualType.StartsWith("int"))
            {
                csharpType = "BigInteger";
            }
            else if (actualType == "address" || actualType == "string")
            {
                csharpType = "string";
            }
            else if (actualType == "bool")
            {
                csharpType = "bool";
            }



            if (isArray)
            {
                return $"List<{csharpType}>";
            }
            else
            {
                return csharpType;
            }
        }
    }
}
