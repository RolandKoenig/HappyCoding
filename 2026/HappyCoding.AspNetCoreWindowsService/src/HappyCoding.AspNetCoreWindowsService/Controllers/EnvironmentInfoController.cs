using HappyCoding.AspNetCoreWindowsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.AspNetCoreWindowsService.Controllers;

[ApiController]
[Route("[controller]")]
public class EnvironmentInfoController
{
    private readonly IHostEnvironment _hostEnvironment;
    
    public EnvironmentInfoController(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;    
    }
    
    [HttpGet]
    public EnvironmentInfo Get()
    {
        return new EnvironmentInfo(
            _hostEnvironment.ApplicationName,
            _hostEnvironment.EnvironmentName,
            _hostEnvironment.ContentRootPath,
            Environment.CurrentDirectory);
    }
}