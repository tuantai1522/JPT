using FluentValidation;

namespace JPT.UseCases.Features.Countries.GetCitiesByCountryId;

internal sealed class GetCitiesByCountryIdQueryValidator : AbstractValidator<GetCitiesByCountryIdQuery>
{
    public GetCitiesByCountryIdQueryValidator()
    {
        RuleFor(c => c.CountryId)
            .NotEmpty().WithMessage("Country ID can not be empty.");
    }
}
