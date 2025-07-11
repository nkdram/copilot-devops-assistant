using System.Text;
using System.Text.Json;

namespace AppInsightsMCP.Service
{
    public class LogQueryService : ILogQueryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiVersion;
        private readonly string _applicationId;

        public LogQueryService(HttpClient httpClient, string applicationId, string apiVersion = "v1")
        {
            _httpClient = httpClient;
            _applicationId = applicationId;
            _apiVersion = apiVersion;
        }

        public async Task<JsonElement> ExecuteQueryAsync(string query, string? timespan = null)
        {
            var uri = $"{_apiVersion}/apps/{_applicationId}/query";
            var requestBody = new
            {
                query = query,
                timespan = timespan
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> GetApplicationInfoAsync()
        {
            var uri = $"{_apiVersion}/apps/{_applicationId}";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetMetricAsync(string metricName, string? timespan = null, string? aggregation = null)
        {
            var uri = $"{_apiVersion}/apps/{_applicationId}/metrics/{metricName}";
            var queryParams = new List<string>();
            
            if (!string.IsNullOrEmpty(timespan))
                queryParams.Add($"timespan={Uri.EscapeDataString(timespan)}");
            
            if (!string.IsNullOrEmpty(aggregation))
                queryParams.Add($"aggregation={Uri.EscapeDataString(aggregation)}");

            if (queryParams.Any())
                uri += "?" + string.Join("&", queryParams);

            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetEventsAsync(string eventType, string? timespan = null, int? top = null)
        {
            var uri = $"{_apiVersion}/apps/{_applicationId}/events/{eventType}";
            var queryParams = new List<string>();
            
            if (!string.IsNullOrEmpty(timespan))
                queryParams.Add($"timespan={Uri.EscapeDataString(timespan)}");
            
            if (top.HasValue)
                queryParams.Add($"$top={top}");

            if (queryParams.Any())
                uri += "?" + string.Join("&", queryParams);

            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetExceptionsAsync(string? timespan = null, int? top = null)
        {
            var query = "exceptions";
            
            if (top.HasValue)
                query += $" | limit {top}";

            return await ExecuteQueryAsync(query, timespan);
        }

        public async Task<JsonElement> GetRequestsAsync(string? timespan = null, int? top = null)
        {
            var query = "requests";
            
            if (top.HasValue)
                query += $" | limit {top}";

            return await ExecuteQueryAsync(query, timespan);
        }

        public async Task<JsonElement> GetDependenciesAsync(string? timespan = null, int? top = null)
        {
            var query = "dependencies";
            
            if (top.HasValue)
                query += $" | limit {top}";

            return await ExecuteQueryAsync(query, timespan);
        }

        public async Task<JsonElement> GetTracesAsync(string? timespan = null, int? top = null)
        {
            var query = "traces";
            
            if (top.HasValue)
                query += $" | limit {top}";

            return await ExecuteQueryAsync(query, timespan);
        }

        public async Task<JsonElement> GetPerformanceCountersAsync(string? timespan = null, int? top = null)
        {
            var query = "performanceCounters";
            
            if (top.HasValue)
                query += $" | limit {top}";

            return await ExecuteQueryAsync(query, timespan);
        }
    }
}
