using FluentValidation;

namespace JPT.UseCases.Features.Users.Cvs.RemoveCv;

internal sealed class RemoveCvCommandValidator : AbstractValidator<RemoveCvCommand>
{
    public RemoveCvCommandValidator()
    {
        RuleFor(c => c.FileId)
            .NotEmpty().WithMessage("FileId is required.");
    }
}
