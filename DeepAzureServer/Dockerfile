# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["DeepAzureServer/DeepAzureServer.csproj", "DeepAzureServer/"]
COPY ["DeepAzureServer.Tests/DeepAzureServer.Tests.csproj", "DeepAzureServer.Tests/"]

# Restore dependencies for both
RUN dotnet restore "DeepAzureServer/DeepAzureServer.csproj"
RUN dotnet restore "DeepAzureServer.Tests/DeepAzureServer.Tests.csproj"

# Copy everything else and build
COPY . .

# Test Stage
WORKDIR "/src/DeepAzureServer.Tests"
# If this command fails, the docker build stops here!
RUN dotnet test "DeepAzureServer.Tests.csproj" --logger:trx

# Publish Stage
WORKDIR "/src/DeepAzureServer"
RUN dotnet build "DeepAzureServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeepAzureServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeepAzureServer.dll"]