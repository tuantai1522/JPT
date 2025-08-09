using JPT.Core.Features.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JPT.Infrastructure.Configuration.Countries;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(64);

        // One country has multiple cities
        builder.HasMany(r => r.Cities)
            .WithOne(r => r.Country)
            .HasForeignKey(p => p.CountryId);
    }
}