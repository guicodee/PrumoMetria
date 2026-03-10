using Microsoft.AspNetCore.Identity;

namespace PrumoMetria.Entities;

public class ApplicationUser : IdentityUser
{
     public string? Name { get; set; }
}