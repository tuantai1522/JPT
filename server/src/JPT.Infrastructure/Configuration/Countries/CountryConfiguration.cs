using JPT.Core.Features.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Countries;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        // Rename to snake case
        builder.ToTable("countries");
        
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(64);

        // One country has multiple cities
        builder.HasMany(r => r.Cities)
            .WithOne()
            .HasForeignKey(p => p.CountryId);
    }
}