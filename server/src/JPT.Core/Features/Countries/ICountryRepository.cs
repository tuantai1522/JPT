using JPT.Core.Common;

namespace JPT.Core.Features.Countries;

public interface ICountryRepository : IRepository<Country>
{
    public Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken);
    
    public Task<Country?> GetCountryById(int countryId, CancellationToken cancellationToken);

}