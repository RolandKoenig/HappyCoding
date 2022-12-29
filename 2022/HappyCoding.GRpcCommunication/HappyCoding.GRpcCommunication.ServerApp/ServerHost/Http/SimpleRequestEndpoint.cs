using System;
using System.Threading;
using System.Threading.Tasks;
using HappyCoding.GRpcCommunication.Shared.Dtos;
using Microsoft.AspNetCore.Builder;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.Http;

public static class SimpleRequestEndpoint
{
    public static WebApplication MapSimpleRequestEndpoint(this WebApplication app)
    {
        app.MapPost("/http/SimpleRequest", PostSimpleRequestAsync);
        return app;
    }

    private static async Task<SimpleResponseDto> PostSimpleRequestAsync(
        SimpleRequestDto requestDto,
        ServerOptions options)
    {
        if (options.SimulatedProcessingTimeMS > 0)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(options.SimulatedProcessingTimeMS));
        }

        return new SimpleResponseDto()
        {
            Message = "Test response"
        };
    }
}
