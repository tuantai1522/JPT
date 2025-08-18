using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Abstractions.Files;
using JPT.UseCases.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = JPT.Core.Features.Files.File;

namespace JPT.UseCases.Features.Users.JobApplications.GetApplicantsByJobId;

public class GetApplicantsByJobIdQueryHandler(
    IFileUrlResolver fileUrlResolver,
    IUserProvider userProvider,
    IUserRepository userRepository,
    IApplicationDbContext dbContext) : IRequestHandler<GetApplicantsByJobIdQuery, Result<PaginationResponse<GetApplicantsByJobIdResponse>>>
{
    public async Task<Result<PaginationResponse<GetApplicantsByJobIdResponse>>> Handle(GetApplicantsByJobIdQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Company);

        if (user is null)
        {
            return Result.Failure<PaginationResponse<GetApplicantsByJobIdResponse>>(UserErrors.NotFound(userId));
        }

        var job = await GetJobByIdAsync(query.Id, cancellationToken);

        if (job is null)
        {
            return Result.Failure<PaginationResponse<GetApplicantsByJobIdResponse>>(JobErrors.NotFound(query.Id));
        }

        if (job.CompanyId != user.Company?.Id)
        {
            return Result.Failure<PaginationResponse<GetApplicantsByJobIdResponse>>(JobErrors.AccessDenied(query.Id));
        }

        var result = await BuildResponse(query.Id, query.Page, query.PageSize, cancellationToken);

        result.Items = result.Items.Select(item =>
        {
            if (!string.IsNullOrEmpty(item.AvatarUrl))
            {
                item = item with { AvatarUrl = fileUrlResolver.Build(item.UploadAvatarType, item.AvatarUrl) };
            }

            if (!string.IsNullOrEmpty(item.CvUrl))
            {
                item = item with { CvUrl = fileUrlResolver.Build(item.UploadCvType, item.CvUrl) };
            }

            return item;
        }).ToList();
        return Result.Success(result);
    }

    private async Task<PaginationResponse<GetApplicantsByJobIdResponse>> BuildResponse(Guid jobId, int page, int pageSize, CancellationToken cancellationToken)
    {
        // Only job entity needs AsNoTracking
        var query =
            from ja   in dbContext.Set<JobApplication>().AsNoTracking()
            where ja.JobId == jobId                    
            join user in dbContext.Set<User>()
                on ja.ApplicantId equals user.Id

            from avFile in dbContext.Set<File>()
                .Where(f => user.AvatarId != null && f.Id == user.AvatarId)
                .DefaultIfEmpty()

            join cvFile in dbContext.Set<File>()
                on ja.CvId equals cvFile.Id

            orderby ja.AppliedAt descending
            select new GetApplicantsByJobIdResponse(
                user.Id,
                user.FirstName,
                user.MiddleName,
                user.LastName,
                user.Email,
                avFile.UploadType,
                avFile.Path,
                cvFile.UploadType,
                cvFile.Path,
                jobId,
                ja.Id,
                ja.Status.ToString(),
                ja.AppliedAt
            );

        return await PaginationResponse<GetApplicantsByJobIdResponse>.CreateAsync(query, page, pageSize, cancellationToken);
    }

    private async Task<Job?> GetJobByIdAsync(Guid jobId, CancellationToken cancellationToken)
        => await dbContext.Set<Job>()
            .FirstOrDefaultAsync(job => job.Id == jobId, cancellationToken);
}