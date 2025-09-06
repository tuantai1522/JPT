using JPT.Core.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Users;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Rename to snake case
        builder.ToTable("refresh_tokens");
        
        builder.Property(p => p.Token).IsRequired();
        builder.Property(p => p.Token).HasMaxLength(1024);
        
        builder.Property(c => c.Id).ValueGeneratedNever();
    }
}
