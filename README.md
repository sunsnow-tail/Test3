# Test3

Create an MVC/Web API project that will consume the weather web service defined below: 

http://www.webservicex.net/globalweather.asmx?WSDL 

The User Interface should consist of two input fields: 

 Country 

 List of cities for given country 


When the user enters a country the program should query the web service 
API http://www.webservicex.net/globalweather.asmx?op=GetCitiesByCountry 
which will then populate the cities drop down list. 

Once the user selects a relevant city then this will query the web service API 
http://www.webservicex.net/globalweather.asmx?op=GetWeather 
which will then display the following information in read only format: 

 Location 

 Time

 Wind 

 Visibility 

 Sky conditions 

 Temperature 

 Dew Point 

 Relative Humidity 

 Pressure 


Please note if the above service didn’t return any data use the alternative web service (for example for London):  
http://api.openweathermap.org/data/2.5/weather?q=London 
