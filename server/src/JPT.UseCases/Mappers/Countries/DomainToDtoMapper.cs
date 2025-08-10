using JPT.Core.Features.Countries;
using JPT.UseCases.Features.Countries.GetCountries;

namespace JPT.UseCases.Mappers.Countries;

public static class DomainToDtoMapper
{
    public static CountryResponse ToCountryResponse(this Country country)
        => new(country.Id, country.Name);

}