using System.Diagnostics;

namespace HappyCoding.AspNetCoreWindowsService.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Incoming request: {Method} {Path}{QueryString}",
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString);
        
        await _next(context);
        
        stopwatch.Stop();
        _logger.LogInformation(
            "Request completed: {Method} {Path}{QueryString} responded {StatusCode} in {ElapsedMilliseconds} ms",
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}