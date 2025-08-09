using JPT.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Users;

public class CvConfiguration : IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        // Rename to snake case
        builder.ToTable("cvs");
        
        // To define composite key
        builder.HasKey(cm => new { cm.ApplicantId, cm.CvId });
        
        // One cv belongs to one file
        builder.HasOne(u => u.File)
            .WithOne()
            .HasForeignKey<Cv>(u => u.CvId);
    }
}
