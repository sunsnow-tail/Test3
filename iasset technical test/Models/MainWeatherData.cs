using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountryWeather.Models
{
    public class MainWeatherData
    {
        /// <summary>
        /// temperature
        /// </summary>
        public string Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
    }
}
