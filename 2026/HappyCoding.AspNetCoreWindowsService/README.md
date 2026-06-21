# Sample for an ASP.NET Core Windows Service
 - Service uses Microsoft.Extensions.Hosting.WindowsServices
 - Service is configured to run as a Windows Service
 - See publish, install and uninstall scripts in ./script
   - publish.ps1 removes development-only files from publish directory
   - install / uninstall scripts configure the service to run as a Windows Service from ./publish directory
 - <RuntimeIdentifier>win-x64</RuntimeIdentifier> in the .csproj removes unnecessary files from publish directory

# Further reading
 - Source of Microsoft.Extensions.Hosting.WindowsServices: https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.Hosting.WindowsServices
 - Official documentation: https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-10.0