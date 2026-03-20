using Microsoft.AspNetCore.Identity;
using PrumoMetria.Dto.StudyPlans;
using PrumoMetria.Entities;
using PrumoMetria.Helpers;
using PrumoMetria.Repositories;

namespace PrumoMetria.Services.StudyPlans;

public class StudyPlanService : IStudyPlanService
{
    private readonly IStudyPlanRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;

    public StudyPlanService(
        IStudyPlanRepository repository,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }
    
    public async Task<ServiceResult<StudyPlanDTO?>> Create(string userId, CreateStudyPlanDTO studyPlan)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return ServiceResult<StudyPlanDTO?>.Fail("User not found", 404);
        
        var countPlansOfUser = await _repository.GetStudyPlanCountByUserId(userId);
        var maxPlans = user.IsPremium ? int.MaxValue : 2;

        if (countPlansOfUser >= maxPlans)
            return ServiceResult<StudyPlanDTO?>.Fail("Plan limit reached.", 400);
        
        var newStudyPlan = new StudyPlan()
        {
            Name = studyPlan.Name,
            Color = studyPlan.Color,
            Description = studyPlan.Description,
            UserId = userId,
        };

        await _repository.AddStudyPlan(newStudyPlan);

        return ServiceResult<StudyPlanDTO?>.Success(newStudyPlan.ToDTO());
    }

    public async Task<ServiceResult<StudyPlanDTO?>> Update(string userId, Guid studyPlanId, UpdateStudyPlanDTO studyPlan)
    {
        var existsStudyPlan = await _repository.GetStudyPlanById(studyPlanId);

        if (existsStudyPlan == null)
            return ServiceResult<StudyPlanDTO?>.Fail("Plan not found", 404);

        if (existsStudyPlan.UserId != userId)
            return ServiceResult<StudyPlanDTO?>.Fail("Plan not found", 404);

        existsStudyPlan.Name = studyPlan.Name;
        existsStudyPlan.Description = studyPlan.Description;

        await _repository.UpdateStudyPlan(existsStudyPlan);

        return ServiceResult<StudyPlanDTO?>.Success(existsStudyPlan.ToDTO());
    }

    public async Task<ServiceResult<bool>> Delete(string userId, Guid studyPlanId)
    {
        var existsStudyPlan = await _repository.GetStudyPlanById(studyPlanId);

        if (existsStudyPlan == null)
            return ServiceResult<bool>.Fail("Plan not found", 404);

        if (existsStudyPlan.UserId != userId)
            return ServiceResult<bool>.Fail("Plan not found", 404);

        await _repository.DeleteStudyPlan(existsStudyPlan);
        
        return ServiceResult<bool>.Success(true);
    }

    public async Task<ServiceResult<StudyPlanDTO?>> GetPlanById(string userId, Guid studyPlanId)
    {
        var studyPlan = await _repository.GetStudyPlanById(studyPlanId);
        
        if(studyPlan == null)
            return ServiceResult<StudyPlanDTO?>.Fail("Plan not found", 404);

        if (studyPlan.UserId != userId)
            return ServiceResult<StudyPlanDTO?>.Fail("Plan not found", 404);
        
        return ServiceResult<StudyPlanDTO?>.Success(studyPlan.ToDTO());
    }

    public async Task<ServiceResult<List<StudyPlanDTO>>> GetPlansByUserId(string userId)
    {
        var studyPlans = await _repository.GetStudyPlanByUserId(userId);

        return ServiceResult<List<StudyPlanDTO>>
            .Success(studyPlans.Select(x => x.ToDTO()).ToList());
    }
}
