using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public interface IStudyPlanRepository
{
    Task AddStudyPlan(StudyPlan studyPlan);
    Task UpdateStudyPlan(StudyPlan studyPlan);
    Task DeleteStudyPlan(StudyPlan studyPlan);
    Task<StudyPlan?> GetStudyPlanById(Guid id);
    Task<List<StudyPlan>> GetStudyPlanByUserId(string userId);
    Task<int> GetStudyPlanCountByUserId(string userId);
    Task<bool> ExistsStudyPlan(string userId, Guid studyPlanId);
}