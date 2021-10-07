using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace HappyCoding.BlazorWith3D.BlazorServer.Util
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                await _next(context);
                stopWatch.Stop();
            }
            finally
            {
                var elapsedMilliseconds = stopWatch.Elapsed.TotalMilliseconds;
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode} ({statusCodeDesc}), Time: {millis:F0}ms",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode,
                    ReasonPhrases.GetReasonPhrase(context.Response?.StatusCode ?? 0),
                    elapsedMilliseconds);
            }
        }
    }
}
