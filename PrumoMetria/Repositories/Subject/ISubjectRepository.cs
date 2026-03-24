using PrumoMetria.Entities;

namespace PrumoMetria.Repositories;

public interface ISubjectRepository
{
    Task AddSubject(Subject subject);
    Task UpdateSubject(Subject subject);
    Task DeleteSubject(Subject subject);
    Task<Subject?> GetSubjectById(Guid subjectId);
    Task<List<Subject>> GetSubjects(Guid studyPlanId);
    Task<int> CountSubjectsByStudyPlanId(Guid studyPlanId);
}