using Microsoft.AspNetCore.Identity;

namespace PrumoMetria.Entities;

public class ApplicationUser : IdentityUser
{
     public string? Name { get; set; }
     public bool IsPremium { get; set; } = false;

     public ICollection<StudyPlan> StudyPlans { get; set; } = [];
}