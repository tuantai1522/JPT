using JPT.Core.Features.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Jobs;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        // Rename to snake case
        builder.ToTable("job_applications");
        
        builder.Property(c => c.Id).ValueGeneratedNever();
        
        // To store string in database with enum JobApplicationStatus
        builder.Property(p => p.Status)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<JobApplicationStatus>(v));
        builder.Property(p => p.Status).HasMaxLength(64);

    }
}
