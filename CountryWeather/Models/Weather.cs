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
        public string Name { get; set; } // = "not provided";
        public LocationWeatherData Coord { get; set; }
        public string Time { get; set; }
        public Wind Wind { get; set; }
        public string Visibility { get; set; }
        public CloudWeatherData Clouds { get; set; }
        public string DewPoint { get; set; }
        public MainWeatherData Main { get; set; }
    }
}