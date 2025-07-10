using DevopsMCP.Service;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace DevopsMCP.Tools
{
    [McpServerToolType]
    public static class WorkItemTool
    {
        [McpServerTool(Name = "createWorkItems")]
        [Description("Creates a work item")]
        public static Task<int> CreateWorkItemAsync(
            IWorkItemService service,
            string project,
            string type,
            Dictionary<string, object> fields)
            => service.CreateWorkItemAsync(project, type, fields);

        [McpServerTool(Name = "getWorkItem")]
        [Description("Gets a work item by its ID")]
        public static Task<string> GetWorkItemAsync(
            IWorkItemService service,
            int id)
            => service.GetWorkItemAsync(id);

        [McpServerTool(Name = "updateWorkItem")]
        [Description("Updates a work item by its ID")]
        public static Task<bool> UpdateWorkItemAsync(
            IWorkItemService service,
            int id,
            Dictionary<string, object> fields)
            => service.UpdateWorkItemAsync(id, fields);

        [McpServerTool(Name = "deleteWorkItem")]
        [Description("Deletes a work item by its ID")]
        public static Task<bool> DeleteWorkItemAsync(
            IWorkItemService service,
            int id)
            => service.DeleteWorkItemAsync(id);

        [McpServerTool(Name = "getTags")]
        [Description("Gets tags for a work item")]
        public static Task<IReadOnlyList<string>> GetTagsAsync(
            IWorkItemService service,
            int id)
            => service.GetTagsAsync(id);

        [McpServerTool(Name = "addTags")]
        [Description("Adds tags to a work item")]
        public static Task<bool> AddTagsAsync(
            IWorkItemService service,
            int id,
            IEnumerable<string> tags)
            => service.AddTagsAsync(id, tags);

        [McpServerTool(Name = "removeTags")]
        [Description("Removes tags from a work item")]
        public static Task<bool> RemoveTagsAsync(
            IWorkItemService service,
            int id,
            IEnumerable<string> tags)
            => service.RemoveTagsAsync(id, tags);

        [McpServerTool(Name = "getComments")]
        [Description("Gets comments for a work item")]
        public static Task<IReadOnlyList<string>> GetCommentsAsync(
            IWorkItemService service,
            int id)
            => service.GetCommentsAsync(id);

        [McpServerTool(Name = "addComment")]
        [Description("Adds a comment to a work item")]
        public static Task<bool> AddCommentAsync(
            IWorkItemService service,
            int id,
            string comment)
            => service.AddCommentAsync(id, comment);
    }
}
