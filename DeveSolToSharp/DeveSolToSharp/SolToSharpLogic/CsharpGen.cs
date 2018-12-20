using DeveSolToSharp.Config;
using DeveSolToSharp.Helpers;
using Newtonsoft.Json;
using QuickType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeveSolToSharp.SolToSharpLogic
{
    public static class CsharpGen
    {
        public static bool Go(CommandLineArguments cla)
        {
            if (string.IsNullOrWhiteSpace(cla.InputDirectory))
            {
                var inputDit = Directory.GetCurrentDirectory();
                Console.WriteLine($"{nameof(cla.InputDirectory)} is not set, defaulting to current directory: {inputDit}");
                cla.InputDirectory = inputDit;
            }

            if (string.IsNullOrWhiteSpace(cla.OutputDirectory))
            {
                var outputDir = Path.Combine(cla.InputDirectory, Constants.DefaultOutDirName);
                Console.WriteLine($"{nameof(cla.OutputDirectory)} is not set, defaulting to current directory: {outputDir}");
                cla.OutputDirectory = outputDir;
            }

            if (string.IsNullOrWhiteSpace(cla.DesiredNameSpace))
            {
                var parentDir = Path.GetDirectoryName(cla.OutputDirectory);
                var csproj = FileFinder.FoundCsprojFile(parentDir);

                if (string.IsNullOrWhiteSpace(csproj))
                {
                    Console.WriteLine("Error: Could not automatically determine namespace.");
                    return false;
                }



                string namespaceNew = NamespaceFinder.DetermineNamespace(csproj, cla.OutputDirectory);


                Console.WriteLine($"{nameof(cla.DesiredNameSpace)} is not set, automatically determined namespace: {namespaceNew}");
                cla.DesiredNameSpace = namespaceNew;
            }

            return GenerateCodeFiles(cla);
        }



        private static bool GenerateCodeFiles(CommandLineArguments cla)
        {
            var allFiles = Directory.GetFiles(cla.InputDirectory);

            var groups = allFiles.GroupBy(t => Path.GetFileNameWithoutExtension(t))
                .Where(t => t.Count(z => Path.GetExtension(z).Equals(".bin", StringComparison.OrdinalIgnoreCase)) == 1 &&
                            t.Count(z => Path.GetExtension(z).Equals(".abi", StringComparison.OrdinalIgnoreCase)) == 1 &&
                            t.Count(z => Path.GetExtension(z).Equals(".json", StringComparison.OrdinalIgnoreCase)) == 1);

            if (!groups.Any())
            {
                Console.WriteLine($"No contracts found in input dir {cla.InputDirectory}. Make sure that for every contract 3 files exist: ctrname.abi, ctrname.bin and ctrname.json");
                return false;
            }

            bool allWorked = true;
            foreach (var group in groups)
            {
                Console.WriteLine($"Found contract with name {group.Key}");
                var contractOnDisk = new ContractOnDisk(cla.InputDirectory, group.Key);
                var thisWorked = GenerateCodeFilesForContract(cla, contractOnDisk);
                if (thisWorked == false)
                {
                    allWorked = false;
                }
            }
            return allWorked;
        }


        private static bool GenerateCodeFilesForContract(CommandLineArguments cla, ContractOnDisk contractOnDisk)
        {
            if (!Directory.Exists(cla.OutputDirectory))
            {
                Directory.CreateDirectory(cla.OutputDirectory);
            }

            var abstractContractFileName = Path.Combine(cla.OutputDirectory, "AbstractContract.cs");
            if (!File.Exists(abstractContractFileName))
            {
                var abstractContractClass = TemplateProvider.GetAbstractContractTemplate(cla.DesiredNameSpace);
                File.WriteAllText(abstractContractFileName, StringHelper.NormalizeLineEndings(abstractContractClass), Encoding.UTF8);
            }

            var contractOnDiskFileName = Path.Combine(cla.OutputDirectory, "ContractOnDisk.cs");

            if (!File.Exists(contractOnDiskFileName))
            {
                var contractOnDiskClass = TemplateProvider.GetAbstractContractTemplate(cla.DesiredNameSpace);
                File.WriteAllText(contractOnDiskFileName, StringHelper.NormalizeLineEndings(contractOnDiskClass), Encoding.UTF8);
            }

            var abiJson = JsonConvert.DeserializeObject<List<AbiJson>>(contractOnDisk.Abi);

            var ctrNamePart = contractOnDisk.Name;
            var ctrClassName = $"{ctrNamePart}Contract";
            var pocoContractNamespace = $"{ctrClassName}Pocos";
            var eventContractNamespace = $"{ctrClassName}Pocos.Events";
            var contractCsFileName = $"{ctrClassName}.cs";
            var fullCcontractcsFileName = Path.Combine(cla.OutputDirectory, contractCsFileName);

            var dirPocos = Path.Combine(cla.OutputDirectory, pocoContractNamespace);
            if (!Directory.Exists(dirPocos))
            {
                Directory.CreateDirectory(dirPocos);
            }

            var dirEvents = Path.Combine(cla.OutputDirectory, pocoContractNamespace, "Events");
            if (!Directory.Exists(dirEvents))
            {
                Directory.CreateDirectory(dirEvents);
            }


            var sbMethods = new StringBuilder();
            var sbEvents = new StringBuilder();
            bool createdPocos = false;
            bool createdEventPocos = false;

            foreach (var method in abiJson)
            {
                if (method.Type == "function")
                {
                    var createdPoco = CreateFunction(cla, pocoContractNamespace, dirPocos, sbMethods, method);
                    if (createdPoco)
                    {
                        createdPocos = true;
                    }
                }
                else if (method.Type == "event")
                {
                    var createdEventPoco = CreateEvent(cla, eventContractNamespace, dirEvents, sbEvents, method);
                    if (createdEventPoco)
                    {
                        createdEventPocos = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown method type found: {method.Type}");
                }
            }

            string nameSpaceForPocos = "";
            if (createdPocos)
            {
                nameSpaceForPocos = $"using {cla.DesiredNameSpace}.{pocoContractNamespace};";
            }
            string nameSpaceForEvents = "";
            if (createdEventPocos)
            {
                nameSpaceForEvents = $"using {cla.DesiredNameSpace}.{eventContractNamespace};";
            }

            var templateContract = TemplateProvider.GetContractTemplate(cla.DesiredNameSpace, ctrClassName, contractOnDisk.Name, sbMethods.ToString(), sbEvents.ToString(), nameSpaceForPocos, nameSpaceForEvents);
            File.WriteAllText(fullCcontractcsFileName, StringHelper.NormalizeLineEndings(templateContract), Encoding.UTF8);




            Console.WriteLine(templateContract);
            return true;
        }

        private static bool CreateEvent(CommandLineArguments cla, string eventContractNamespace, string dirEvents, StringBuilder sbEvents, AbiJson method)
        {
            var propertiesSb = new StringBuilder();

            for (int i = 0; i < method.Inputs.Length; i++)
            {
                var ip = method.Inputs[i];

                //[Parameter("address", "player", 1, true)]
                //public string Player { get; set; }
                propertiesSb.AppendLine($"        [Parameter(\"{ip.Type}\", \"{ip.Name}\", {i + 1}, {(ip.Indexed == true).ToString().ToLowerInvariant()})]");

                string variableName = StringHelper.FirstCharToUpper(ip.Name);
                if (string.IsNullOrWhiteSpace(variableName))
                {
                    variableName = $"Value{i}";
                }

                propertiesSb.AppendLine($"        public {SolidityTypeToCsharpType.ConvertToCsharpType(ip.Type)} {variableName} {{ get; set; }}");
                propertiesSb.AppendLine();
            }


            var classNamePoco = StringHelper.FirstCharToUpper(method.Name);
            var outputFileName = $"{Path.Combine(dirEvents, classNamePoco)}.cs";
            var templatePocoEvent = TemplateProvider.GetPocoEventTemplate(cla.DesiredNameSpace, eventContractNamespace, classNamePoco, propertiesSb.ToString());
            File.WriteAllText(outputFileName, StringHelper.NormalizeLineEndings(templatePocoEvent), Encoding.UTF8);


            var eventTemplate = TemplateProvider.GetEventTemplate(method.Name, classNamePoco);
            sbEvents.AppendLine();
            sbEvents.AppendLine(eventTemplate);

            return true;
        }

        private static bool CreateFunction(CommandLineArguments cla, string pocoContractNamespace, string dirPocos, StringBuilder sbMethods, AbiJson method)
        {
            bool createdPoco = false;

            for (int i = 0; i < method.Inputs.Length; i++)
            {
                var inputHere = method.Inputs[i];
                if (string.IsNullOrWhiteSpace(inputHere.Name))
                {
                    inputHere.Name = $"value{i}";
                }
            }

            var methodParameters = string.Join(", ", method.Inputs.Select(t => $"{SolidityTypeToCsharpType.ConvertToCsharpType(t.Type)} {t.Name}"));
            var parametersForNethereum = string.Join(", ", method.Inputs.Select(t => t.Name));
            var parametersForNethereumStartingWithCommaIfTheyExist = parametersForNethereum;
            if (!string.IsNullOrWhiteSpace(parametersForNethereumStartingWithCommaIfTheyExist))
            {
                parametersForNethereumStartingWithCommaIfTheyExist = $", {parametersForNethereumStartingWithCommaIfTheyExist}";
            }

            if (method.Constant == false)
            {
                var templateMethod = TemplateProvider.GetMethodTemplateSendTransactionNoOutput(method.Name, methodParameters, parametersForNethereumStartingWithCommaIfTheyExist);
                sbMethods.AppendLine();
                sbMethods.AppendLine(templateMethod);
            }

            if (method.Outputs?.Length == 1)
            {
                var outputType = SolidityTypeToCsharpType.ConvertToCsharpType(method.Outputs[0].Type);

                var templateMethod = TemplateProvider.GetMethodTemplateCallAsync(method.Name, outputType, methodParameters, parametersForNethereum);
                sbMethods.AppendLine();
                sbMethods.AppendLine(templateMethod);


            }
            else if (method.Outputs?.Length > 1)
            {
                var classNamePoco = StringHelper.FirstCharToUpper(method.Name);

                var propertiesSb = new StringBuilder();

                for (int i = 0; i < method.Outputs.Length; i++)
                {
                    var op = method.Outputs[i];
                    //[Parameter("uint256", 1)]
                    //public BigInteger TokenId { get; set; }
                    propertiesSb.AppendLine($"        [Parameter(\"{op.Type}\", {i + 1})]");

                    string variableName = StringHelper.FirstCharToUpper(op.Name);
                    if (string.IsNullOrWhiteSpace(variableName))
                    {
                        variableName = $"Value{i}";
                    }

                    propertiesSb.AppendLine($"        public {SolidityTypeToCsharpType.ConvertToCsharpType(op.Type)} {variableName} {{ get; set; }}");
                    propertiesSb.AppendLine();
                }

                var outputFileName = $"{Path.Combine(dirPocos, classNamePoco)}.cs";
                var templatePoco = TemplateProvider.GetPocoTemplate(cla.DesiredNameSpace, pocoContractNamespace, classNamePoco, propertiesSb.ToString());
                createdPoco = true;
                File.WriteAllText(outputFileName, StringHelper.NormalizeLineEndings(templatePoco), Encoding.UTF8);

                var templateMethod = TemplateProvider.GetMethodTemplateCallDeserializingToObjectAsync(method.Name, classNamePoco, methodParameters, parametersForNethereumStartingWithCommaIfTheyExist);
                sbMethods.AppendLine();
                sbMethods.AppendLine(templateMethod);
            }
            else if (method.Constant == true)
            {
                Console.WriteLine("This shouldn't happen");
            }

            return createdPoco;
        }
    }
}
