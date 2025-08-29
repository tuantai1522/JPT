using FluentValidation;

namespace JPT.Infrastructure.Options.Jwts
{
    public sealed class JwtOptionsValidator : AbstractValidator<JwtOptions>
    {
        public JwtOptionsValidator()
        {
            RuleFor(x => x.AccessTokenKey)
                .NotEmpty().WithMessage("JwtOptions:AccessTokenKey is required");
            
            RuleFor(x => x.RefreshTokenKey)
                .NotEmpty().WithMessage("JwtOptions:RefreshTokenKey is required");
            
            RuleFor(x => x.Issuer)
                .NotEmpty().WithMessage("JwtOptions:Issuer is required");
            
            RuleFor(x => x.Audience)
                .NotEmpty().WithMessage("JwtOptions:Audience is required");
            
            RuleFor(x => x.ExpiredAccessToken)
                .NotNull().WithMessage("JwtOptions:ExpiredAccessToken is required");
            
            RuleFor(x => x.ExpiredRefreshToken)
                .NotNull().WithMessage("JwtOptions:ExpiredRefreshToken is required");
        }
    }
}

