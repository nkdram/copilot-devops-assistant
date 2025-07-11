using System.Net.Http.Headers;
using AppInsightsMCP.Service;
using AppInsightsMCP.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

var builder = Host.CreateApplicationBuilder(args);
// builder.Logging.ClearProviders();

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
app.Run();
