##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
##Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
##For more information, please see https://aka.ms/containercompat
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
##OPY ["Websocket_UI.csproj", "."]
#RUN dotnet restore "./././Websocket_UI.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "./Websocket_UI.csproj" -c %BUILD_CONFIGURATION% -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./Websocket_UI.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Websocket_UI.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN apt-get update \
    && DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
        ca-certificates

# hadolint ignore=DL#3008,DL#3009
RUN apt-get update \
    && apt-get install -y --no-install-recommends libunwind-dev

# hadolint ignore=DL#3008,DL#3009
RUN apt-get update \
    && apt-get install -y --no-install-recommends vim

# hadolint ignore=DL#3008,DL#3009
RUN apt-get update \
    && apt-get install -y --no-install-recommends telnet
    
# hadolint ignore=DL#3008,DL#3009
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl

WORKDIR /app 

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Websocket_UI.dll"]