using CountryWeather.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CountryWeather.Data
{
    /// <summary>
    /// a class with ccountries
    /// it can be any class that implemets ICountry
    /// </summary>
    public class RestCountries : ICountry
    {
        private IEnumerable<Country> _countries;

        public RestCountries()
        {
            _countries = GetCountries().OrderBy(p=>p.Name);
        }

        private List<Country> GetCountries()
        {
            var uri = ConfigurationManager.AppSettings["cityUrl"];

            HttpClient client = new HttpClient();
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
                contryList.Add(new Country { Name = "_Select a country" });

                client.Dispose();

                return contryList;

            }
            else
            {
                client.Dispose();

                //the EU service fail.
                //let's mock some data
                return new List<Country>{
                    new Country{Name="_Select a country"},
                    new Country{Name="Australia"},
                    new Country{Name="Russia"},
                };
            }

        }


        public IEnumerable<Country> Countries
        {
            get
            {
                return _countries;
            }
        }
    }
}