using FluentValidation;

namespace JPT.UseCases.Features.Users.JobApplications.GetJobApplicationById;

internal sealed class GetJobApplicationByIdQueryValidator : AbstractValidator<GetJobApplicationByIdQuery>
{
    public GetJobApplicationByIdQueryValidator()
    {
        RuleFor(c => c.JobApplicationId)
            .NotEmpty().WithMessage("JobApplicationId is required.");
    }
}
