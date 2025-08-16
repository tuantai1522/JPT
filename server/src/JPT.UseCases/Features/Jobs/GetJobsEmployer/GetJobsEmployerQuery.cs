using JPT.Core.Common;
using JPT.UseCases.Pagination;
using MediatR;

namespace JPT.UseCases.Features.Jobs.GetJobsEmployer;

public sealed record GetJobsEmployerQuery : PaginationRequest<GetJobsEmployerResponse>;