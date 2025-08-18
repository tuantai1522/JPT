using FluentValidation;

namespace JPT.UseCases.Features.Users.JobApplications.GetApplicantsByJobId;

internal sealed class GetApplicantsByJobIdQueryValidator : AbstractValidator<GetApplicantsByJobIdQuery>
{
    public GetApplicantsByJobIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
