var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddHealthChecksUI(settings =>
    {
        settings.AddHealthCheckEndpoint("Application", "https://localhost:5001/healthz");
    })
    .AddInMemoryStorage();

// Configure the HTTP request pipeline.
var app = builder.Build();
app.MapHealthChecksUI();
app.Run();