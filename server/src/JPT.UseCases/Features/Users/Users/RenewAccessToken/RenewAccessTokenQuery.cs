using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.RenewAccessToken;

public sealed record RenewAccessTokenQuery : IRequest<Result<RenewAccessTokenResponse>>;
