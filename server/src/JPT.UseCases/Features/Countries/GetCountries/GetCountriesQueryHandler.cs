using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.UseCases.Mappers.Countries;
using MediatR;

namespace JPT.UseCases.Features.Countries.GetCountries;

public class GetCountriesQueryHandler(ICountryRepository countryRepository) : IRequestHandler<GetCountriesQuery, Result<List<CountryResponse>>>
{
    public async Task<Result<List<CountryResponse>>> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await countryRepository.GetCountriesAsync(cancellationToken);

        var result = countries
            .Select(x => x.ToCountryResponse())
            .ToList();

        return Result.Success(result);
    }
}