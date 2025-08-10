using JPT.UseCases.Features.Countries.GetCitiesByCountryId;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Countries;

internal sealed class GetCitiesByCountryId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("countries/{id:int}/get-cities", async (
            int id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var query = new GetCitiesByCountryIdQuery(id);

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Countries);
    }
}
