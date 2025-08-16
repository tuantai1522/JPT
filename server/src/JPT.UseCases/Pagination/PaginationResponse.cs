using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Pagination;

public class PaginationResponse<TResponse> where TResponse : class
{
    private PaginationResponse(IReadOnlyList<TResponse> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }


    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;

    public IReadOnlyList<TResponse> Items { get; set; }

    public static async Task<PaginationResponse<TResponse>> CreateAsync(IQueryable<TResponse> query, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PaginationResponse<TResponse>(items, page, pageSize, totalCount);
    }
}