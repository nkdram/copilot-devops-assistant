using DevopsMCP.Service;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace DevopsMCP.Tools
{
    [McpServerToolType]
    public static class WorkItemTool
    {
        [McpServerTool(Name = "createWorkItems")]
        [Description("Creates a work item")]
        public static async Task<JsonElement> CreateWorkItemAsync(
            IWorkItemService service,
            string project,
            string type,
            Dictionary<string, object> fields)
        {
            return await service.CreateWorkItemAsync(project, type, fields);
        }

        [McpServerTool(Name = "getWorkItem")]
        [Description("Gets a work item by its ID")]
        public static async Task<JsonElement> GetWorkItemAsync(
            IWorkItemService service,
            int id)
        {
            return await service.GetWorkItemAsync(id);
        }

        [McpServerTool(Name = "updateWorkItem")]
        [Description("Updates a work item by its ID")]
        public static async Task<JsonElement> UpdateWorkItemAsync(
            IWorkItemService service,
            int id,
            Dictionary<string, object> fields)
        {
            return await service.UpdateWorkItemAsync(id, fields);
        }

        [McpServerTool(Name = "deleteWorkItem")]
        [Description("Deletes a work item by its ID")]
        public static async Task<JsonElement> DeleteWorkItemAsync(
            IWorkItemService service,
            int id)
        {
            return await service.DeleteWorkItemAsync(id);
        }

        [McpServerTool(Name = "getTags")]
        [Description("Gets tags for a work item")]
        public static async Task<JsonElement> GetTagsAsync(
            IWorkItemService service,
            int id)
        {
            return await service.GetTagsAsync(id);
        }

        [McpServerTool(Name = "addTags")]
        [Description("Adds tags to a work item")]
        public static async Task<JsonElement> AddTagsAsync(
            IWorkItemService service,
            int id,
            IEnumerable<string> tags)
        {
            return await service.AddTagsAsync(id, tags);
        }

        [McpServerTool(Name = "removeTags")]
        [Description("Removes tags from a work item")]
        public static async Task<JsonElement> RemoveTagsAsync(
            IWorkItemService service,
            int id,
            IEnumerable<string> tags)
        {
            return await service.RemoveTagsAsync(id, tags);
        }

        [McpServerTool(Name = "getComments")]
        [Description("Gets comments for a work item")]
        public static async Task<JsonElement> GetCommentsAsync(
            IWorkItemService service,
            int id)
        {
            return await service.GetCommentsAsync(id);
        }

        [McpServerTool(Name = "addComment")]
        [Description("Adds a comment to a work item")]
        public static async Task<JsonElement> AddCommentAsync(
            IWorkItemService service,
            int id,
            string comment)
        {
            return await service.AddCommentAsync(id, comment);
        }
    }
}
