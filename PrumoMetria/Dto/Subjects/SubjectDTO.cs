using System.ComponentModel.DataAnnotations;
using PrumoMetria.Entities;

namespace PrumoMetria.Dto.Subjects;

public record CreateSubjectDTO(
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    string Name, 
    
    [Required(ErrorMessage = "Color is required")]
    string Color
);

public record UpdateSubjectDTO(
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    string Name, 
    
    [Required(ErrorMessage = "Color is required")]
    string Color
);

public record SubjectDTO{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public int Progress { get; init; }
    public ICollection<Content?> Contents { get; init; } = [];
    public DateTime CreatedAt { get; init; }
};