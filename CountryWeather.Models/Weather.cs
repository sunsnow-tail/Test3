using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountryWeather.Models
{
    /// <summary>
    /// structure for incomming data
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// keeps system info
        /// </summary>
        public SysWeatherData Sys { get; set; }
        /// <summary>
        /// Location - City
        /// </summary>
        public string Location { get; set; }
        public LocationWeatherData Coord { get; set; }
        public string Time { get; set; }
        public Wind Wind { get; set; }
        public string Visibility { get; set; }
        public CloudWeatherData Clouds { get; set; }
        public string DewPoint { get; set; }
        public MainWeatherData Main { get; set; }
    }

    public class WeatherData
    {
        public WeatherData(Weather weather)
        {
            Location = weather.Location;
            Humidity = weather.Main?.Humidity;
            Time = weather.Time;
            WindSpeed = weather.Wind?.Speed;
            Visibility = weather.Visibility;
            Clouds = weather.Clouds?.All;
            DewPoint = weather.DewPoint;
            Temperature = weather.Main?.Temp;
            Pressure = weather.Main?.Pressure;
        }

        public WeatherData()
        {
        }

        /// <summary>
        /// Location - City
        /// </summary>
        public string Location { get; set; } = "not provided";
        public string Humidity { get; set; }
        public string Time { get; set; }
        public string WindSpeed { get; set; }
        public string Visibility { get; set; }
        public string Clouds { get; set; }
        public string DewPoint { get; set; }
        public string Temperature { get; set; }
        public string Pressure { get; set; }
    }
}