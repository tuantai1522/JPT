using FluentValidation;

namespace JPT.UseCases.Features.Users.LogIn;

internal sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email can not be empty.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password can not be empty.");
    }
}
