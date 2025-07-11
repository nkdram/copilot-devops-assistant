using AppInsightsMCP.Models;
using AppInsightsMCP.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AppInsightsMCP.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LogQueryController : ControllerBase
    {
        private readonly ILogQueryService _logQueryService;

        public LogQueryController(ILogQueryService logQueryService)
        {
            _logQueryService = logQueryService;
        }

        /// <summary>
        /// Executes a KQL query against Application Insights
        /// </summary>
        /// <param name="request">The query execution request</param>
        /// <returns>The query results</returns>
        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExecuteQuery([FromBody] ExecuteQueryRequest request)
        {
            try
            {
                var result = await _logQueryService.ExecuteQueryAsync(request.Query, request.Timespan);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets application information from Application Insights
        /// </summary>
        /// <returns>The application information</returns>
        [HttpGet("application-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicationInfo()
        {
            try
            {
                var result = await _logQueryService.GetApplicationInfoAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a specific metric from Application Insights
        /// </summary>
        /// <param name="request">The metric request</param>
        /// <returns>The metric data</returns>
        [HttpPost("metric")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMetric([FromBody] GetMetricRequest request)
        {
            try
            {
                var result = await _logQueryService.GetMetricAsync(request.MetricName, request.Timespan, request.Aggregation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets events of a specific type from Application Insights
        /// </summary>
        /// <param name="request">The events request</param>
        /// <returns>The events data</returns>
        [HttpPost("events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEvents([FromBody] GetEventsRequest request)
        {
            try
            {
                var result = await _logQueryService.GetEventsAsync(request.EventType, request.Timespan, request.Top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets exceptions from Application Insights
        /// </summary>
        /// <param name="request">Optional request parameters</param>
        /// <returns>The exceptions data</returns>
        [HttpPost("exceptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExceptions([FromBody] TelemetryRequest? request = null)
        {
            try
            {
                var result = await _logQueryService.GetExceptionsAsync(request?.Timespan, request?.Top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets HTTP requests from Application Insights
        /// </summary>
        /// <param name="request">Optional request parameters</param>
        /// <returns>The requests data</returns>
        [HttpPost("requests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRequests([FromBody] TelemetryRequest? request = null)
        {
            try
            {
                var result = await _logQueryService.GetRequestsAsync(request?.Timespan, request?.Top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets dependency calls from Application Insights
        /// </summary>
        /// <param name="request">Optional request parameters</param>
        /// <returns>The dependencies data</returns>
        [HttpPost("dependencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDependencies([FromBody] TelemetryRequest? request = null)
        {
            try
            {
                var result = await _logQueryService.GetDependenciesAsync(request?.Timespan, request?.Top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets traces/logs from Application Insights
        /// </summary>
        /// <param name="request">Optional request parameters</param>
        /// <returns>The traces data</returns>
        [HttpPost("traces")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTraces([FromBody] TelemetryRequest? request = null)
        {
            try
            {
                var result = await _logQueryService.GetTracesAsync(request?.Timespan, request?.Top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets performance counters from Application Insights
        /// </summary>
        /// <param name="request">Optional request parameters</param>
        /// <returns>The performance counters data</returns>
        [HttpPost("performance-counters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPerformanceCounters([FromBody] TelemetryRequest? request = null)
        {
            try
            {
                var result = await _logQueryService.GetPerformanceCountersAsync(request?.Timespan, request?.Top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
