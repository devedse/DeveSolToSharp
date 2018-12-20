# DeveSolToSharp
With this tool you can create C# files based on the .ABI of solidity files.

Install the tool by running:
`dotnet tool install -g DeveSolToSharp`

Then run the tool from the directory the .ABI files are in.

## Parameters

| Param (Short) | Param (Full) | Description | Default Value | Required |
| -- | -- | -- | -- | -- |
| -i | --inputdir | The directory containing the .abi files | Current directory | False |
| -o | --outputdir | The directory the C# files should be generated in | inputdirectory\DeveSolToSharp | False |
| -n | --namespace | The desired C# namespace, e.g. MySolution.Contracts | Tried to determine this automatically | False |

## Build status

| Travis (Linux/Osx build) | AppVeyor (Windows build) |
|:------------------------:|:------------------------:|
| [![Build Status](https://travis-ci.org/devedse/DeveSolToSharp.svg?branch=master)](https://travis-ci.org/devedse/DeveSolToSharp) | [![Build status](https://ci.appveyor.com/api/projects/status/datwgk9gb4gmpodi?svg=true)](https://ci.appveyor.com/project/devedse/DeveSolToSharp) |

## Code Coverage Status

| CodeCov |
|:-------:|
| [![codecov](https://codecov.io/gh/devedse/DeveSolToSharp/branch/master/graph/badge.svg)](https://codecov.io/gh/devedse/DeveSolToSharp) |

## Code Quality Status

| SonarQube |
|:---------:|
| [![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=DeveSolToSharp&metric=alert_status)](https://sonarcloud.io/dashboard?id=DeveSolToSharp) |

## Package

| NuGet |
|:-----:|
| [![NuGet](https://img.shields.io/nuget/v/DeveSolToSharp.svg)](https://www.nuget.org/packages/DeveSolToSharp/) |
