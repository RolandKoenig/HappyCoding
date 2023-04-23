using HappyCoding.GrpcCommunicationFeatures.Server.WithJsonTranscoding.Services;
using Microsoft.OpenApi.Models;

namespace HappyCoding.GrpcCommunicationFeatures.Server.WithJsonTranscoding;

public class Programm
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // ###### Configure services
        
        // gRPC
        builder.Services.AddGrpc().AddJsonTranscoding();

        // Swagger
        builder.Services.AddGrpcSwagger();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
        });

        // ###### Configure request pipeline
        var app = builder.Build();
        
        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
        
        // gRPC
        app.MapGrpcService<GreeterService>();
         
        app.Run();
    }
}