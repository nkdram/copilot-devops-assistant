using DevopsMCP.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevopsMCP.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryService _repositoryService;

        public RepositoryController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet("repositories/{project}")]
        public async Task<JsonElement> GetRepositories(string project)
        {
            return await _repositoryService.GetRepositoriesAsync(project);
        }

        [HttpGet("repository/{repositoryId}")]
        public async Task<JsonElement> GetRepository(string repositoryId, [FromQuery] string? project = null)
        {
            return await _repositoryService.GetRepositoryAsync(repositoryId, project);
        }

        [HttpGet("file")]
        public async Task<JsonElement> GetFileContent(
            [FromQuery] string repositoryId,
            [FromQuery] string path,
            [FromQuery] string? branch = null,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.GetFileContentAsync(repositoryId, path, branch, project);
        }

        [HttpGet("item/metadata")]
        public async Task<JsonElement> GetItemMetadata(
            [FromQuery] string repositoryId,
            [FromQuery] string path,
            [FromQuery] string? branch = null,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.GetItemMetadataAsync(repositoryId, path, branch, project);
        }

        [HttpGet("folder")]
        public async Task<JsonElement> GetFolderContents(
            [FromQuery] string repositoryId,
            [FromQuery] string path,
            [FromQuery] string? branch = null,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.GetFolderContentsAsync(repositoryId, path, branch, project);
        }

        [HttpGet("branches/{repositoryId}")]
        public async Task<JsonElement> GetBranches(string repositoryId, [FromQuery] string? project = null)
        {
            return await _repositoryService.GetBranchesAsync(repositoryId, project);
        }

        [HttpPost("branch")]
        public async Task<JsonElement> CreateBranch(
            [FromQuery] string repositoryId,
            [FromQuery] string branchName,
            [FromQuery] string sourceBranch,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.CreateBranchAsync(repositoryId, branchName, sourceBranch, project);
        }

        [HttpPost("commit")]
        public async Task<JsonElement> CreateCommit(
            [FromQuery] string repositoryId,
            [FromQuery] string branchName,
            [FromQuery] string commitMessage,
            [FromBody] List<FileChange> changes,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.CreateCommitAsync(repositoryId, branchName, changes, commitMessage, project);
        }

        [HttpPost("pullrequest")]
        public async Task<JsonElement> CreatePullRequest(
            [FromQuery] string repositoryId,
            [FromQuery] string sourceBranch,
            [FromQuery] string targetBranch,
            [FromQuery] string title,
            [FromQuery] string? description = null,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.CreatePullRequestAsync(repositoryId, sourceBranch, targetBranch, title, description, project);
        }

        [HttpGet("pullrequest/{repositoryId}/{pullRequestId}")]
        public async Task<JsonElement> GetPullRequest(string repositoryId, int pullRequestId, [FromQuery] string? project = null)
        {
            return await _repositoryService.GetPullRequestAsync(repositoryId, pullRequestId, project);
        }

        [HttpPatch("pullrequest/{repositoryId}/{pullRequestId}")]
        public async Task<JsonElement> UpdatePullRequest(
            string repositoryId,
            int pullRequestId,
            [FromBody] Dictionary<string, object> updates,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.UpdatePullRequestAsync(repositoryId, pullRequestId, updates, project);
        }

        [HttpGet("pullrequests/{repositoryId}")]
        public async Task<JsonElement> GetPullRequests(
            string repositoryId,
            [FromQuery] string? status = null,
            [FromQuery] string? project = null)
        {
            return await _repositoryService.GetPullRequestsAsync(repositoryId, status, project);
        }
    }
}
