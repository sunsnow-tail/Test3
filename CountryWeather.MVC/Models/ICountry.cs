using System.Collections.Generic;

namespace CountryWeather.Models
{
    /// <summary>
    /// interface for country data
    /// </summary>
    public interface ICountry
    {
        IEnumerable<Country> Countries { get; }
    }
}