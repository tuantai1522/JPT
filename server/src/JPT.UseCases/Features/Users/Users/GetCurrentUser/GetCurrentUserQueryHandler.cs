using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.GetCurrentUser;

internal sealed class GetCurrentUserQueryHandler(
    IUserProvider userProvider,
    IUserRepository userRepository, 
    ITokenProvider tokenProvider): IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse>>
{
    public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;
        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<GetCurrentUserResponse>(UserErrors.NotFoundByEmail);
        }
        
        string accessToken = tokenProvider.CreateAccessToken(user);

        var response = new GetCurrentUserResponse(
            user.Id,
            user.FirstName,
            user.Role.ToString(),
            accessToken
        );
        
        return Result.Success(response);
    }
}
