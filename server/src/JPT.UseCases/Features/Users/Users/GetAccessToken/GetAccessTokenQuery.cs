using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.GetAccessToken;

public sealed record GetAccessTokenQuery : IRequest<Result<GetAccessTokenResponse>>;
