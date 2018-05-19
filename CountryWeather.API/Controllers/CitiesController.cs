using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;

namespace CountryWeather.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CitiesController : ApiController
    {
        // GET api/<controller>/5
        public IEnumerable<string> Get(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return new List<string>();
            }

            var root = ConfigurationManager.AppSettings["rootName"];

            try
            {
                using (var client = new GlobalWeatherService.GlobalWeatherSoapClient())
                {
                    client.Open();

                    var dataXml = client.GetCitiesByCountry(country);

                    var xml = XDocument.Parse(dataXml);

                    //NewDataSet comes from http://www.webservicex.net/globalweather.asmx/GetCitiesByCountry
                    var items = xml.Descendants(root).Elements();

                    var returnData = items.Select(p => GetCity(p.ToString())).ToList();
                    returnData.Add(" Select a city");

                    return returnData.Distinct().OrderBy(p => p);
                }
            }
            catch
            {
                return new List<string>
                {
                    "Select a city...",
                    "London",
                    "Moscow",
                    "Brisbane"
                };
            }
        }

        [NonAction]
        public string TestGetCity(string data)
        {
            return GetCity(data);
        }

        /// <summary>
        /// gets city from <city>...</city>
        /// </summary>
        private string GetCity(string data)
        {
            //6 is <City>
            var startString = data.Substring(data.IndexOf("<City>") + 6);

            return startString.Substring(0, startString.IndexOf("</City>"));
        }
    }
}
