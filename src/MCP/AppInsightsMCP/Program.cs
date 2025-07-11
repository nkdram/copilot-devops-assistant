using System.Net.Http.Headers;
using AppInsightsMCP.Service;
using AppInsightsMCP.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

// Check if we should run in Web API mode or MCP mode
var commandArgs = Environment.GetCommandLineArgs();
var isWebApiMode = commandArgs.Contains("--web") || commandArgs.Contains("--api");

if (isWebApiMode)
{
    // Web API mode
    var builder = WebApplication.CreateBuilder(args);

    // Configure services
    var appInsightsSettings = builder.Configuration.GetSection("AppInsights").Get<AppInsights>();

    // Add Web API services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "AppInsightsMCP API",
            Version = "v1",
            Description = "REST API for Azure App Insights operations",
        });
    });

    // Configure HttpClient for Application Insights
    builder.Services.AddSingleton(_ =>
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("https://api.applicationinsights.io/") };
        httpClient.DefaultRequestHeaders.Add("X-API-Key", appInsightsSettings?.ApiKey);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    });

    // Register services
    builder.Services.AddSingleton<ILogQueryService>(provider =>
    {
        var httpClient = provider.GetRequiredService<HttpClient>();
        return new LogQueryService(httpClient, appInsightsSettings?.ApplicationId ?? string.Empty);
    });

    var app = builder.Build();

    // Configure Web API pipeline
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppInsightsMCP API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
    
    app.UseRouting();
    app.MapControllers();

    Console.WriteLine("Starting AppInsightsMCP in Web API mode...");
    Console.WriteLine($"Swagger UI available at: {app.Urls.FirstOrDefault() ?? "http://localhost:5050"}");
    Console.WriteLine($"API endpoints available at: {app.Urls.FirstOrDefault() ?? "http://localhost:5050"}/api/logquery");

    app.Run();
}
else
{
    // MCP mode
    var builder = Host.CreateApplicationBuilder(args);

    var appInsightsSettings = builder.Configuration.GetSection("AppInsights").Get<AppInsights>();

    builder.Services.AddSingleton(_ =>
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("https://api.applicationinsights.io/") };
        httpClient.DefaultRequestHeaders.Add("X-API-Key", appInsightsSettings?.ApiKey);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    });

    builder.Services.AddSingleton<ILogQueryService>(provider =>
    {
        var httpClient = provider.GetRequiredService<HttpClient>();
        return new LogQueryService(httpClient, appInsightsSettings?.ApplicationId ?? string.Empty);
    });

    builder.Services.AddMcpServer(o =>
    {
        o.ServerInfo = new Implementation { Name = "AppInsightsMCP", Version = "1.0.0" };
    })
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

    var app = builder.Build();

    Console.WriteLine("Starting AppInsightsMCP in MCP mode...");
    app.Run();
}
