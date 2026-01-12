# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./src/Consolidation/Consolidation.Api/Consolidation.Api.csproj
RUN dotnet publish ./src/Consolidation/Consolidation.Api/Consolidation.Api.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Consolidation.Api.dll"]
