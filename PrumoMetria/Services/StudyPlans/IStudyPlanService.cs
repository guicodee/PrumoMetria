using PrumoMetria.Dto.StudyPlans;

namespace PrumoMetria.Services.StudyPlans;

public interface IStudyPlanService
{
    Task<ServiceResult<StudyPlanDTO?>> Create(string userId, CreateStudyPlanDTO studyPlan);
    Task<ServiceResult<StudyPlanDTO?>> Update(string userId, Guid studyPlanId, UpdateStudyPlanDTO studyPlan);
    Task<ServiceResult<bool>> Delete(string userId, Guid studyPlanId);
    Task<ServiceResult<StudyPlanDTO?>> GetPlanById(string userId, Guid studyPlanId);
    Task<ServiceResult<List<StudyPlanDTO>>> GetPlansByUserId(string userId);
}