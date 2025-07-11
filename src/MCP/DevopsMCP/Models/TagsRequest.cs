using System.ComponentModel.DataAnnotations;

namespace DevopsMCP.Models
{
    /// <summary>
    /// Request model for tag operations
    /// </summary>
    public class TagsRequest
    {
        /// <summary>
        /// List of tags
        /// </summary>
        [Required]
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
