using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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