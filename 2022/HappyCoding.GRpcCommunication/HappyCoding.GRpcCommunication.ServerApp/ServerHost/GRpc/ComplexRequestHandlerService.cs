using System;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.ComplexRequest;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;

internal class ComplexRequestHandlerService : ComplexRequestHandler.ComplexRequestHandlerBase 
{
    /// <inheritdoc />
    public override Task<ComplexResponse> Handle(ComplexRequest request, ServerCallContext context)
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

        return Task.FromResult(response);
    }
}
