namespace PrumoMetria.Entities;

public class Tasks : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    
    public Guid StudyPlanId { get; set; }
    public StudyPlan StudyPlan { get; set; } = null!;

    public Guid? SubjectId { get; set; }        
    public Subject? Subject { get; set; }

    public Guid? ContentId { get; set; }         
    public Content? Content { get; set; }
}