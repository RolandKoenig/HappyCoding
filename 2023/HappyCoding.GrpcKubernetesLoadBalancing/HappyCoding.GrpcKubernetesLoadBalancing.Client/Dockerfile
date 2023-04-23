FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "HappyCoding.GrpcKubernetesLoadBalancing.Client.csproj"
RUN dotnet build "HappyCoding.GrpcKubernetesLoadBalancing.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyCoding.GrpcKubernetesLoadBalancing.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HappyCoding.GrpcKubernetesLoadBalancing.Client.dll"]