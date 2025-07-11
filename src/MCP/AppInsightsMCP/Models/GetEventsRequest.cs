using System.ComponentModel.DataAnnotations;

namespace AppInsightsMCP.Models
{
    /// <summary>
    /// Request model for getting events of a specific type
    /// </summary>
    public class GetEventsRequest
    {
        /// <summary>
        /// The type of events to retrieve (e.g., "customEvents", "pageViews")
        /// </summary>
        [Required]
        public string EventType { get; set; } = string.Empty;

        /// <summary>
        /// Optional timespan for the events (e.g., "PT1H" for past 1 hour)
        /// </summary>
        public string? Timespan { get; set; }

        /// <summary>
        /// Optional maximum number of events to return
        /// </summary>
        [Range(1, 10000)]
        public int? Top { get; set; }
    }
}
