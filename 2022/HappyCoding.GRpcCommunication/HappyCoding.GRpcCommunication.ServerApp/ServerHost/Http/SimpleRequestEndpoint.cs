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

    private static Task<SimpleResponseDto> PostSimpleRequestAsync(SimpleRequestDto requestDto)
    {
        return Task.FromResult(new SimpleResponseDto()
        {
            Message = "Test response"
        });
    }
}
