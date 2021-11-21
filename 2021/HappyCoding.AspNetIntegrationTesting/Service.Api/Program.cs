var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();




// Make Program class public to be accessed from integration test project
public partial class Program
{

}