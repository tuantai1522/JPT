using JPT.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Users;

public class SavedJobConfiguration : IEntityTypeConfiguration<SavedJob>
{
    public void Configure(EntityTypeBuilder<SavedJob> builder)
    {
        // Rename to snake case
        builder.ToTable("saved_jobs");
        
        // To define composite key
        builder.HasKey(cm => new { cm.ApplicantId, cm.JobId });
        
        // One job has multiple savedd jobs
        builder.HasOne(x => x.Job)
            .WithMany()
            .HasForeignKey(u => u.JobId);
    }
}
