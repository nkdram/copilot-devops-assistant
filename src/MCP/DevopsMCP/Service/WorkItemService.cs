using System.Text;
using System.Text.Json;

namespace DevopsMCP.Service
{
    public class WorkItemService : IWorkItemService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiVersion;

        public WorkItemService(HttpClient httpClient, string apiVersion = "7.1-preview.3")
        {
            _httpClient = httpClient;
            _apiVersion = apiVersion;
        }

        public async Task<JsonElement> GetWorkItemAsync(int id)
        {
            var uri = $"_apis/wit/workitems/{id}?api-version={_apiVersion}";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> CreateWorkItemAsync(string project, string type, Dictionary<string, object> fields)
        {
            var encodedType = Uri.EscapeDataString(type);
            var uri = $"_apis/wit/workitems/${encodedType}?api-version={_apiVersion}";
            var patchDocument = new List<object>();
            foreach (var field in fields)
            {
                patchDocument.Add(new
                {
                    op = "add",
                    path = $"/fields/{field.Key}",
                    value = field.Value
                });
            }

            var content = new StringContent(JsonSerializer.Serialize(patchDocument), Encoding.UTF8, "application/json-patch+json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> UpdateWorkItemAsync(int id, Dictionary<string, object> fields)
        {
            var uri = $"_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var patchDocument = new List<object>();
            foreach (var field in fields)
            {
                patchDocument.Add(new
                {
                    op = "replace",
                    path = $"/fields/{field.Key}",
                    value = field.Value
                });
            }

            var content = new StringContent(JsonSerializer.Serialize(patchDocument), Encoding.UTF8, "application/json-patch+json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> DeleteWorkItemAsync(int id)
        {
            var uri = $"_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.SerializeToElement(new { success = true, id });
        }

        public async Task<JsonElement> GetTagsAsync(int id)
        {
            var uri = $"_apis/wit/workitems/{id}?$expand=all&api-version={_apiVersion}";
            var workItem = await _httpClient.GetFromJsonAsync<JsonElement>(uri);
            
            if (workItem.TryGetProperty("fields", out var fields) &&
                fields.TryGetProperty("System.Tags", out var tagsProperty))
            {
                var tagsString = tagsProperty.GetString();
                var tags = string.IsNullOrEmpty(tagsString) 
                    ? new List<string>() 
                    : tagsString.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                return JsonSerializer.SerializeToElement(tags);
            }
            
            return JsonSerializer.SerializeToElement(new List<string>());
        }

        public async Task<JsonElement> AddTagsAsync(int id, IEnumerable<string> tags)
        {
            // First get existing tags
            var workItem = await _httpClient.GetFromJsonAsync<JsonElement>($"_apis/wit/workitems/{id}?$expand=all&api-version={_apiVersion}");
            var existingTags = new List<string>();
            
            if (workItem.TryGetProperty("fields", out var fields) &&
                fields.TryGetProperty("System.Tags", out var tagsProperty))
            {
                var tagsString = tagsProperty.GetString();
                if (!string.IsNullOrEmpty(tagsString))
                {
                    existingTags = tagsString.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                }
            }

            // Add new tags
            var allTags = existingTags.Union(tags).ToList();
            var tagsValue = string.Join("; ", allTags);

            var uri = $"_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var patchDocument = new List<object>
            {
                new
                {
                    op = "replace",
                    path = "/fields/System.Tags",
                    value = tagsValue
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(patchDocument), Encoding.UTF8, "application/json-patch+json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.SerializeToElement(new { success = true, tags = allTags });
        }

        public async Task<JsonElement> RemoveTagsAsync(int id, IEnumerable<string> tagsToRemove)
        {
            // First get existing tags
            var workItem = await _httpClient.GetFromJsonAsync<JsonElement>($"_apis/wit/workitems/{id}?$expand=all&api-version={_apiVersion}");
            var existingTags = new List<string>();
            
            if (workItem.TryGetProperty("fields", out var fields) &&
                fields.TryGetProperty("System.Tags", out var tagsProperty))
            {
                var tagsString = tagsProperty.GetString();
                if (!string.IsNullOrEmpty(tagsString))
                {
                    existingTags = tagsString.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                }
            }

            // Remove specified tags
            var remainingTags = existingTags.Except(tagsToRemove).ToList();
            var tagsValue = string.Join("; ", remainingTags);

            var uri = $"_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var patchDocument = new List<object>
            {
                new
                {
                    op = "replace",
                    path = "/fields/System.Tags",
                    value = tagsValue
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(patchDocument), Encoding.UTF8, "application/json-patch+json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.SerializeToElement(new { success = true, tags = remainingTags });
        }

        public async Task<JsonElement> GetCommentsAsync(int id)
        {
            var uri = $"_apis/wit/workitems/{id}/comments?api-version={_apiVersion}";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> AddCommentAsync(int id, string comment)
        {
            var uri = $"_apis/wit/workitems/{id}/comments?api-version={_apiVersion}";
            var commentData = new { text = comment };
            var response = await _httpClient.PostAsJsonAsync(uri, commentData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }
    }
}