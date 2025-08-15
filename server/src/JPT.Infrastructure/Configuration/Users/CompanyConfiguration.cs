using JPT.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = JPT.Core.Features.Files.File;

namespace JPT.Infrastructure.Configuration.Users;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Rename to snake case
        builder.ToTable("companies");
        
        builder.Property(c => c.Id).ValueGeneratedNever();
        
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(512);
        
        builder.Property(p => p.Description).HasMaxLength(2048);
        
        // 1:1 relationship with Employer
        builder.HasOne<User>()    
            .WithOne() 
            .HasForeignKey<Company>(r => r.EmployerId);
        
        // One company has one logo
        builder.HasOne<File>()
            .WithOne()
            .HasForeignKey<Company>(u => u.LogoId)
            .OnDelete(DeleteBehavior.SetNull); // Don't remove company when avatar is deleted
        
        // One company has multiple jobs
        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.Company)
            .HasForeignKey(u => u.CompanyId);

    }
}
