using Microsoft.AspNetCore.Mvc;
using PrumoMetria.Dto.Subjects;
using PrumoMetria.Services.Subjects;

namespace PrumoMetria.Controllers;

[Route("api/studyplan/{studyPlanId}/subject")]
public class SubjectController : BaseController
{
    private readonly ISubjectService _service;

    public SubjectController(ISubjectService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSubject(
        [FromRoute] Guid studyPlanId, 
        [FromBody] CreateSubjectDTO subject)
    {
        var userId = GetUserId();
        
        var result = await _service.Create(userId, studyPlanId, subject);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return CreatedAtAction(nameof(GetSubject), new { studyPlanId, subjectId = result.Data!.Id}, result.Data);
    }

    [HttpPut("{subjectId}")]
    public async Task<IActionResult> UpdateSubject(
        [FromRoute] Guid subjectId,
        [FromBody] UpdateSubjectDTO subject)
    {
        var userId = GetUserId();

        var result = await _service.Update(userId, subjectId, subject);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Data);
    }

    [HttpDelete("{subjectId}")]
    public async Task<IActionResult> DeleteSubject([FromRoute] Guid subjectId)
    {
        var userId = GetUserId();

        var result = await _service.Delete(userId, subjectId);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return NoContent();
    }

    [HttpGet("{subjectId}")]
    public async Task<IActionResult> GetSubject([FromRoute] Guid subjectId)
    {
        var userId = GetUserId();

        var result = await _service.GetSubjectById(userId, subjectId);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);
        
        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetListSubjects([FromRoute] Guid studyPlanId)
    {
        var userId = GetUserId();

        var result = await _service.GetSubjectsByUserId(userId, studyPlanId);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);
        
        return Ok(result.Data);
    }
}