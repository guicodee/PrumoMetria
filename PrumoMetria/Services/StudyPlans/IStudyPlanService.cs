using PrumoMetria.Dto.StudyPlans;

namespace PrumoMetria.Services.StudyPlans;

public interface IStudyPlanService
{
    Task<StudyPlanDTO?> Create(string userId, CreateStudyPlanDTO studyPlan);
    Task<StudyPlanDTO?> Update(string userId, Guid studyPlanId, UpdateStudyPlanDTO studyPlan);
    Task<bool> Delete(string userId, Guid studyPlanId);
    Task<StudyPlanDTO?> GetPlanById(string userId, Guid studyPlanId);
    Task<List<StudyPlanDTO>> GetPlansByUserId(string userId);
}