using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.RefreshToken;

public sealed record RefreshTokenQuery : IRequest<Result<RefreshTokenResponse>>;
