using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Jobs.GetJobCategories;

public sealed record GetJobCategoriesQuery : IRequest<Result<IReadOnlyList<GetJobCategoriesResponse>>>;