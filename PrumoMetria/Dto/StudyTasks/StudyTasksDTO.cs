using System.ComponentModel.DataAnnotations;

namespace PrumoMetria.Dto;

public class CreateStudyTasksDTO
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string? Description { get; set; } = string.Empty;
    
    public DateTime? DueDate { get; set; }
    
    public Guid? ContentId { get; set; }
    public Guid? SubjectId { get; set; }
}

public class UpdateStudyTaskDTO
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;

    public Guid? SubjectId { get; set; }    // opcional
    public Guid? ContentId { get; set; }    // opcional
}

public record StudyTaskDTO
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public Guid? SubjectId { get; init; }
    public Guid? ContentId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
