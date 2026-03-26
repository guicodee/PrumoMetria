using System.ComponentModel.DataAnnotations;
using PrumoMetria.Entities;

namespace PrumoMetria.Dto.Subjects;

public class CreateSubjectDTO{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Color is required")]
    public string Color { get; set; } = string.Empty;
};

public class UpdateSubjectDTO{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Color is required")]
    public string Color { get; set; } = string.Empty;
};

public record SubjectDTO{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public int Progress { get; init; }
    public ICollection<Content?> Contents { get; init; } = [];
    public DateTime CreatedAt { get; init; }
};