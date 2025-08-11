using FluentValidation;
using JPT.Core.Features.Users;

namespace JPT.UseCases.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name can not be empty.");

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company Name can not be empty.")
            .When(x => x.Role == UserRole.Employer);

    }
}
