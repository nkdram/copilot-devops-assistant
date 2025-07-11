using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DevopsMCP.Service;
using DevopsMCP.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

var builder = Host.CreateApplicationBuilder(args);
// builder.Logging.ClearProviders();

var adoSettings = builder.Configuration.GetSection("AzureDevops").Get<AzureDevops>();

builder.Services.AddSingleton(_ =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri($"https://dev.azure.com/{adoSettings?.Organization}/{adoSettings?.Project}/") };
    var token = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{adoSettings?.PersonalAccessToken}"));
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    return httpClient;
});

builder.Services.AddSingleton<IWorkItemService, WorkItemService>();

builder.Services.AddMcpServer(o =>
{
    o.ServerInfo = new Implementation { Name = "DevopMCP", Version = "1.0.0" };
})
.WithStdioServerTransport()
.WithToolsFromAssembly();

var app = builder.Build();
app.Run();
