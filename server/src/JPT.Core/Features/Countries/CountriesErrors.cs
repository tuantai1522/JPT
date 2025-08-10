using JPT.Core.Common;

namespace JPT.Core.Features.Countries;

public static class CountriesErrors
{
    public static Error NotFound(int countryId) => Error.NotFound(
        "Users.NotFound",
        $"The country with the Id = '{countryId}' was not found");
}
