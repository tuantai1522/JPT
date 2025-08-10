using JPT.Core.Common;

namespace JPT.Core.Features.Countries;

public interface ICountryRepository : IRepository<Country>
{
    public Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<City>> GetCitiesByCountryId(int countryId, CancellationToken cancellationToken);

}