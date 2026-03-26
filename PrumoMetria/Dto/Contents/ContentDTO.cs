using System.ComponentModel.DataAnnotations;
using PrumoMetria.Enum;

namespace PrumoMetria.Dto.Contents;

public class CreateContentDTO
{
    [Required(ErrorMessage = "Name is required")] 
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; } = string.Empty;

    public ContentPriority Priority { get; set; }
};

public class UpdateContentDTO
{
    [Required(ErrorMessage = "Name is required")] 
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; } = string.Empty;

    public ContentPriority Priority { get; set; } 

    public ContentStatus Status { get; set; } 
};

public record ContentDTO {
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public ContentStatus Status { get; init; }
    public ContentPriority Priority { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdateAt { get; init; }
};