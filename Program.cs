/**
 * Configures the Hello World web application, including static page serving,
 * page-load test logging, and the health endpoint.
 *
 * Behavior:
 * - Emits one test log at every supported verbosity level for each landing-page request
 * - Uses the Azure App Service diagnostics provider when hosted in Azure
 * - Serves the static landing page and exposes the /healthz endpoint
 */
var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddAzureWebAppDiagnostics();

var app = builder.Build();
var pageLoadLogger = app.Services
    .GetRequiredService<ILoggerFactory>()
    .CreateLogger("HelloWorld.PageLoad");

app.Use(async (context, next) =>
{
    var requestedPath = context.Request.Path;
    var isPageRequest = HttpMethods.IsGet(context.Request.Method)
        && (requestedPath == "/" || requestedPath == "/index.html");

    await next();

    if (!isPageRequest)
    {
        return;
    }

    pageLoadLogger.LogTrace("Test page-load log at Trace level for {Path}", requestedPath);
    pageLoadLogger.LogDebug("Test page-load log at Debug level for {Path}", requestedPath);
    pageLoadLogger.LogInformation("Test page-load log at Information level for {Path}", requestedPath);
    pageLoadLogger.LogWarning("Test page-load log at Warning level for {Path}", requestedPath);
    pageLoadLogger.LogError("Test page-load log at Error level for {Path}", requestedPath);
    pageLoadLogger.LogCritical("Test page-load log at Critical level for {Path}", requestedPath);
});

// Serve wwwroot/index.html at "/" and other static assets.
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/healthz", () => Results.Ok(new { status = "healthy" }));

app.Run();
