using System.ComponentModel.DataAnnotations;

namespace DevopsMCP.Models
{
    /// <summary>
    /// Request model for updating a work item
    /// </summary>
    public class UpdateWorkItemRequest
    {
        /// <summary>
        /// The work item fields to update
        /// </summary>
        [Required]
        public Dictionary<string, object> Fields { get; set; } = new();
    }
}
