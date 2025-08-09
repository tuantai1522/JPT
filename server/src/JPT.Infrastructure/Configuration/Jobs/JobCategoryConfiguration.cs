using JPT.Core.Features.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Jobs;

public class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
{
    public void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        // Rename to snake case
        builder.ToTable("job_categories");
        
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(512);
        
        // One categories job has multiple jobs
        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.JobCategory)
            .HasForeignKey(u => u.JobCategoryId);
    }
}