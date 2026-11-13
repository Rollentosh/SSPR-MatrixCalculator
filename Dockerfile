FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ServerApp/ServerApp.csproj", "ServerApp/"]
COPY ["ClientApp/ClientApp.csproj", "ClientApp/"]
RUN dotnet restore "ServerApp/ServerApp.csproj" && dotnet restore "ClientApp/ClientApp.csproj"

COPY . .
RUN dotnet publish "ServerApp/ServerApp.csproj" -c Release -o /app/server /p:UseAppHost=false
RUN dotnet publish "ClientApp/ClientApp.csproj" -c Release -o /app/client /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/server ./server
COPY --from=build /app/client ./client

ENV SERVER_URL=http://localhost:5000

ENTRYPOINT ["sh", "-c", "dotnet /app/server/ServerApp.dll & sleep 2 && dotnet /app/client/ClientApp.dll"]
