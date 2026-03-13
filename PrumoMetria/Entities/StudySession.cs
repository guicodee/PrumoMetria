namespace PrumoMetria.Entities;

public class StudySession : BaseEntity
{
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public int DurationMinutes { get; set; }
    
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
    
    public Guid? ContentId { get; set; }
    public Content Content  { get; set; }
}