using DevopsMCP.Models;
using DevopsMCP.Service;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace DevopsMCP.Tools
{
    [McpServerToolType]
    public static class RepositoryTool
    {
        [McpServerTool(Name = "getRepositories")]
        [Description("Gets all repositories in a project")]
        public static async Task<JsonElement> GetRepositories(IRepositoryService service, string project)
        {
            return await service.GetRepositoriesAsync(project);
        }

        [McpServerTool(Name = "getRepository")]
        [Description("Gets a specific repository by ID")]
        public static async Task<JsonElement> GetRepository(IRepositoryService service, string repositoryId, string? project = null)
        {
            return await service.GetRepositoryAsync(repositoryId, project);
        }

        [McpServerTool(Name = "getFileContent")]
        [Description("Gets file content from a repository using Azure DevOps full file path (e.g., from work item links)")]
        public static async Task<JsonElement> GetFileContent(
            IRepositoryService service,
            string repositoryId,
            string path,
            string? branch = null,
            string? project = null)
        {
            return await service.GetFileContentAsync(repositoryId, path, branch, project);
        }

        [McpServerTool(Name = "getItemMetadata")]
        [Description("Gets metadata for a file or folder in a repository")]
        public static async Task<JsonElement> GetItemMetadata(
            IRepositoryService service,
            string repositoryId,
            string path,
            string? branch = null,
            string? project = null)
        {
            return await service.GetItemMetadataAsync(repositoryId, path, branch, project);
        }

        [McpServerTool(Name = "getFolderContents")]
        [Description("Gets contents of a folder in a repository")]
        public static async Task<JsonElement> GetFolderContents(
            IRepositoryService service,
            string repositoryId,
            string path,
            string? branch = null,
            string? project = null)
        {
            return await service.GetFolderContentsAsync(repositoryId, path, branch, project);
        }

        [McpServerTool(Name = "getBranches")]
        [Description("Gets all branches in a repository")]
        public static async Task<JsonElement> GetBranches(IRepositoryService service, string repositoryId, string? project = null)
        {
            return await service.GetBranchesAsync(repositoryId, project);
        }

        [McpServerTool(Name = "createBranch")]
        [Description("Creates a new branch in a repository")]
        public static async Task<JsonElement> CreateBranch(
            IRepositoryService service,
            string repositoryId,
            string branchName,
            string sourceBranch,
            string? project = null)
        {
            return await service.CreateBranchAsync(repositoryId, branchName, sourceBranch, project);
        }

        [McpServerTool(Name = "createCommit")]
        [Description("Creates a commit with file changes in a repository branch")]
        public static async Task<JsonElement> CreateCommit(
            IRepositoryService service,
            string repositoryId,
            string branchName,
            string commitMessage,
            List<FileChangeRequest> changes,
            string? project = null)
        {
            var fileChanges = changes.Select(c => new FileChange
            {
                Path = c.Path,
                ChangeType = c.ChangeType,
                Content = c.Content,
                Encoding = c.Encoding ?? "base64"
            }).ToList();

            return await service.CreateCommitAsync(repositoryId, branchName, fileChanges, commitMessage, project);
        }

        [McpServerTool(Name = "createPullRequest")]
        [Description("Creates a pull request in a repository")]
        public static async Task<JsonElement> CreatePullRequest(
            IRepositoryService service,
            string repositoryId,
            string sourceBranch,
            string targetBranch,
            string title,
            string? description = null,
            string? project = null)
        {
            return await service.CreatePullRequestAsync(repositoryId, sourceBranch, targetBranch, title, description, project);
        }

        [McpServerTool(Name = "getPullRequest")]
        [Description("Gets a specific pull request by ID")]
        public static async Task<JsonElement> GetPullRequest(IRepositoryService service, string repositoryId, int pullRequestId, string? project = null)
        {
            return await service.GetPullRequestAsync(repositoryId, pullRequestId, project);
        }

        [McpServerTool(Name = "updatePullRequest")]
        [Description("Updates a pull request (e.g., change status, add reviewers)")]
        public static async Task<JsonElement> UpdatePullRequest(
            IRepositoryService service,
            string repositoryId,
            int pullRequestId,
            Dictionary<string, object> updates,
            string? project = null)
        {
            return await service.UpdatePullRequestAsync(repositoryId, pullRequestId, updates, project);
        }

        [McpServerTool(Name = "getPullRequests")]
        [Description("Gets pull requests in a repository with optional status filter")]
        public static async Task<JsonElement> GetPullRequests(
            IRepositoryService service,
            string repositoryId,
            string? status = null,
            string? project = null)
        {
            return await service.GetPullRequestsAsync(repositoryId, status, project);
        }
    }
}
