using FluentValidation;

namespace JPT.UseCases.Features.Jobs.UpdateJobApplicationStatus;

internal sealed class UpdateJobApplicationStatusCommandValidator : AbstractValidator<UpdateJobApplicationStatusCommand>
{
    public UpdateJobApplicationStatusCommandValidator()
    {
        RuleFor(c => c.JobId)
            .NotEmpty().WithMessage("JobId is required.");
        
        RuleFor(c => c.JobApplicationId)
            .NotEmpty().WithMessage("JobApplicationId is required.");
        
        RuleFor(c => c.Status)
            .IsInEnum().WithMessage("Invalid Job Application Status.");
    }
}
