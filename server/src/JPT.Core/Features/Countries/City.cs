using JPT.Core.Common;

namespace JPT.Core.Features.Countries;

public sealed class City : IBaseEntity
{
    public int Id { get; init; }

    public string Name { get; private set; } = null!;
    
    public int CountryId { get; private set; }

    private City()
    {
        
    }

    public static City Create(string name, int countryId)
    {
        return new City
        {
            Name = name,
            CountryId = countryId
        };
    }

    public void Update(string cityName)
    {
        Name = cityName;
    }
}