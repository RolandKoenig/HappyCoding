$ErrorActionPreference = "Stop"

dotnet build -c Release ../HappyCoding.AspNetCoreWindowsService.slnx
dotnet publish -c Release -r win-x64 -o ../publish ../src/HappyCoding.AspNetCoreWindowsService/HappyCoding.AspNetCoreWindowsService.csproj

# Remove development-only configuration from the published output
rm ../publish/appsettings.Development.json