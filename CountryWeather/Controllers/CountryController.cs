using System.Web.Mvc;
using CountryWeather.Models;
using System.Collections.Generic;
using CountryWeather.WebApi;
using System.Linq;

namespace CountryWeather.Controllers
{
    public class CountryController : Controller
    {
        private ICountry _country;

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
            return View(new CountryWeatherInformation());
        }

        [HttpGet]
        public ActionResult TestHtml()
        {
            return View(_country.Countries);
        }

        [HttpPost]
        public ActionResult GetCountryData()
        {
            var selectedCountry = Request.Form["inputCountry"];

            var returnInfo = new CountryWeatherInformation();
            returnInfo.Country = selectedCountry;
            var cities = new CitiesController();
            returnInfo.Cities = cities.Get(selectedCountry).ToList();

            var selectedCity = Request.Form["SelectedCity"];
            returnInfo.SelectedCity = selectedCity != null && selectedCity.Contains("_Select") ? null : selectedCity;

            if (!string.IsNullOrWhiteSpace(returnInfo.SelectedCity) && returnInfo.Cities.Contains(returnInfo.SelectedCity))
            {
                var weather = new WeatherController();

                returnInfo.WeatherInfo = weather.Get(returnInfo.SelectedCity, selectedCountry);
            }

            return View("TestRazor", returnInfo);
        }
    }
}