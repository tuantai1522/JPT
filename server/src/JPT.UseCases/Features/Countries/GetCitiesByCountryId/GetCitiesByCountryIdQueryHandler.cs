using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.UseCases.Mappers.Countries;
using MediatR;

namespace JPT.UseCases.Features.Countries.GetCitiesByCountryId;

public class GetCitiesByCountryIdQueryHandler(ICountryRepository countryRepository) : IRequestHandler<GetCitiesByCountryIdQuery, Result<List<CityResponse>>>
{
    public async Task<Result<List<CityResponse>>> Handle(GetCitiesByCountryIdQuery query, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetCountryById(query.CountryId, cancellationToken);

        if (country is null)
        {
            return Result.Failure<List<CityResponse>>(CountriesErrors.NotFound(query.CountryId));
        }

        var result = country.Cities
            .Select(x => x.ToCityResponse())
            .ToList();

        return Result.Success(result);
    }
}