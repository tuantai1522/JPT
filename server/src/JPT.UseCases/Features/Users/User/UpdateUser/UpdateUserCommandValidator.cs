using FluentValidation;

namespace JPT.UseCases.Features.Users.User.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name can not be empty.");
    }
}
