#####################################
# Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "HappyCoding.WebApiWithSqliteDb/HappyCoding.WebApiWithSqliteDb.csproj"
WORKDIR "/src/HappyCoding.WebApiWithSqliteDb"
RUN dotnet build "HappyCoding.WebApiWithSqliteDb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyCoding.WebApiWithSqliteDb.csproj" -c Release -o /app/publish /p:UseAppHost=false

#####################################
# Runtime image
## Configure OS
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final

## Create a group, a user, the workdir and set permissions
RUN addgroup --gid 1000 -S app && \
    adduser --uid 1000 -S app -G app && \
    mkdir /home/app/net && \
    mkdir /home/app/net/data && \
    chown -R app:app /home/app/net
WORKDIR /home/app/net

## Copy and start the application
USER app
COPY --from=publish /app/publish .

## Update access rights
USER root
RUN chmod a+r /home/app/net/appsettings.json

VOLUME /home/app/net/data
EXPOSE 80

USER app
ENTRYPOINT ["dotnet", "HappyCoding.WebApiWithSqliteDb.dll"]