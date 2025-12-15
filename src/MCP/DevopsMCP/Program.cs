using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DevopsMCP.Service;
using DevopsMCP.Settings;
using DevopsMCP.Tools;
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
    var adoSettings = builder.Configuration.GetSection("AzureDevops").Get<AzureDevops>();

    // Add Web API services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "DevopsMCP API",
            Version = "v1",
            Description = "REST API for Azure DevOps Work Item operations",           
        });
    });

    // Configure HttpClient for Azure DevOps
    builder.Services.AddSingleton(_ =>
    {
        var httpClient = new HttpClient { BaseAddress = new Uri($"https://dev.azure.com/{adoSettings?.Organization}/{adoSettings?.Project}/") };
        var token = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{adoSettings?.PersonalAccessToken}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    });

    // Register services
    builder.Services.AddSingleton<IWorkItemService, WorkItemService>();
    builder.Services.AddSingleton<ITestPlanService, TestPlanService>();

    var app = builder.Build();

    // Configure Web API pipeline
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevopsMCP API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });

    app.UseRouting();
    app.MapControllers();

    Console.WriteLine("Starting DevopsMCP in Web API mode...");
    Console.WriteLine($"Swagger UI available at: {app.Urls.FirstOrDefault() ?? "http://localhost:5000"}");
    Console.WriteLine($"API endpoints available at: {app.Urls.FirstOrDefault() ?? "http://localhost:5000"}/api/workitem");
    Console.WriteLine($"API endpoints available at: {app.Urls.FirstOrDefault() ?? "http://localhost:5000"}/api/testcase");

    app.Run();
}
else
{
    // MCP mode
    var builder = Host.CreateApplicationBuilder(args);

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
    builder.Services.AddSingleton<ITestPlanService, TestPlanService>();

    builder.Services.AddMcpServer(o =>
    {
        o.ServerInfo = new Implementation { Name = "DevopsMCP", Version = "1.0.0" };
    })
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

    var app = builder.Build();

    Console.WriteLine("Starting DevopsMCP in MCP mode...");
    app.Run();
}
