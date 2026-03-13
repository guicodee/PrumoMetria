namespace PrumoMetria.Entities;

public class StudyPlan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    
    public ICollection<Subject> Subjects { get; set; } = [];
    public ICollection<Tasks> Tasks { get; set; } = [];
    
    public int TotalStudyTime { get; set; }
    public int Progress { get; set; }
}