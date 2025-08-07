namespace JPT.Core.Features.Countries;

public sealed class City
{
    public int Id { get; init; }

    public string Name { get; private set; } = null!;
    
    public Guid CountryId { get; private set; }
    public Country Country { get; private set; } = null!;

    private City()
    {
        
    }

    public static City Create(string name, Guid countryId)
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