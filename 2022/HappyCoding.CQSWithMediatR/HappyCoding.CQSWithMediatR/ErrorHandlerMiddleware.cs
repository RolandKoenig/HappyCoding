using System.Net;
using System.Text.Json;
using HappyCoding.CQSWithMediatR.Application.Infrastructure;

namespace HappyCoding.CQSWithMediatR;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorString = (string?) null;
            switch(error)
            {
                case RequestValidationException e: // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorString = e.Message;
                    break;

                case KeyNotFoundException e: // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                     
                default: // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = errorString });
            await response.WriteAsync(result);
        }
    }
}