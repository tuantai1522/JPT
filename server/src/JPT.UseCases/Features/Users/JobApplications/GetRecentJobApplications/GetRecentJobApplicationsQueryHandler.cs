using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = JPT.Core.Features.Files.File;

namespace JPT.UseCases.Features.Users.JobApplications.GetRecentJobApplications;

public class GetRecentJobApplicationsQueryHandler(
    IUserRepository userRepository,
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<GetRecentJobApplicationsQuery, Result<PaginationResponse<GetRecentJobApplicationsResponse>>>
{
    public async Task<Result<PaginationResponse<GetRecentJobApplicationsResponse>>> Handle(GetRecentJobApplicationsQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;
        
        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Company);

        if (user is null || user.Role == UserRole.JobSeeker)
        {
            return Result.Failure<PaginationResponse<GetRecentJobApplicationsResponse>>(JobErrors.UnauthorizedJobSeeker);
        }

        var result = await BuildResponse(userId, query.Page, query.PageSize, cancellationToken);

        return Result.Success(result);
    }

    private async Task<PaginationResponse<GetRecentJobApplicationsResponse>> BuildResponse(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query =
            from company in dbContext.Set<Company>().AsNoTracking()
            where company.EmployerId == userId

            join job in dbContext.Set<Job>().AsNoTracking()
                on company.Id equals job.CompanyId

            join ja in dbContext.Set<JobApplication>().AsNoTracking()
                on job.Id equals ja.JobId

            join applicant in dbContext.Set<User>().AsNoTracking()
                on ja.ApplicantId equals applicant.Id

            from avFile in dbContext.Set<File>()
                .Where(f => applicant.AvatarId != null && f.Id == applicant.AvatarId)
                .DefaultIfEmpty()

            orderby ja.AppliedAt descending
            
            select new GetRecentJobApplicationsResponse(
                ja.Id,
                applicant.FirstName,
                applicant.MiddleName,
                applicant.LastName,
                avFile.Path,
                job.Title,
                ja.AppliedAt
            );

        return await PaginationResponse<GetRecentJobApplicationsResponse>.CreateAsync(query, page, pageSize, cancellationToken);
    }
}