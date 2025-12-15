using DevopsMCP.Models;
using DevopsMCP.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevopsMCP.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TestCaseController : ControllerBase
    {
        private readonly ITestPlanService _testPlanService;

        public TestCaseController(ITestPlanService testPlanService)
        {
            _testPlanService = testPlanService;
        }

        /// <summary>
        /// Creates a new test plan
        /// </summary>
        /// <param name="request">The test plan creation request</param>
        /// <returns>The created test plan</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTestPlan([FromBody] CreateTestPlanRequest request)
        {
            try
            {
                var result = await _testPlanService.CreateTestPlanAsync(
                    request.Project, 
                    request.Name, 
                    request.Fields, 
                    request.Steps, 
                    request.Parameters);
                return CreatedAtAction(nameof(GetTestPlan), new { id = result.GetProperty("id").GetInt32() }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a test plan by its ID
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <returns>The test plan details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTestPlan([FromRoute] int id)
        {
            try
            {
                var result = await _testPlanService.GetTestPlanAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <param name="request">The update request</param>
        /// <returns>The updated test plan</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTestPlan([FromRoute] int id, [FromBody] UpdateTestPlanRequest request)
        {
            try
            {
                var result = await _testPlanService.UpdateTestPlanAsync(
                    id, 
                    request.Fields, 
                    request.Steps, 
                    request.Parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <returns>Success confirmation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTestPlan([FromRoute] int id)
        {
            try
            {
                var result = await _testPlanService.DeleteTestPlanAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets tags for a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <returns>List of tags</returns>
        [HttpGet("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTags([FromRoute] int id)
        {
            try
            {
                var result = await _testPlanService.GetTagsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Adds tags to a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <param name="request">The tags to add</param>
        /// <returns>Success confirmation with updated tags</returns>
        [HttpPost("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTags([FromRoute] int id, [FromBody] TagsRequest request)
        {
            try
            {
                var result = await _testPlanService.AddTagsAsync(id, request.Tags);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Removes tags from a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <param name="request">The tags to remove</param>
        /// <returns>Success confirmation with remaining tags</returns>
        [HttpDelete("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveTags([FromRoute] int id, [FromBody] TagsRequest request)
        {
            try
            {
                var result = await _testPlanService.RemoveTagsAsync(id, request.Tags);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets comments for a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <returns>List of comments</returns>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            try
            {
                var result = await _testPlanService.GetCommentsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Adds a comment to a test plan
        /// </summary>
        /// <param name="id">The test plan ID</param>
        /// <param name="request">The comment to add</param>
        /// <returns>The added comment</returns>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddComment([FromRoute] int id, [FromBody] CommentRequest request)
        {
            try
            {
                var result = await _testPlanService.AddCommentAsync(id, request.Comment);
                return CreatedAtAction(nameof(GetComments), new { id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
