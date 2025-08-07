using JPT.Core.Common;

namespace JPT.Core.Features.Countries;

public sealed class Country : IAggregateRoot
{
    public int Id { get; init; }
    
    public string Name { get; private set; } = null!;

    private readonly List<City> _cities = [];
    
    public IReadOnlyList<City> Cities => _cities.ToList();

    private Country()
    {
        
    }

    /// <summary>
    /// Create new country
    /// </summary>
    /// <returns></returns>
    public static Country Create(string name)
    {
        return new Country
        {
            Name = name,
        };
    }

    /// <summary>
    /// Add city into list of cities
    /// </summary>
    /// <param name="city"></param>
    public Result<City> AddCity(City city)
    {
        _cities.Add(city);
        
        return Result.Success(city);
    }
}