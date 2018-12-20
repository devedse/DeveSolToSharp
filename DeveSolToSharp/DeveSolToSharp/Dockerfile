# Stage 1
FROM microsoft/dotnet:2.2-sdk AS builder
WORKDIR /source

# caches restore result by copying csproj file separately
COPY /SolToSharpLogic/*.csproj /source/SolToSharpLogic/
COPY /SolToSharpLogic.Tests/*.csproj /source/SolToSharpLogic.Tests/
COPY /SolToSharpLogic.sln /source/
RUN ls
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet build --configuration Release
RUN dotnet test --configuration Release ./SolToSharpLogic.Tests/SolToSharpLogic.Tests.csproj
RUN dotnet publish ./SolToSharpLogic/SolToSharpLogic.csproj --output /app/ --configuration Release

# Stage 2
FROM microsoft/aspnetcore:2.0.5-stretch
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "SolToSharpLogic.dll"]