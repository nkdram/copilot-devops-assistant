namespace DevopsMCP.Service
{
    public interface IWorkItemService
    {
        Task<int> CreateWorkItemAsync(string project, string type, Dictionary<string, object> fields);
        Task<string> GetWorkItemAsync(int id);
        Task<bool> UpdateWorkItemAsync(int id, Dictionary<string, object> fields);
        Task<bool> DeleteWorkItemAsync(int id);

        // Tags
        Task<IReadOnlyList<string>> GetTagsAsync(int id);
        Task<bool> AddTagsAsync(int id, IEnumerable<string> tags);
        Task<bool> RemoveTagsAsync(int id, IEnumerable<string> tags);

        // Comments
        Task<IReadOnlyList<string>> GetCommentsAsync(int id);
        Task<bool> AddCommentAsync(int id, string comment);
    }
}