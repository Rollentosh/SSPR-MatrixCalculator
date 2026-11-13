FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ServerApp/ServerApp.csproj ServerApp/
COPY ServerApp.Tests/ServerApp.Tests.csproj ServerApp.Tests/
RUN dotnet restore ServerApp.Tests/ServerApp.Tests.csproj

COPY . .

RUN dotnet test ServerApp.Tests/ServerApp.Tests.csproj --no-restore
RUN dotnet publish ServerApp/ServerApp.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "ServerApp.dll"]
