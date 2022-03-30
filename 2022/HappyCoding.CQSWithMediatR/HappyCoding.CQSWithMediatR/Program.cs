using HappyCoding.CQSWithMediatR;
using HappyCoding.CQSWithMediatR.Application;
using HappyCoding.CQSWithMediatR.InMemoryRepositories;
using MediatR;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddCQSWithMediatRApplication();
builder.Services.AddInMemoryRepositories();

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();
