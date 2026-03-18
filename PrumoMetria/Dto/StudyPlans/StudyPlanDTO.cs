using System.ComponentModel.DataAnnotations;

namespace PrumoMetria.Dto.StudyPlans;

public record CreateStudyPlanDTO(
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    string Name, 
    
    [Required(ErrorMessage = "Color is required")]
    string Color,
    
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    string? Description
);

public record StudyPlanDTO{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime CreatedAt { get; init; }
};