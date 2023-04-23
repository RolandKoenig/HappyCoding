FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "HappyCoding.GrpcKubernetesLoadBalancing.Server.csproj"
RUN dotnet build "HappyCoding.GrpcKubernetesLoadBalancing.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyCoding.GrpcKubernetesLoadBalancing.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HappyCoding.GrpcKubernetesLoadBalancing.Server.dll"]