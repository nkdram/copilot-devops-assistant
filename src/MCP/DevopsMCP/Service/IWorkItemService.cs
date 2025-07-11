using System.Text.Json;

namespace DevopsMCP.Service
{
    public interface IWorkItemService
    {
        Task<JsonElement> CreateWorkItemAsync(string project, string type, Dictionary<string, object> fields);
        Task<JsonElement> GetWorkItemAsync(int id);
        Task<JsonElement> UpdateWorkItemAsync(int id, Dictionary<string, object> fields);
        Task<JsonElement> DeleteWorkItemAsync(int id);

        // Tags
        Task<JsonElement> GetTagsAsync(int id);
        Task<JsonElement> AddTagsAsync(int id, IEnumerable<string> tags);
        Task<JsonElement> RemoveTagsAsync(int id, IEnumerable<string> tags);

        // Comments
        Task<JsonElement> GetCommentsAsync(int id);
        Task<JsonElement> AddCommentAsync(int id, string comment);
    }
}