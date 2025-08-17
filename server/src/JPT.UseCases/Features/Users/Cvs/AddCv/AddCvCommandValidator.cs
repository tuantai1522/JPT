using FluentValidation;

namespace JPT.UseCases.Features.Users.Cvs.AddCv;

internal sealed class AddCvCommandValidator : AbstractValidator<AddCvCommand>
{
    public AddCvCommandValidator()
    {
        RuleFor(c => c.FileId)
            .NotEmpty().WithMessage("FileId is required.");
    }
}
