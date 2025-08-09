using FluentValidation;
using JPT.Core.Features.Users;

namespace JPT.UseCases.Features.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name can not be empty.");
        
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email can not be empty.");
        
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password can not be empty.");
        
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role must be 'Employee' or 'JobSeeker'.");
        
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Company Name is required when registered with JobSeeker role.")
            .When(x => x.Role == UserRole.JobSeeker);
    }
}
