using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public interface IStudyTasksRepository
{
    Task AddStudyTasks(StudyTask task);
    Task UpdateStudyTasks(StudyTask task);
    Task DeleteStudyTasks(StudyTask task);
    Task<StudyTask?> GetStudyTasksById(Guid taskId);
    Task<List<StudyTask>> GetStudyTasks(Guid studyPlanId);
    Task<int> CountStudyTaskByStudyPlanId(Guid studyPlanId);
}