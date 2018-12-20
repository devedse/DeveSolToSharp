﻿using System.IO;

namespace DeveSolToSharp.SolToSharpLogic
{
    public class ContractOnDisk
    {
        public string Name { get; private set; }

        public string Abi { get; private set; }
        public string Bin { get; private set; }
        public string Json { get; private set; }

        public ContractOnDisk(string inputDirectory, string contractName)
        {
            Name = contractName;

            Abi = File.ReadAllText(Path.Combine(inputDirectory, $"{contractName}.abi"));
            Bin = File.ReadAllText(Path.Combine(inputDirectory, $"{contractName}.bin"));
            Json = File.ReadAllText(Path.Combine(inputDirectory, $"{contractName}.json"));
        }
    }
}
