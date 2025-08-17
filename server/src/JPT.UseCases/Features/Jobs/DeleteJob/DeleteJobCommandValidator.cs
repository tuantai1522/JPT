using FluentValidation;

namespace JPT.UseCases.Features.Jobs.DeleteJob;

internal sealed class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}