using PrumoMetria.Dto;

namespace PrumoMetria.Services.StudyTasks;

public interface IStudyTasksService
{
    Task<ServiceResult<StudyTaskDTO>> Create(string userId,Guid studyPlanId, CreateStudyTasksDTO studyTask);
    Task<ServiceResult<StudyTaskDTO>> Update(string userId, Guid studyTaskId, UpdateStudyTaskDTO studyTask);
    Task<ServiceResult<bool>> Delete(string userId, Guid studyTaskId);
    Task<ServiceResult<StudyTaskDTO>> GetStudyTaskById(string userId, Guid studyTaskId);
    Task<ServiceResult<List<StudyTaskDTO>>> GetStudyTasksByStudyPlanId(string userId, Guid studyPlanId);
}