using JPT.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = JPT.Core.Features.Files.File;

namespace JPT.Infrastructure.Configuration.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Rename to snake case
        builder.ToTable("users");
        
        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.FirstName).HasMaxLength(64);
        
        builder.Property(p => p.MiddleName).IsRequired(false);
        builder.Property(p => p.MiddleName).HasMaxLength(64);
        
        builder.Property(p => p.LastName).IsRequired(false);
        builder.Property(p => p.LastName).HasMaxLength(64);
        
        builder.Property(p => p.Description).IsRequired(false);
        builder.Property(p => p.Description).HasMaxLength(2048);
        
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(128);
        
        builder.Property(p => p.HashPassword).IsRequired();
        builder.Property(p => p.HashPassword).HasMaxLength(1024);
        
        // To store string in database with enum JobStatus
        builder.Property(p => p.Role)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<UserRole>(v));
        builder.Property(p => p.Role).HasMaxLength(64);

        // One user has one avatar
        builder.HasOne<File>()
            .WithOne()
            .HasForeignKey<User>(u => u.AvatarId)
            .OnDelete(DeleteBehavior.SetNull); // Don't remove user when avatar is deleted
        
        // One user has multiple saved jobs
        builder.HasMany(x => x.SavedJobs)
            .WithOne()
            .HasForeignKey(u => u.ApplicantId);
        
        // One user has multiple job applications
        builder.HasMany(x => x.JobApplications)
            .WithOne()
            .HasForeignKey(u => u.ApplicantId);
        
        // One user has multiple Cvs
        builder.HasMany(x => x.Cvs)
            .WithOne()
            .HasForeignKey(u => u.ApplicantId);
                
        // One user has multiple RefreshTokens
        builder.HasMany(x => x.RefreshTokens)
            .WithOne()
            .HasForeignKey(u => u.UserId);
    }
}
