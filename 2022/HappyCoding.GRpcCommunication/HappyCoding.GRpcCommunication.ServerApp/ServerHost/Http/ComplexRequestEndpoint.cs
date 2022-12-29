using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HappyCoding.GRpcCommunication.Shared.Dtos;
using Microsoft.AspNetCore.Builder;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.Http;

public static class ComplexRequestEndpoint
{
    public static WebApplication MapComplexRequestEndpoint(this WebApplication app)
    {
        app.MapPost("/http/ComplexRequest", PostComplexRequestAsync);
        return app;
    }

    private static async Task<ComplexResponseDto> PostComplexRequestAsync(
        ComplexRequestDto request,
        ServerOptions options)
    {
        // Use given seed to calculate same response for same request
        var random = new Random(request.StationId);

        var response = new ComplexResponseDto();
        response.Message = "Test response";

        var responseItemCount = random.Next(5, 15);
        response.DummyItemList = new List<DummyResponseItemDto>(responseItemCount);
        for (var loop = 0; loop < responseItemCount; loop++)
        {
            response.DummyItemList.Add(new DummyResponseItemDto()
            {
                Property1 = random.Next(1000, int.MaxValue)
            });
        }

        if (options.SimulatedProcessingTimeMS > 0)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(options.SimulatedProcessingTimeMS));
        }

        return response;
    }
}
