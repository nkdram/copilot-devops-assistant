using System.Text.Json;

namespace AppInsightsMCP.Service
{
    public interface ILogQueryService
    {
        Task<JsonElement> ExecuteQueryAsync(string query, string? timespan = null);
        Task<JsonElement> GetApplicationInfoAsync();
        Task<JsonElement> GetMetricAsync(string metricName, string? timespan = null, string? aggregation = null);
        Task<JsonElement> GetEventsAsync(string eventType, string? timespan = null, int? top = null);
        Task<JsonElement> GetExceptionsAsync(string? timespan = null, int? top = null);
        Task<JsonElement> GetRequestsAsync(string? timespan = null, int? top = null);
        Task<JsonElement> GetDependenciesAsync(string? timespan = null, int? top = null);
        Task<JsonElement> GetTracesAsync(string? timespan = null, int? top = null);
        Task<JsonElement> GetPerformanceCountersAsync(string? timespan = null, int? top = null);
    }
}
