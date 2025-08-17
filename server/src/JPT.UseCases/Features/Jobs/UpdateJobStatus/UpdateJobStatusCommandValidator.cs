using FluentValidation;

namespace JPT.UseCases.Features.Jobs.UpdateJobStatus;

internal sealed class UpdateJobStatusCommandValidator : AbstractValidator<UpdateJobStatusCommand>
{
    public UpdateJobStatusCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.");
        
        RuleFor(c => c.Status)
            .IsInEnum().WithMessage("Status must be 'Active' or 'Closed'.");
    }
}