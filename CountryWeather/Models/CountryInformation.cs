using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryWeather.Models
{
    public class CountryWeatherInformation
    {
        public string Country { get; set; }

        public string SelectedCity { get; set; }

        public List<string> Cities { get; set; }

        public Weather WeatherInfo { get; set; }
    }
}