using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Files;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository, 
    IFileUrlResolver fileUrlResolver,
    IFileRepository fileRepository): IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
{
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(query.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetUserByIdResponse>(UserErrors.NotFoundByEmail);
        }

        var fileIds = user.Cvs.Select(cv => cv.CvId).ToList();

        var files = await fileRepository.GetFilesByIdAsync(fileIds, cancellationToken);
        
        // Todo: To add actual data
        var response = new GetUserByIdResponse(
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.Description,
            null,
            user.Role.ToString(),
            null,
            null,
            null,
            files.Select(file => fileUrlResolver.Build(file.UploadType, file.Path)).ToList());
        
        return Result.Success(response);
    }
}
