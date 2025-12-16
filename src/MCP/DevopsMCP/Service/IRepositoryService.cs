using System.Text.Json;

namespace DevopsMCP.Service
{
    public interface IRepositoryService
    {
        // Repository operations
        Task<JsonElement> GetRepositoriesAsync(string project);
        Task<JsonElement> GetRepositoryAsync(string repositoryId, string? project = null);
        
        // File operations
        Task<JsonElement> GetFileContentAsync(string repositoryId, string path, string? branch = null, string? project = null);
        Task<JsonElement> GetItemMetadataAsync(string repositoryId, string path, string? branch = null, string? project = null);
        Task<JsonElement> GetFolderContentsAsync(string repositoryId, string path, string? branch = null, string? project = null);
        
        // Branch operations
        Task<JsonElement> GetBranchesAsync(string repositoryId, string? project = null);
        Task<JsonElement> CreateBranchAsync(string repositoryId, string branchName, string sourceBranch, string? project = null);
        
        // Commit operations
        Task<JsonElement> CreateCommitAsync(string repositoryId, string branchName, List<FileChange> changes, string commitMessage, string? project = null);
        
        // Pull Request operations
        Task<JsonElement> CreatePullRequestAsync(string repositoryId, string sourceBranch, string targetBranch, string title, string? description = null, string? project = null);
        Task<JsonElement> GetPullRequestAsync(string repositoryId, int pullRequestId, string? project = null);
        Task<JsonElement> UpdatePullRequestAsync(string repositoryId, int pullRequestId, Dictionary<string, object> updates, string? project = null);
        Task<JsonElement> GetPullRequestsAsync(string repositoryId, string? status = null, string? project = null);
    }

    public class FileChange
    {
        public string Path { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty; // add, edit, delete
        public string? Content { get; set; }
        public string? Encoding { get; set; } = "base64"; // base64 or raw
    }
}
