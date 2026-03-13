using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrumoMetria.Entities;

namespace PrumoMetria.Data.Configurations;

// Data/Configurations/StudySessionConfiguration.cs
public class StudySessionConfiguration : IEntityTypeConfiguration<StudySession>
{
    public void Configure(EntityTypeBuilder<StudySession> builder)
    {
        builder.ToTable("StudySessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StartedAt)
            .IsRequired();

        builder.Property(x => x.FinishedAt)
            .IsRequired();

        builder.Property(x => x.DurationMinutes)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.HasOne(x => x.Subject)
            .WithMany(x => x.StudySessions)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId)
            .OnDelete(DeleteBehavior.SetNull) // opcional, não deleta a sessão
            .IsRequired(false);
    }
}