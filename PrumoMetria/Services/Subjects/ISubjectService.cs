using PrumoMetria.Dto.Subjects;

namespace PrumoMetria.Services.Subjects;

public interface ISubjectService
{
    Task<ServiceResult<SubjectDTO?>> Create(string userId, Guid studyPlanId, CreateSubjectDTO subject);
    Task<ServiceResult<SubjectDTO>> Update(string userId, Guid studyPlanId, UpdateSubjectDTO id);
    Task<ServiceResult<bool>> Delete(string userId, Guid studyPlanId);
    Task<ServiceResult<SubjectDTO?>> GetSubjectById(string userId, Guid studyPlanId);
    Task<ServiceResult<List<SubjectDTO>>> GetSubjectsByUserId(string userId);
}