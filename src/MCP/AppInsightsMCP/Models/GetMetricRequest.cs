using System.ComponentModel.DataAnnotations;

namespace AppInsightsMCP.Models
{
    /// <summary>
    /// Request model for getting a specific metric
    /// </summary>
    public class GetMetricRequest
    {
        /// <summary>
        /// The name of the metric to retrieve
        /// </summary>
        [Required]
        public string MetricName { get; set; } = string.Empty;

        /// <summary>
        /// Optional timespan for the metric (e.g., "PT1H" for past 1 hour)
        /// </summary>
        public string? Timespan { get; set; }

        /// <summary>
        /// Optional aggregation method (e.g., "avg", "sum", "count")
        /// </summary>
        public string? Aggregation { get; set; }
    }
}
