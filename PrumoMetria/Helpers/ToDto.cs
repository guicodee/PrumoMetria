using PrumoMetria.Dto;
using PrumoMetria.Dto.Contents;
using PrumoMetria.Dto.StudyPlans;
using PrumoMetria.Dto.Subjects;
using PrumoMetria.Entities;

namespace PrumoMetria.Helpers;

public static class StudyPlanExtensions
{
    public static StudyPlanDTO ToDTOStudyPlan(this StudyPlan studyPlan)
    {
        return new StudyPlanDTO
        {
            Id = studyPlan.Id,
            Name = studyPlan.Name,
            Color = studyPlan.Color,
            Description = studyPlan.Description,
            CreatedAt = studyPlan.CreatedAt
        };
    }
    public static ContentDTO ToDTOContent(this Content content)
    {
        return new ContentDTO
        {
            Id = content.Id,
            Name = content.Name,
            Description = content.Description,
            Priority = content.Priority,
            Status = content.Status
        };
    } 
    public static SubjectDTO ToDTOSubject(this Subject subject)
    {
        return new SubjectDTO()
        {
            Id = subject.Id,
            Name = subject.Name,
            Color = subject.Color,
            Contents = subject.Contents,
            CreatedAt = subject.CreatedAt,
            Progress = subject.Progress
        };
    }
    
    public static StudyTaskDTO ToDTOStudyTasks(this StudyTask task)
    {
        return new StudyTaskDTO()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt,
        };
    }
}