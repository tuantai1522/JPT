using FluentValidation;

namespace JPT.UseCases.Features.Users.RemoveCv;

internal sealed class RemoveCvCommandValidator : AbstractValidator<RemoveCvCommand>
{
    public RemoveCvCommandValidator()
    {
        RuleFor(c => c.FileId)
            .NotNull().WithMessage("FileId is required.");
    }
}
