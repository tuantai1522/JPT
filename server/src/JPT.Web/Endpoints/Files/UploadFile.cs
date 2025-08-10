using JPT.UseCases.Features.Files.UploadFile;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JPT.Web.Endpoints.Files;

internal sealed class UploadFile : IEndpoint
{
    private sealed record Request(IFormFile File);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("files", async (
            [FromForm] Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            // Open stream to read from file
            await using var stream = request.File.OpenReadStream();
            
            var command = new UploadFileCommand(stream, request.File.FileName, request.File.ContentType);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Files)
        .DisableAntiforgery();
    }
}
