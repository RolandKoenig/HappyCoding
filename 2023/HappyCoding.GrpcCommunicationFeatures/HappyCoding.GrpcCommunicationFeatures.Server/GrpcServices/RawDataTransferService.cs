using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.Server.GrpcServices;

public class RawDataTransferService : RawDataTransfer.RawDataTransferBase
{
    private readonly ILogger _logger;
    
    public RawDataTransferService(ILogger<RawDataTransferService> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc />
    public override async Task<Empty> SendRawData(RawDataRequest request, ServerCallContext context)
    {
        _logger.LogInformation(
            "Received file {FileName} with {Length} bytes",
            request.FileName,
            request.RawData.Length);

        return new Empty();
    }
}