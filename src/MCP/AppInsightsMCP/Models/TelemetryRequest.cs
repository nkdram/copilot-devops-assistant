using System.ComponentModel.DataAnnotations;

namespace AppInsightsMCP.Models
{
    /// <summary>
    /// Request model for getting telemetry data with timespan and top parameters
    /// </summary>
    public class TelemetryRequest
    {
        /// <summary>
        /// Optional timespan for the data (e.g., "PT1H" for past 1 hour)
        /// </summary>
        public string? Timespan { get; set; }

        /// <summary>
        /// Optional maximum number of records to return
        /// </summary>
        [Range(1, 10000)]
        public int? Top { get; set; }
    }
}
