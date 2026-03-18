using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrumoMetria.Entities;

namespace PrumoMetria.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<StudyPlan> StudyPlans { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }
    public DbSet<Tasks> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}