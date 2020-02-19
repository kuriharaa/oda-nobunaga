#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-sac2016 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
#COPY src/*.sln .
COPY src/src/*.csproj src/src/
#COPY ["GiveawayFreeSteamBot.csproj", ""]
COPY . .
#RUN dotnet restore "./GiveawayFreeSteamBot.csproj"
RUN dotnet restore src/src/GiveawayFreeSteamBot.csproj


#WORKDIR "/src/."
#RUN dotnet build "GiveawayFreeSteamBot.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/src
RUN dotnet publish -c Release -o /src/publish

#FROM base AS final
#FROM base AS runtime
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
#ENTRYPOINT ["dotnet", "GiveawayFreeSteamBot.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GiveawayFreeSteamBot.dll