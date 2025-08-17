using FluentValidation;

namespace JPT.UseCases.Features.Users.SavedJobs.UnSaveJob;

internal sealed class UnSaveJobCommandValidator : AbstractValidator<UnSaveJobCommand>
{
    public UnSaveJobCommandValidator()
    {
        RuleFor(c => c.JobId)
            .NotEmpty().WithMessage("JobId is required.");
    }
}
