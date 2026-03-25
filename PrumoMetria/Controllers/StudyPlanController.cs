using Microsoft.AspNetCore.Mvc;
using PrumoMetria.Dto.StudyPlans;
using PrumoMetria.Services.StudyPlans;

namespace PrumoMetria.Controllers;

[Route("api/studyplan")]
public class StudyPlanController : BaseController
{
    private readonly IStudyPlanService _service;

    public StudyPlanController(IStudyPlanService service)
    {
        _service = service;
    }

    [HttpGet("{studyPlanId}")]
    public async Task<IActionResult> GetPlanById([FromRoute] Guid studyPlanId)
    {
        var userId = GetUserId();
        
        var result = await _service.GetPlanById(userId, studyPlanId);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetPlans()
    {
        var userId = GetUserId();
        
        var result = await _service.GetPlansByUserId(userId);
        
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);
        
        return Ok(result.Data);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreateStudyPlanDTO studyPlan)
    {
        var userId = GetUserId();
        
        var result = await _service.Create(userId, studyPlan);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return CreatedAtAction(nameof(GetPlanById), new { studyPlanId = result.Data!.Id },  result.Data);
    }

    [HttpPut("{studyPlanId}")]
    public async Task<IActionResult> UpdatePlan(
        [FromRoute] Guid studyPlanId, 
        [FromBody] UpdateStudyPlanDTO studyPlan)
    {
        var userId = GetUserId();
        
        var result = await _service.Update(userId, studyPlanId, studyPlan);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);
            
        return Ok(result.Data);
    }

    [HttpDelete("{studyPlanId}")]
    public async Task<IActionResult> DeletePlan([FromRoute] Guid studyPlanId)
    {
        var userId = GetUserId();
        
        var result = await _service.Delete(userId, studyPlanId);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result.Error);

        return NoContent();
    }
}