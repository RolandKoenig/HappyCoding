using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.ComplexRequest;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;

internal class ComplexRequestHandlerService : ComplexRequestHandler.ComplexRequestHandlerBase
{
    private readonly ServerOptions _options;

    public ComplexRequestHandlerService(ServerOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public override async Task<ComplexResponse> Handle(ComplexRequest request, ServerCallContext context)
    {
        // Use given seed to calculate same response for same request
        var random = new Random(request.StationId);

        var response = new ComplexResponse();
        response.Message = "Test response";

        var responseItemCount = random.Next(5, 15);
        for (var loop = 0; loop < responseItemCount; loop++)
        {
            response.DummyItemList.Add(new DummyResponseItem()
            {
                Property1 = random.Next(1000, int.MaxValue)
            });
        }

        if (_options.SimulatedProcessingTimeMS > 0)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(_options.SimulatedProcessingTimeMS));
        }

        return response;
    }
}
