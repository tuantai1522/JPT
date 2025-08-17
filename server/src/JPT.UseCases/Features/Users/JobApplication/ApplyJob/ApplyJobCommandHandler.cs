using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Users.JobApplication.ApplyJob;

internal sealed class ApplyJobCommandHandler(
    IApplicationDbContext dbContext,
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
    IFileRepository fileRepository,
    IUserRepository userRepository) : IRequestHandler<ApplyJobCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(ApplyJobCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.JobApplications);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }
        
        var file = await fileRepository.GetFileByIdAsync(command.CvId, cancellationToken);
        
        if (file is null || file.IsDeleted)
        {
            return Result.Failure<Guid>(FileErrors.NotFound(command.CvId));
        }

        var verifyExistedJob = await VerifyExistedJobByIdAsync(command.JobId, cancellationToken);

        if (!verifyExistedJob)
        {
            return Result.Failure<Guid>(JobErrors.NotFound(command.JobId));
        }

        var result = user.ApplyJob(command.JobId, command.CvId);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return userId;
    }
    
    private async Task<bool> VerifyExistedJobByIdAsync(Guid jobId, CancellationToken cancellationToken)
        => await dbContext.Set<Job>()
            .AnyAsync(job => job.Id == jobId, cancellationToken);
}
