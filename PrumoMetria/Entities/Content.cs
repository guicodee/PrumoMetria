using PrumoMetria.Enum;

namespace PrumoMetria.Entities;

public class Content : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public ContentStatus Status { get; set; } = ContentStatus.NotStarted;
    public ContentPriority Priority { get; set; } = ContentPriority.Low;
    
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}