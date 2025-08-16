using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Jobs;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        // Rename to snake case
        builder.ToTable("jobs");
        
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Title).HasMaxLength(512);
        
        builder.Property(p => p.Description).IsRequired(false);
        builder.Property(p => p.Requirements).IsRequired(false);

        builder
            .Property(j => j.MinSalary)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false); 
        
        builder
            .Property(j => j.MinSalary)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false); 
        
        // To store string in database with enum JobType
        builder.Property(p => p.Type)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<JobType>(v));
        builder.Property(p => p.Type).HasMaxLength(64);

        
        // To store string in database with enum JobStatus
        builder.Property(p => p.Status)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<JobStatus>(v));
        builder.Property(p => p.Status).HasMaxLength(64);

        // One job belongs to one city
        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(u => u.CityId);
        
        // One job belongs to one company
        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(u => u.CompanyId);
        
        // One job has multiple applications
        builder.HasMany(x => x.JobApplications)
            .WithOne()
            .HasForeignKey(u => u.JobId);
    }
}