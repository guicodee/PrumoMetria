namespace PrumoMetria.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Color  { get; set; } = string.Empty;
    public int Progress { get; set; }
    
    public Guid StudyPlanId { get; set; }
    public StudyPlan StudyPlan { get; set; } = null!;
    
    public ICollection<Content> Contents { get; set; } = [];
    public ICollection<StudySession> StudySessions { get; set; } = [];
}