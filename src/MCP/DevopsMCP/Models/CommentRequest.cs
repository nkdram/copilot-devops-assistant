using System.ComponentModel.DataAnnotations;

namespace DevopsMCP.Models
{
    /// <summary>
    /// Request model for adding a comment
    /// </summary>
    public class CommentRequest
    {
        /// <summary>
        /// The comment text
        /// </summary>
        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
