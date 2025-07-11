using DevopsMCP.Models;
using DevopsMCP.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevopsMCP.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemService _workItemService;

        public WorkItemController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        /// <summary>
        /// Creates a new work item
        /// </summary>
        /// <param name="request">The work item creation request</param>
        /// <returns>The created work item</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateWorkItem([FromBody] CreateWorkItemRequest request)
        {
            try
            {
                var result = await _workItemService.CreateWorkItemAsync(request.Project, request.Type, request.Fields);
                return CreatedAtAction(nameof(GetWorkItem), new { id = result.GetProperty("id").GetInt32() }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a work item by its ID
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <returns>The work item details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorkItem([FromRoute] int id)
        {
            try
            {
                var result = await _workItemService.GetWorkItemAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <param name="request">The update request</param>
        /// <returns>The updated work item</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWorkItem([FromRoute] int id, [FromBody] UpdateWorkItemRequest request)
        {
            try
            {
                var result = await _workItemService.UpdateWorkItemAsync(id, request.Fields);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <returns>Success confirmation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkItem([FromRoute] int id)
        {
            try
            {
                var result = await _workItemService.DeleteWorkItemAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets tags for a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <returns>List of tags</returns>
        [HttpGet("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTags([FromRoute] int id)
        {
            try
            {
                var result = await _workItemService.GetTagsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Adds tags to a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <param name="request">The tags to add</param>
        /// <returns>Success confirmation with updated tags</returns>
        [HttpPost("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTags([FromRoute] int id, [FromBody] TagsRequest request)
        {
            try
            {
                var result = await _workItemService.AddTagsAsync(id, request.Tags);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Removes tags from a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <param name="request">The tags to remove</param>
        /// <returns>Success confirmation with remaining tags</returns>
        [HttpDelete("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveTags([FromRoute] int id, [FromBody] TagsRequest request)
        {
            try
            {
                var result = await _workItemService.RemoveTagsAsync(id, request.Tags);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Gets comments for a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <returns>List of comments</returns>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            try
            {
                var result = await _workItemService.GetCommentsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Adds a comment to a work item
        /// </summary>
        /// <param name="id">The work item ID</param>
        /// <param name="request">The comment to add</param>
        /// <returns>The added comment</returns>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddComment([FromRoute] int id, [FromBody] CommentRequest request)
        {
            try
            {
                var result = await _workItemService.AddCommentAsync(id, request.Comment);
                return CreatedAtAction(nameof(GetComments), new { id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
