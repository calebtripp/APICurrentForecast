using System.Runtime.Intrinsics.X86;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net;
using System.Text.Json;

namespace APICurrentForecast
{
    internal class Program
    {   
            static void Main(string[] args)
            {
                var client = new HttpClient();
                var city = "Harrisburg";
                var state = "PA";
                var country = "USA";

            //GEOLOCATOR//////////////////////////

            var locatorUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{state},{country}&limit={1}&appid={"269fcd68b6d3d0d501d5f58d3d61fc61"}&units=imperial";
            var apiGeoResponse = client.GetStringAsync(locatorUrl).Result;
            var lat = "";
            var lon = "";

            JArray parsedApi = JArray.Parse(apiGeoResponse);      

            foreach (JObject jObject in parsedApi)
            {
                lat = $"{(string)jObject["lat"]}";
                lon = $"{(string)jObject["lon"]}";
            }
             
            //using (JsonDocument apiGeoResponse = JsonDocument.Parse(json, options))
            //{
            //    int sumOfAllTemperatures = 0;
            //    int count = 0;

            //    foreach (JsonElement element in document.RootElement.EnumerateArray())
            //    {
            //        DateTimeOffset date = element.GetProperty("date").GetDateTimeOffset();
            //        (...)

            // these two need the array response parsed first. 
            //    var lat = JArray.Parse(apiGeoResponse).GetValue("lat").ToString();
            // var lon = JArray.Parse(apiGeoResponse).GetValue("lon").ToString();         


            //FORECAST///////////////////////////

            var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={"269fcd68b6d3d0d501d5f58d3d61fc61"}";
            var apiWeaResponse = client.GetStringAsync(weatherUrl).Result;
            Console.WriteLine(apiWeaResponse);
            //will need to parse out current forecast. 

            //
            //  JArray FromObject(object o)
            //  var forecast = 

            //Console.WriteLine($"{apiWeaResponse}");

        }

    }
}
//need to add other api


//C Cre
//R
//U
//D


//Array - Array.parse
//obj obj . parse



//api key 269fcd68b6d3d0d501d5f58d3d61fc61



//Use thex
//OpenWeatherMap APIx
//to find out what the current weather forecast is!
//Create an OpenWeatherMap account to get a free API Keyx
//Add the NewtonSoft.Json Nuget Package to your Console Appx
//Figure out how to get the CURRENT weather for the City you specify in degrees fahrenheit
//Use this site for extra help:
//https://openweathermap.org/current
//Creatively display &organize the response
//Hint: For more info on Units of Measurement visit: