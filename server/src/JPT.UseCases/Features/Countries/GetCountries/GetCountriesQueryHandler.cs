using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Countries.GetCountries;

public class GetCountriesQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCountriesQuery, Result<List<CountryResponse>>>
{
    public async Task<Result<List<CountryResponse>>> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
    {
        var result = await dbContext.Set<Country>()
            .Select(country => new CountryResponse(country.Id, country.Name))
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}