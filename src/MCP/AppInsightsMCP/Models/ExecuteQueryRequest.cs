using System.ComponentModel.DataAnnotations;

namespace AppInsightsMCP.Models
{
    /// <summary>
    /// Request model for executing a KQL query
    /// </summary>
    public class ExecuteQueryRequest
    {
        /// <summary>
        /// The KQL query to execute
        /// </summary>
        [Required]
        public string Query { get; set; } = string.Empty;

        /// <summary>
        /// Optional timespan for the query (e.g., "PT1H" for past 1 hour)
        /// </summary>
        public string? Timespan { get; set; }
    }
}
