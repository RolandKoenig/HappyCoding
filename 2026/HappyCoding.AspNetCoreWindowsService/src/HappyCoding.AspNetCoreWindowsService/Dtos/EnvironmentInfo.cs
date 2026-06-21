namespace HappyCoding.AspNetCoreWindowsService.Dtos;

public record EnvironmentInfo(
    string ApplicationName,
    string EnvironmentName,
    string ContentRootPath,
    string CurrentDirectory);