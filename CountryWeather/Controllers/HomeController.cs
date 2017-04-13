using System.Web.Mvc;
using CountryWeather.Models;

namespace CountryWeather.Controllers
{
    public class HomeController : Controller
    {
        private ICountry _country;

        public HomeController(ICountry countries)
        {
            _country = countries;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test()
        {          
            return View(_country.Countries);
        }


    }
}