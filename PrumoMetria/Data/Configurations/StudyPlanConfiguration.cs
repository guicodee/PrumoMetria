using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrumoMetria.Entities;

namespace PrumoMetria.Data.Configurations;

public class StudyPlanConfiguration : IEntityTypeConfiguration<StudyPlan>
{
    public void Configure(EntityTypeBuilder<StudyPlan> builder)
    {
        builder.ToTable("StudyPlans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Color)
            .IsRequired()
            .HasMaxLength(7);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.StudyPlans)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Subjects)
            .WithOne(x => x.StudyPlan)
            .HasForeignKey(x => x.StudyPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.StudyPlan)
            .HasForeignKey(x => x.StudyPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}