using System.Web.Mvc;
using CountryWeather.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;

namespace CountryWeather.Controllers
{
    public class CountryController : Controller
    {
        private ICountry _country;

        private static string _apiUrl = ConfigurationManager.AppSettings["apiUrl"];

        public CountryController(ICountry countries)
        {
            //get default countries
            _country = countries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TestRazor()
        {         
            //user will type a country 
            return View(new CountryWeatherInformation());
        }

        [HttpGet]
        public ActionResult TestHtml()
        {
            //show a list of countries
            return View(_country.Countries);
        }

        [HttpPost]
        public ActionResult GetCountryData()
        {
            var selectedCountry = Request.Form["inputCountry"];

            var returnInfo = new CountryWeatherInformation();
            returnInfo.Country = selectedCountry;
            returnInfo.Cities = GetCities(selectedCountry);

            var selectedCity = Request.Form["SelectedCity"];
            returnInfo.SelectedCity = selectedCity != null && selectedCity.Contains("_Select") ? null : selectedCity;

            if (!string.IsNullOrWhiteSpace(returnInfo.SelectedCity) && returnInfo.Cities.Contains(returnInfo.SelectedCity))
            {
                returnInfo.WeatherInfo = GetWeather(returnInfo.SelectedCity, selectedCountry);
            }

            return View("TestRazor", returnInfo);
        }

        private WeatherData GetWeather(string city, string country)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_apiUrl}api/Weather/?city={city}&country={country}";

                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var weatherJson = response.Content.ReadAsStringAsync().Result;
                    var weather = JsonConvert.DeserializeObject<WeatherData>(weatherJson);

                    return weather;
                }

                return new WeatherData();
            }
        }

        private List<string> GetCities(string country)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_apiUrl}api/Cities/?country={country}";

                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var citiesJson = response.Content.ReadAsStringAsync().Result;
                    var cities = JsonConvert.DeserializeObject<List<string>>(citiesJson);

                    return cities;
                }

                return new List<string>();
            }
        }
    }
}