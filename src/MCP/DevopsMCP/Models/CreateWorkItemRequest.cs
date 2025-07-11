using System.ComponentModel.DataAnnotations;

namespace DevopsMCP.Models
{
    /// <summary>
    /// Request model for creating a work item
    /// </summary>
    public class CreateWorkItemRequest
    {
        /// <summary>
        /// The project name
        /// </summary>
        [Required]
        public string Project { get; set; } = string.Empty;

        /// <summary>
        /// The work item type (e.g., Bug, Task, User Story)
        /// </summary>
        [Required]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The work item fields
        /// </summary>
        [Required]
        public Dictionary<string, object> Fields { get; set; } = new();
    }
}
