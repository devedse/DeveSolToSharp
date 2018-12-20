using DeveSolToSharp.Helpers;
using System.Collections.Generic;
using System.IO;

namespace DeveSolToSharp.SolToSharpLogic
{
    public static class TemplateProvider
    {
        private static Dictionary<string, string> cacheDict = new Dictionary<string, string>();

        public static string GetTemplate(string name)
        {
            if (cacheDict.TryGetValue(name, out string output))
            {
                return output;
            }

            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(location);

            var templateFile = Path.Combine(directory, "Templates", $"{name}.txt");

            var readTxt = File.ReadAllText(templateFile);
            readTxt = StringHelper.NormalizeLineEndings(readTxt);
            cacheDict.Add(name, readTxt);
            return readTxt;
        }

        public static string GetAbstractContractTemplate(string desiredNameSpace)
        {
            var template = GetTemplate("AbstractContractTemplate");
            template = template.Replace("{NameSpace}", desiredNameSpace);
            return template;
        }

        public static string GetContractOnDiskTemplate(string desiredNameSpace)
        {
            var template = GetTemplate("ContractOnDiskTemplate");
            template = template.Replace("{NameSpace}", desiredNameSpace);
            return template;
        }

        public static string GetContractTemplate(string desiredNameSpace, string className, string contractPath, string methods, string events, string usingForPocos, string usingForEventPocos)
        {
            var template = GetTemplate("ContractTemplate");
            template = template.Replace("{NameSpace}", desiredNameSpace);
            template = template.Replace("{ClassName}", className);
            template = template.Replace("{ContractPath}", contractPath);
            template = template.Replace("{Methods}", methods);
            template = template.Replace("{Events}", events);
            template = template.Replace("{UsingForPocos}", usingForPocos);
            template = template.Replace("{UsingForEventPocos}", usingForEventPocos);
            return template;
        }

        public static string GetMethodTemplateCallAsync(string methodName, string returnType, string methodParameters, string parametersForNethereum)
        {
            var methodNameCapitalized = StringHelper.FirstCharToUpper(methodName);

            var template = GetTemplate("MethodTemplateCallAsync");
            template = template.Replace("{MethodNameCapitalized}", methodNameCapitalized);
            template = template.Replace("{MethodName}", methodName);
            template = template.Replace("{ReturnType}", returnType);
            template = template.Replace("{MethodParameters}", methodParameters);
            template = template.Replace("{ParametersForNethereum}", parametersForNethereum);
            return template;
        }

        public static string GetMethodTemplateCallDeserializingToObjectAsync(string methodName, string returnType, string methodParameters, string parametersForNethereum)
        {
            var methodNameCapitalized = StringHelper.FirstCharToUpper(methodName);

            var template = GetTemplate("MethodTemplateCallDeserializingToObjectAsync");
            template = template.Replace("{MethodNameCapitalized}", methodNameCapitalized);
            template = template.Replace("{MethodName}", methodName);
            template = template.Replace("{ReturnType}", returnType);
            template = template.Replace("{MethodParameters}", methodParameters);
            template = template.Replace("{ParametersForNethereum}", parametersForNethereum);
            return template;
        }

        public static string GetMethodTemplateSendTransactionNoOutput(string methodName, string methodParameters, string parametersForNethereum)
        {
            var methodNameCapitalized = StringHelper.FirstCharToUpper(methodName);

            var template = GetTemplate("MethodTemplateSendTransactionNoOutput");
            template = template.Replace("{MethodNameCapitalized}", methodNameCapitalized);
            template = template.Replace("{MethodName}", methodName);
            template = template.Replace("{MethodParameters}", methodParameters);
            template = template.Replace("{ParametersForNethereum}", parametersForNethereum);
            return template;
        }

        public static string GetPocoTemplate(string desiredNameSpace, string lastNamespacePart, string className, string properties)
        {
            var template = GetTemplate("PocoTemplate");
            template = template.Replace("{NameSpace}", desiredNameSpace);
            template = template.Replace("{LastNamespacePart}", lastNamespacePart);
            template = template.Replace("{ClassName}", className);
            template = template.Replace("{Properties}", properties);
            return template;
        }

        public static string GetPocoEventTemplate(string desiredNameSpace, string lastNamespacePart, string className, string properties)
        {
            var template = GetTemplate("PocoEventTemplate");
            template = template.Replace("{NameSpace}", desiredNameSpace);
            template = template.Replace("{LastNamespacePart}", lastNamespacePart);
            template = template.Replace("{ClassName}", className);
            template = template.Replace("{Properties}", properties);
            return template;
        }

        public static string GetEventTemplate(string eventName, string returnType)
        {
            var eventNameCapitalized = StringHelper.FirstCharToUpper(eventName);

            var template = GetTemplate("EventTemplate");
            template = template.Replace("{EventNameCapitalized}", eventNameCapitalized);
            template = template.Replace("{EventName}", eventName);
            template = template.Replace("{ReturnType}", returnType);
            return template;
        }
    }
}
