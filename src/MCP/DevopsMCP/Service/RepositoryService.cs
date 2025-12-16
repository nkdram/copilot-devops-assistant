using System.Text;
using System.Text.Json;

namespace DevopsMCP.Service
{
    public class RepositoryService : IRepositoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _gitApiVersion;
        private readonly string _prApiVersion;

        public RepositoryService(HttpClient httpClient, string gitApiVersion = "7.1", string prApiVersion = "7.1")
        {
            _httpClient = httpClient;
            _gitApiVersion = gitApiVersion;
            _prApiVersion = prApiVersion;
        }

        public async Task<JsonElement> GetRepositoriesAsync(string project)
        {
            var uri = $"{project}/_apis/git/repositories?api-version={_gitApiVersion}";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetRepositoryAsync(string repositoryId, string? project = null)
        {
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}?api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}?api-version={_gitApiVersion}";
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetFileContentAsync(string repositoryId, string path, string? branch = null, string? project = null)
        {
            var versionDescriptor = branch != null ? $"&versionDescriptor.version={branch}&versionDescriptor.versionType=branch" : "";
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/items?path={Uri.EscapeDataString(path)}&includeContent=true{versionDescriptor}&api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/items?path={Uri.EscapeDataString(path)}&includeContent=true{versionDescriptor}&api-version={_gitApiVersion}";
            
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetItemMetadataAsync(string repositoryId, string path, string? branch = null, string? project = null)
        {
            var versionDescriptor = branch != null ? $"&versionDescriptor.version={branch}&versionDescriptor.versionType=branch" : "";
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/items?path={Uri.EscapeDataString(path)}{versionDescriptor}&api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/items?path={Uri.EscapeDataString(path)}{versionDescriptor}&api-version={_gitApiVersion}";
            
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetFolderContentsAsync(string repositoryId, string path, string? branch = null, string? project = null)
        {
            var versionDescriptor = branch != null ? $"&versionDescriptor.version={branch}&versionDescriptor.versionType=branch" : "";
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/items?scopePath={Uri.EscapeDataString(path)}{versionDescriptor}&api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/items?scopePath={Uri.EscapeDataString(path)}{versionDescriptor}&api-version={_gitApiVersion}";
            
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> GetBranchesAsync(string repositoryId, string? project = null)
        {
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/refs?filter=heads/&api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/refs?filter=heads/&api-version={_gitApiVersion}";
            
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> CreateBranchAsync(string repositoryId, string branchName, string sourceBranch, string? project = null)
        {
            // First get the source branch commit ID
            var getBranchUri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/refs?filter=heads/{sourceBranch}&api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/refs?filter=heads/{sourceBranch}&api-version={_gitApiVersion}";
            
            var branchResult = await _httpClient.GetFromJsonAsync<JsonElement>(getBranchUri);
            
            if (!branchResult.TryGetProperty("value", out var branches) || branches.GetArrayLength() == 0)
            {
                throw new Exception($"Source branch '{sourceBranch}' not found");
            }

            var sourceCommitId = branches[0].GetProperty("objectId").GetString();

            // Create new branch
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/refs?api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/refs?api-version={_gitApiVersion}";

            var payload = new[]
            {
                new
                {
                    name = $"refs/heads/{branchName}",
                    oldObjectId = "0000000000000000000000000000000000000000",
                    newObjectId = sourceCommitId
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> CreateCommitAsync(string repositoryId, string branchName, List<FileChange> changes, string commitMessage, string? project = null)
        {
            // Get the latest commit on the branch
            var getBranchUri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/refs?filter=heads/{branchName}&api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/refs?filter=heads/{branchName}&api-version={_gitApiVersion}";
            
            var branchResult = await _httpClient.GetFromJsonAsync<JsonElement>(getBranchUri);
            
            if (!branchResult.TryGetProperty("value", out var branches) || branches.GetArrayLength() == 0)
            {
                throw new Exception($"Branch '{branchName}' not found");
            }

            var oldCommitId = branches[0].GetProperty("objectId").GetString();

            // Prepare push payload
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/pushes?api-version={_gitApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/pushes?api-version={_gitApiVersion}";

            var changesList = changes.Select(c =>
            {
                var changeObj = new Dictionary<string, object>
                {
                    ["changeType"] = c.ChangeType,
                    ["item"] = new { path = c.Path }
                };

                if (c.Content != null)
                {
                    changeObj["newContent"] = new
                    {
                        content = c.Content,
                        contentType = c.Encoding == "base64" ? "base64encoded" : "rawtext"
                    };
                }

                return changeObj;
            }).ToList();

            var payload = new
            {
                refUpdates = new[]
                {
                    new
                    {
                        name = $"refs/heads/{branchName}",
                        oldObjectId = oldCommitId
                    }
                },
                commits = new[]
                {
                    new
                    {
                        comment = commitMessage,
                        changes = changesList
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> CreatePullRequestAsync(string repositoryId, string sourceBranch, string targetBranch, string title, string? description = null, string? project = null)
        {
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/pullrequests?api-version={_prApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/pullrequests?api-version={_prApiVersion}";

            var payload = new
            {
                sourceRefName = $"refs/heads/{sourceBranch}",
                targetRefName = $"refs/heads/{targetBranch}",
                title = title,
                description = description ?? ""
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> GetPullRequestAsync(string repositoryId, int pullRequestId, string? project = null)
        {
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/pullrequests/{pullRequestId}?api-version={_prApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/pullrequests/{pullRequestId}?api-version={_prApiVersion}";
            
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }

        public async Task<JsonElement> UpdatePullRequestAsync(string repositoryId, int pullRequestId, Dictionary<string, object> updates, string? project = null)
        {
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/pullrequests/{pullRequestId}?api-version={_prApiVersion}"
                : $"_apis/git/repositories/{repositoryId}/pullrequests/{pullRequestId}?api-version={_prApiVersion}";

            var content = new StringContent(JsonSerializer.Serialize(updates), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }

        public async Task<JsonElement> GetPullRequestsAsync(string repositoryId, string? status = null, string? project = null)
        {
            var statusFilter = status != null ? $"&searchCriteria.status={status}" : "";
            var uri = project != null
                ? $"{project}/_apis/git/repositories/{repositoryId}/pullrequests?api-version={_prApiVersion}{statusFilter}"
                : $"_apis/git/repositories/{repositoryId}/pullrequests?api-version={_prApiVersion}{statusFilter}";
            
            return await _httpClient.GetFromJsonAsync<JsonElement>(uri);
        }
    }
}
