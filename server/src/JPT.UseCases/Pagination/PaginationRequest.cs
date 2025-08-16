using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Pagination;

public abstract record PaginationRequest<TResponse> : IRequest<Result<PaginationResponse<TResponse>>> where TResponse : class
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}