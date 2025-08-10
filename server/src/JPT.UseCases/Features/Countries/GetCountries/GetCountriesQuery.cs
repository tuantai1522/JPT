using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Countries.GetCountries;

public sealed record GetCountriesQuery : IRequest<Result<List<CountryResponse>>>;