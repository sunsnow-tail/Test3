using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace CountryWeatherTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CanGetCountries()
        {
            var countries = new CountryWeather.Data.RestCountries();

            Assert.IsTrue(countries.Countries.Any());
        }

        [TestMethod]
        public void CanGetCities()
        {
            var controller = new CountryWeather.WebApi.CitiesController();

            var cities = controller.Get("Russia");

            Assert.IsTrue(cities.Any());
        }

        [TestMethod]
        public void CanGetCitiesForCountry()
        {
            var controller = new CountryWeather.WebApi.CitiesController();

            var cities = controller.Get("Australia");

            Assert.IsTrue(cities.Contains("Canberra"));
        }

        [TestMethod]
        public void CanGetWeather()
        {
            var controller = new CountryWeather.WebApi.WeatherController();

            var weather = controller.Get("Canberra", "Australia");

            Assert.IsTrue(weather != null);
        }

        [TestMethod]
        public void CanGetWeatherForCountry()
        {
            var city = "Canberra";

            var controller = new CountryWeather.WebApi.WeatherController();

            var weather = controller.Get(city, "Australia");

            Assert.IsTrue(weather.Name == city);
        }

        [TestMethod]
        public void TestCityParsing()
        {
            var testData = new Dictionary<string,string>();
            testData.Add("London", "<City>London</City>");
            testData.Add("Moscow", "<City>Moscow</City>");
            testData.Add("Not", "<City>Not</City>");
            testData.Add("HH BB", "<City>HH BB</City>");
            testData.Add("My big city name", "<City>My big city name</City>");

            var controller = new CountryWeather.WebApi.CitiesController();

            foreach(var line in testData){
                var result = controller.TestGetCity(line.Value);

                Assert.IsTrue(result == line.Key);
            }            

        }
    }
}
