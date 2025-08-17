using FluentValidation;

namespace JPT.UseCases.Features.Users.SavedJobs.SaveJob;

internal sealed class SaveJobCommandValidator : AbstractValidator<SaveJobCommand>
{
    public SaveJobCommandValidator()
    {
        RuleFor(c => c.JobId)
            .NotEmpty().WithMessage("JobId is required.");
    }
}
