# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY src/*.sln .
COPY src/*.csproj src/
COPY . .
RUN dotnet restore src/GiveawayFreeSteamBot.csproj -p:RestoreUseSkipNonexistentTargets=false -nowarn:msb3202,nu1503

# publish
FROM build AS publish
WORKDIR /src/src
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GiveawayFreeSteamBot.dll
