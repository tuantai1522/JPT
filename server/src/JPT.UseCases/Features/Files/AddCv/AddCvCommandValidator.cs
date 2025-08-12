using FluentValidation;

namespace JPT.UseCases.Features.Files.AddCv;

internal sealed class AddCvCommandValidator : AbstractValidator<AddCvCommand>
{
    public AddCvCommandValidator()
    {
        RuleFor(c => c.FileId)
            .NotNull().WithMessage("FileId is required.");
    }
}
