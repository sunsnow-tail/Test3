using CountryWeather.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CountryWeather.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CountriesController : ApiController
    {
        // GET api/<controller>/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var uri = ConfigurationManager.AppSettings["countriesUrl"];

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var countries = response.Content.ReadAsStringAsync().Result;
                    var contryList = JsonConvert.DeserializeObject<List<Country>>(countries);

                    return contryList.Select(p => p.Name).Distinct();
                }
                else
                {
                    //the EU service fail.
                    //let's mock some data
                    return new List<string>{
                        "_Select a country",
                        "Australia",
                        "Russia",
                    };
                }
            }
        }
    }
}
