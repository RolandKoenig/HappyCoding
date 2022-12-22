using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.Http.SimpleRequest;

public static class SimpleRequestEndpoint
{
    public static WebApplication MapSimpleRequestEndpoint(this WebApplication app)
    {
        app.MapPost("/http/SimpleRequest", PostSimpleRequestAsync)
            .Produces<SimpleResponse>();
        return app;
    }

    public static Task<SimpleResponse> PostSimpleRequestAsync(SimpleRequest request)
    {
        return Task.FromResult(new SimpleResponse()
        {
            Message = "Test response"
        });
    }
}
