using JPT.Core.Features.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = JPT.Core.Features.Files.File;

namespace JPT.Infrastructure.Configuration.Files;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        // Rename to snake case
        builder.ToTable("files");
        
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(p => p.Name).HasMaxLength(512);
        
        builder.Property(p => p.Path).IsRequired();
        builder.Property(p => p.Path).HasMaxLength(512);
        
        builder.Property(p => p.MimeType).IsRequired();
        builder.Property(p => p.MimeType).HasMaxLength(128);
        
        // To store string in database with enum UploadFileType
        builder.Property(p => p.UploadType)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<UploadFileType>(v));
        builder.Property(p => p.UploadType).HasMaxLength(128);
    }
}