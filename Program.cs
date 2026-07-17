var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World from .NET 10 on Azure Web Apps!");

app.MapGet("/healthz", () => Results.Ok(new { status = "healthy" }));

app.Run();
