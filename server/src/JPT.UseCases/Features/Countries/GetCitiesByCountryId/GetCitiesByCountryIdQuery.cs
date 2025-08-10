using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Countries.GetCitiesByCountryId;

public sealed record GetCitiesByCountryIdQuery(int CountryId) : IRequest<Result<List<CityResponse>>>;
