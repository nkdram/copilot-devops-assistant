using AppInsightsMCP.Service;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace AppInsightsMCP.Tools
{
    [McpServerToolType]
    public static class LogQueryTool
    {
        [McpServerTool(Name = "executeQuery")]
        [Description("Executes a KQL query against Application Insights")]
        public static async Task<JsonElement> ExecuteQueryAsync(
            ILogQueryService service,
            string query,
            string? timespan = null)
        {
            return await service.ExecuteQueryAsync(query, timespan);
        }

        [McpServerTool(Name = "getApplicationInfo")]
        [Description("Gets application information from Application Insights")]
        public static async Task<JsonElement> GetApplicationInfoAsync(
            ILogQueryService service)
        {
            return await service.GetApplicationInfoAsync();
        }

        [McpServerTool(Name = "getMetric")]
        [Description("Gets a specific metric from Application Insights")]
        public static async Task<JsonElement> GetMetricAsync(
            ILogQueryService service,
            string metricName,
            string? timespan = null,
            string? aggregation = null)
        {
            return await service.GetMetricAsync(metricName, timespan, aggregation);
        }

        [McpServerTool(Name = "getEvents")]
        [Description("Gets events of a specific type from Application Insights")]
        public static async Task<JsonElement> GetEventsAsync(
            ILogQueryService service,
            string eventType,
            string? timespan = null,
            int? top = null)
        {
            return await service.GetEventsAsync(eventType, timespan, top);
        }

        [McpServerTool(Name = "getExceptions")]
        [Description("Gets exceptions from Application Insights")]
        public static async Task<JsonElement> GetExceptionsAsync(
            ILogQueryService service,
            string? timespan = null,
            int? top = null)
        {
            return await service.GetExceptionsAsync(timespan, top);
        }

        [McpServerTool(Name = "getRequests")]
        [Description("Gets HTTP requests from Application Insights")]
        public static async Task<JsonElement> GetRequestsAsync(
            ILogQueryService service,
            string? timespan = null,
            int? top = null)
        {
            return await service.GetRequestsAsync(timespan, top);
        }

        [McpServerTool(Name = "getDependencies")]
        [Description("Gets dependency calls from Application Insights")]
        public static async Task<JsonElement> GetDependenciesAsync(
            ILogQueryService service,
            string? timespan = null,
            int? top = null)
        {
            return await service.GetDependenciesAsync(timespan, top);
        }

        [McpServerTool(Name = "getTraces")]
        [Description("Gets traces/logs from Application Insights")]
        public static async Task<JsonElement> GetTracesAsync(
            ILogQueryService service,
            string? timespan = null,
            int? top = null)
        {
            return await service.GetTracesAsync(timespan, top);
        }

        [McpServerTool(Name = "getPerformanceCounters")]
        [Description("Gets performance counters from Application Insights")]
        public static async Task<JsonElement> GetPerformanceCountersAsync(
            ILogQueryService service,
            string? timespan = null,
            int? top = null)
        {
            return await service.GetPerformanceCountersAsync(timespan, top);
        }
    }
}
