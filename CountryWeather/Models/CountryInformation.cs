using System.Collections.Generic;

namespace CountryWeather.Models
{
    public class CountryWeatherInformation
    {
        public string Country { get; set; }

        public string SelectedCity { get; set; }

        public List<string> Cities { get; set; }

        public WeatherData WeatherInfo { get; set; }
    }
}