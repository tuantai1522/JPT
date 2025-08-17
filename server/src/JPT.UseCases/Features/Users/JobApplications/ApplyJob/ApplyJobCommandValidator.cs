using FluentValidation;

namespace JPT.UseCases.Features.Users.JobApplications.ApplyJob;

internal sealed class ApplyJobCommandValidator : AbstractValidator<ApplyJobCommand>
{
    public ApplyJobCommandValidator()
    {
        RuleFor(c => c.CvId)
            .NotEmpty().WithMessage("CvId is required.");
        
        RuleFor(c => c.JobId)
            .NotEmpty().WithMessage("JobId is required.");
    }
}
