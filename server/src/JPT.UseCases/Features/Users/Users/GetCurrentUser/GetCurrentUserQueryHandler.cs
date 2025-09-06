using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.GetCurrentUser;

internal sealed class GetCurrentUserQueryHandler(
    IUserProvider userProvider,
    IFileRepository fileRepository,
    IUserRepository userRepository): IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse>>
{
    public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;
        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Company);
        
        if (user is null)
        {
            return Result.Failure<GetCurrentUserResponse>(UserErrors.NotFoundByEmail);
        }

        string? avatarUrl = null;

        if (user.AvatarId.HasValue)
        {
            var avatar = await fileRepository.GetFileByIdAsync(user.AvatarId.Value, cancellationToken);

            if (avatar != null)
            {
                avatarUrl = avatar.Path;
            }
        }
        var response = new GetCurrentUserResponse(user.Id, user.FirstName, user.Role.ToString(), user.Email, avatarUrl, user.Company?.Name);
        
        return Result.Success(response);
    }
}
