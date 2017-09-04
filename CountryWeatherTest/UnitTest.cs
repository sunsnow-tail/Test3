using System.Linq;
using NUnit.Framework;

namespace CountryWeatherTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void CanGetCountries()
        {
            var countries = new CountryWeather.Data.CountryData();

            Assert.IsTrue(countries.Countries.Any());
        }

        [Test]
        public void CanGetCities()
        {
            var controller = new CountryWeather.WebApi.CitiesController();

            var cities = controller.Get("Russia");

            Assert.IsTrue(cities.Any());
        }

        [TestCase("Australia", "Canberra", ExpectedResult = true)]
        [TestCase("Australia", "Brisbane", ExpectedResult = true)]
        [TestCase("Australia", "Sydney", ExpectedResult = true)]
        [TestCase("Russia", "Moscow", ExpectedResult = true)]
        [TestCase("Russia", "Habarovsk", ExpectedResult = true)]
        [TestCase("Russia", "Sydney", ExpectedResult = false)]
        [TestCase("Russia", "Berlin", ExpectedResult = false)]
        [TestCase("Russia", "London", ExpectedResult = false)]
        public bool CanGetCitiesForCountry(string country, string city)
        {
            var controller = new CountryWeather.WebApi.CitiesController();

            var cities = controller.Get(country);

            return cities.Any(p => p.Contains(city));
        }

        [TestCase("UK","London", ExpectedResult = true)]
        [TestCase("Australia", "Sydney", ExpectedResult = true)]
        [TestCase("Russia", "Moscow", ExpectedResult = true)]
        [TestCase("Russia", "Habarovsk", ExpectedResult = true)]
        public bool CanGetWeather(string country, string city)
        {
            var controller = new CountryWeather.WebApi.WeatherController();

            var weather = controller.Get(city, country);

            return weather != null;
        }

        [TestCase("UK", "London", ExpectedResult = true)]
        //[TestCase("Canberra", ExpectedResult = true)] - unfortunately, service can get any data but for London now
        public bool CanGetWeatherForCountry(string country, string city)
        {
            var controller = new CountryWeather.WebApi.WeatherController();

            var weather = controller.Get(city, "Australia");

            return weather.Location == city;
        }

        [TestCase("London", "<City>London</City>", ExpectedResult = true)]
        [TestCase("Moscow", "<City>Moscow</City>", ExpectedResult = true)]
        [TestCase("Not", "<City>Not</City>", ExpectedResult = true)]
        [TestCase("HH BB", "<City>HH BB</City>", ExpectedResult = true)]
        [TestCase("My big city name", "<City>My big city name</City>", ExpectedResult = true)]
        [TestCase("My big city name2", "<City>My big city name</City>", ExpectedResult = false)]
        [TestCase("My big city name", "<City>My big city name2</City>", ExpectedResult = false)]
        [TestCase("My big city name", "<City>My big name</City>", ExpectedResult = false)]
        public bool TestCityParsing(string city, string parseLine)
        {
            var controller = new CountryWeather.WebApi.CitiesController();

            var result = controller.TestGetCity(parseLine);

            return (result == city);
        }
    }
}
