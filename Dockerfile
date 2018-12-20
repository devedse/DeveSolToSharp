# Stage 1
FROM microsoft/dotnet:2.2-sdk AS builder
WORKDIR /source

# caches restore result by copying csproj file separately
COPY /DeveSolToSharp/*.csproj /source/DeveSolToSharp/
COPY /DeveSolToSharp.Tests/*.csproj /source/DeveSolToSharp.Tests/
COPY /DeveSolToSharp.sln /source/
RUN ls
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet build --configuration Release
RUN dotnet test --configuration Release ./DeveSolToSharp.Tests/DeveSolToSharp.Tests.csproj
RUN dotnet publish ./DeveSolToSharp/DeveSolToSharp.csproj --output /app/ --configuration Release

# Stage 2
FROM microsoft/aspnetcore:2.0.9-stretch
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "DeveSolToSharp.dll"]