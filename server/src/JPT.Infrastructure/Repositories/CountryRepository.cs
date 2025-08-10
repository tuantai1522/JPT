using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace JPT.Infrastructure.Repositories;

public sealed class CountryRepository(ApplicationDbContext context) : ICountryRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Countries
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Country?> GetCountryById(int countryId, CancellationToken cancellationToken)
    {
        return await _context.Countries
            .AsNoTracking()
            .Include(country => country.Cities.Where(city => city.CountryId == countryId))
            .FirstOrDefaultAsync(country => country.Id == countryId, cancellationToken);
    }
}