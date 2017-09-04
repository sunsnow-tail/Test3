using CountryWeather.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CountryWeather.WebApi
{
    public class WeatherController : ApiController
    {
        private readonly Weather mockWeather = new Weather()
        {
            Coord = new LocationWeatherData
            {
                lat = "10",
                lon = "10"
            },
            DewPoint = "As usual",
            Main = new MainWeatherData
            {
                Humidity = "25",
                Pressure = "35",
                Temp = "18"
            },
            Location = "My city",
            Clouds = new CloudWeatherData{
                All="85"
            },
            Sys = new SysWeatherData
            {
                Country = "AU"
            },
            Time = DateTime.Now.ToLocalTime().ToString(),
            Visibility = "25",
            Wind = new Wind
            {
                Deg = "5",
                Speed = "25"
            }
        };

        // GET api/<controller>/5
        public WeatherData Get(string city, string country)
        {
            using (var client = new GlobalWeatherService.GlobalWeatherSoapClient())
            {
                client.Open();

                var data = client.GetWeather(city, country);

                //I did not find a city that returns weather

                if (data.Equals("Data Not Found"))
                {
                    client.Close();
                    
                    //call different service

                    //apiId is mandotary and it is for London
                    var uri = string.Format(ConfigurationManager.AppSettings["altService"], city);
                    var apiId = ConfigurationManager.AppSettings["altServiceId"];

                    uri += "&" + apiId;

                    using (HttpClient client2 = new HttpClient())
                    {
                        client2.BaseAddress = new Uri(uri);

                        // Add an Accept header for JSON format.
                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client2.GetAsync(uri).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            // Parse the response body. Blocking!
                            var weatherJson = response.Content.ReadAsStringAsync().Result;
                            var weather = JsonConvert.DeserializeObject<Weather>(weatherJson);

                            return new WeatherData(weather)
                            {
                                Location = city
                            };
                        }                        
                    }
                }
                
                //the first service has no weather
                //the second service fail.
                //let's mock some data
                return new WeatherData(mockWeather)
                {
                    Location = city
                };
            }
        }        
    }
}