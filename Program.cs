var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve wwwroot/index.html at "/" and other static assets.
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/healthz", () => Results.Ok(new { status = "healthy" }));

app.Run();
