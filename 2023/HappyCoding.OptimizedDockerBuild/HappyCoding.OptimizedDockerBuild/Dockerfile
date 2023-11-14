#####################################
# Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Improve cache usage: Copy *.csproj files only, the call .NET restore
# see https://andrewlock.net/optimising-asp-net-core-apps-in-docker-avoiding-manually-copying-csproj-files-part-2/
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore "HappyCoding.OptimizedDockerBuild/HappyCoding.OptimizedDockerBuild.csproj"

# Copy the rest of the application, then build and publish it
COPY . .
RUN dotnet build "HappyCoding.OptimizedDockerBuild/HappyCoding.OptimizedDockerBuild.csproj" -c Release -o /app/build
RUN dotnet publish "HappyCoding.OptimizedDockerBuild/HappyCoding.OptimizedDockerBuild.csproj" -c Release -o /app/publish /p:UseAppHost=false

#####################################
# Runtime image
## Configure OS
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
EXPOSE 80

## Create a group, user and work directory for the application
RUN addgroup --gid 1000 -S app &&  \
    adduser --uid 1000 -S app -G app && \
    mkdir /home/app/net && \
    chown -R app:app /home/app/net
WORKDIR /home/app/net

## Copy application and define entrypoint
USER app
COPY --chown=app:app --from=build /app/publish .
ENTRYPOINT ["dotnet", "HappyCoding.OptimizedDockerBuild.dll"]