using System.Text;
using System.Text.Json;

namespace DevopsMCP.Service
{
    public class WorkItemService : IWorkItemService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseAddress;
        private readonly string _apiVersion;

        public WorkItemService(HttpClient httpClient, string apiBaseAddress, string apiVersion = "7.1-preview.3")
        {
            _httpClient = httpClient;
            _apiBaseAddress = apiBaseAddress.TrimEnd('/');
            _apiVersion = apiVersion;
        }

        public async Task<int> CreateWorkItemAsync(string project, string type, Dictionary<string, object> fields)
        {
            var uri = $"{_apiBaseAddress}/{project}/_apis/wit/workitems/${type}?api-version={_apiVersion}";
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
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("id").GetInt32();
        }

        public async Task<string> GetWorkItemAsync(int id)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var json = await _httpClient.GetFromJsonAsync<JsonElement>(uri);
            return json.ToString();
        }

        public async Task<bool> UpdateWorkItemAsync(int id, Dictionary<string, object> fields)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}?api-version={_apiVersion}";
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
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteWorkItemAsync(int id)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var response = await _httpClient.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }

        public async Task<IReadOnlyList<string>> GetTagsAsync(int id)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}/tags?api-version={_apiVersion}";
            var json = await _httpClient.GetFromJsonAsync<JsonElement>(uri);
            // Deserialize tags from json
            return JsonSerializer.Deserialize<List<string>>(json.GetProperty("value").GetRawText());
        }

        public async Task<bool> AddTagsAsync(int id, IEnumerable<string> tags)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var patchDocument = new List<object>
            {
                new
                {
                    op = "add",
                    path = "/fields/System.Tags",
                    value = string.Join(",", tags)
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(patchDocument), Encoding.UTF8, "application/json-patch+json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveTagsAsync(int id, IEnumerable<string> tags)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}?api-version={_apiVersion}";
            var patchDocument = new List<object>
            {
                new
                {
                    op = "remove",
                    path = $"/fields/System.Tags",
                    value = string.Join(",", tags)
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(patchDocument), Encoding.UTF8, "application/json-patch+json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<IReadOnlyList<string>> GetCommentsAsync(int id)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}/comments?api-version={_apiVersion}";
            var json = await _httpClient.GetFromJsonAsync<JsonElement>(uri);
            // Deserialize comments from json
            return JsonSerializer.Deserialize<List<string>>(json.GetProperty("comments").GetRawText());
        }

        public async Task<bool> AddCommentAsync(int id, string comment)
        {
            var uri = $"{_apiBaseAddress}/_apis/wit/workitems/{id}/comments?api-version={_apiVersion}";
            var content = new StringContent(JsonSerializer.Serialize(new { comment }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }
    }
}