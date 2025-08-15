using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Countries.GetCitiesByCountryId;

public class GetCitiesByCountryIdQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCitiesByCountryIdQuery, Result<List<CityResponse>>>
{
    public async Task<Result<List<CityResponse>>> Handle(GetCitiesByCountryIdQuery query, CancellationToken cancellationToken)
    {
        var result = await dbContext.Set<City>()
            .Where(c => c.CountryId == query.CountryId)
            .Join(dbContext.Set<Country>(),
                city => city.CountryId,
                country => country.Id,
                (city, country) => new CityResponse(city.Id, city.Name, country.Name))
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}