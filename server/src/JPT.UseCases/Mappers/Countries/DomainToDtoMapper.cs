using JPT.Core.Features.Countries;
using JPT.UseCases.Features.Countries.GetCitiesByCountryId;
using JPT.UseCases.Features.Countries.GetCountries;

namespace JPT.UseCases.Mappers.Countries;

public static class DomainToDtoMapper
{
    public static CityResponse ToCityResponse(this City city)
        => new(city.Id, city.Name, city.Country.Name);
    
    public static CountryResponse ToCountryResponse(this Country country)
        => new(country.Id, country.Name);

}