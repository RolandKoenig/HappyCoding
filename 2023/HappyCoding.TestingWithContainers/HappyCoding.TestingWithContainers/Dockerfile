#####################################
# Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID
WORKDIR /src
COPY . .
RUN dotnet restore "HappyCoding.TestingWithContainers/HappyCoding.TestingWithContainers.csproj"
WORKDIR "/src/HappyCoding.TestingWithContainers"
RUN dotnet build "HappyCoding.TestingWithContainers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyCoding.TestingWithContainers.csproj" -c Release -o /app/publish /p:UseAppHost=false

#####################################
# Runtime image
## Configure OS
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

## run as non-root 
## see https://levelup.gitconnected.com/6-things-to-know-when-dockerizing-microsoft-net-apps-in-production-45b8c27a41b0

## Create a group and user
RUN addgroup --gid 1000 -S app && adduser --uid 1000 -S app -G app

## Create the work dir and set permissions as WORKDIR set the permissions as root
RUN mkdir /home/app/net && chown -R app:app /home/app/net
WORKDIR /home/app/net

## Copy and start the application
USER app
COPY --from=publish /app/publish .

## Update access rights
USER root
RUN chmod a+r /home/app/net/appsettings.json

USER app
ENTRYPOINT ["dotnet", "HappyCoding.TestingWithContainers.dll"]