using JPT.UseCases.Features.Countries.GetCitiesByCountryId;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Cities;

internal sealed class GetCitiesByCountryId : IEndpoint
{
    private sealed record Request(int CountryId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("cities", async (
            [AsParameters] Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetCitiesByCountryIdQuery(request.CountryId);

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Cities);
    }
}
