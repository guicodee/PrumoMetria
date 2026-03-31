using Microsoft.AspNetCore.Mvc;
using PrumoMetria.Dto.Contents;
using PrumoMetria.Entities;
using PrumoMetria.Services.Contents;

namespace PrumoMetria.Controllers;

[Route("api/studyplan/{studyPlanId}/subject/{subjectId}/content")]
public class ContentController : BaseController
{
    private readonly IContentService _service;

    public ContentController(IContentService contentService)
    {
        _service = contentService;
    }

    [HttpGet("{contentId}")]
    public async Task<IActionResult> GetContentById([FromRoute] Guid contentId)
    {
        var userId = GetUserId();

        var result = await _service.GetContentById(userId, contentId);
        
        if(!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);
        
        return Ok(result.Data);
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetContents([FromRoute] Guid subjectId)
    {
        var userId = GetUserId();

        var result = await _service.GetContentList(userId, subjectId);
        
        if(!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);
        
        return Ok(result.Data);
    }
    

    [HttpPost()]
    public async Task<IActionResult> CreateContent(
        [FromRoute] Guid subjectId,
        [FromBody] CreateContentDTO content)
    {
        var userId = GetUserId();

        var result = await _service.Create(userId, subjectId, content);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);    
        
        return CreatedAtAction(nameof(GetContentById), new { subjectId, contentId = result.Data!.Id}, result.Data);
    }

    [HttpPut("{contentId}")]
    public async Task<IActionResult> UpdateContent(
        [FromRoute] Guid contentId,
        [FromBody] UpdateContentDTO content)
    {
        var userId = GetUserId();

        var result = await _service.Update(userId, contentId, content);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Data);
    }

    [HttpDelete("{contentId}")]
    public async Task<IActionResult> DeleteContent([FromRoute] Guid contentId)
    {
        var userId = GetUserId();

        var result = await _service.Delete(userId, contentId);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return NoContent();
    }
}