using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Jobs.GetStatisticJobs;

public sealed record GetStatisticJobsQuery : IRequest<Result<GetStatisticJobsResponse>>;