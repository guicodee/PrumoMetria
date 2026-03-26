using System.ComponentModel.DataAnnotations;

namespace PrumoMetria.Dto.StudyPlans;

public class CreateStudyPlanDTO {
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty; 
    
    [Required(ErrorMessage = "Color is required")]
    public string Color { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; } = string.Empty;
};

public class UpdateStudyPlanDTO
{
    [Required(ErrorMessage = "Name is required")] 
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; } = string.Empty;
};

public record StudyPlanDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime CreatedAt { get; init; }
};