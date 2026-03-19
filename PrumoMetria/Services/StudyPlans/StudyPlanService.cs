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
    
    public async Task<StudyPlanDTO?> Create(string userId, CreateStudyPlanDTO studyPlan)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return null;
        
        var countPlansOfUser = await _repository.GetStudyPlanCountByUserId(userId);
        var maxPlans = user.IsPremium ? int.MaxValue : 2;

        if (countPlansOfUser >= maxPlans)
            return null;
        
        var newStudyPlan = new StudyPlan()
        {
            Name = studyPlan.Name,
            Color = studyPlan.Color,
            Description = studyPlan.Description,
            UserId = userId,
        };

        await _repository.AddStudyPlan(newStudyPlan);

        return newStudyPlan.ToDTO();
    }

    public async Task<StudyPlanDTO?> Update(string userId, Guid studyPlanId, UpdateStudyPlanDTO studyPlan)
    {
        var existsStudyPlan = await _repository.GetStudyPlanById(studyPlanId);

        if (existsStudyPlan == null)
            return null;

        if (existsStudyPlan.UserId != userId)
            return null;

        existsStudyPlan.Name = studyPlan.Name;
        existsStudyPlan.Description = studyPlan.Description;

        await _repository.UpdateStudyPlan(existsStudyPlan);

        return existsStudyPlan.ToDTO();
    }

    public async Task<bool> Delete(string userId, Guid studyPlanId)
    {
        var existsStudyPlan = await _repository.GetStudyPlanById(studyPlanId);

        if (existsStudyPlan == null)
            return false;

        if (existsStudyPlan.UserId != userId)
            return false;

        await _repository.DeleteStudyPlan(existsStudyPlan);
        
        return true;
    }

    public async Task<StudyPlanDTO?> GetPlanById(string userId, Guid studyPlanId)
    {
        var studyPlan = await _repository.GetStudyPlanById(studyPlanId);
        
        if(studyPlan == null)
            return null;

        if (studyPlan.UserId != userId)
            return null;

        return studyPlan.ToDTO();
    }

    public async Task<List<StudyPlanDTO>> GetPlansByUserId(string userId)
    {
        var studyPlans = await _repository.GetStudyPlanByUserId(userId);

        return studyPlans.Select(x => x.ToDTO()).ToList();
    }
}
