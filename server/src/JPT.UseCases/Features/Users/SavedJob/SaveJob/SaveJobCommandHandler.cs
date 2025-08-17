using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Users.SavedJob.SaveJob;

internal sealed class SaveJobCommandHandler(
    IApplicationDbContext dbContext,
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
    IUserRepository userRepository) : IRequestHandler<SaveJobCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(SaveJobCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.SavedJobs);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }
        
        var verifyExistedJob = await VerifyExistedJobByIdAsync(command.JobId, cancellationToken);

        if (!verifyExistedJob)
        {
            return Result.Failure<Guid>(JobErrors.NotFound(command.JobId));
        }

        var result = user.SaveJob(command.JobId);

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
