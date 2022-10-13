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
            var locatorUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{state},{country}&limit={1}&appid={"269fcd68b6d3d0d501d5f58d3d61fc61"}";
            var apiGeoResponse = client.GetStringAsync(locatorUrl).Result;

            var lat = "";
            var lon = "";
            JArray parsedApi = JArray.Parse(apiGeoResponse);
            foreach (JObject jObject in parsedApi)
            {
                lat = $"{(string)jObject["lat"]}";
                lon = $"{(string)jObject["lon"]}";
            }


            //FORECAST////////////////////////////
            var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={"269fcd68b6d3d0d501d5f58d3d61fc61"}&units=imperial";
            var apiWeaResponse = client.GetStringAsync(weatherUrl).Result;

            var main = JObject.Parse(apiWeaResponse).GetValue("main").ToString();
                var temp = JObject.Parse(main).GetValue("temp").ToString();
                var tempMin = JObject.Parse(main).GetValue("temp_min").ToString();
                var tempMax = JObject.Parse(main).GetValue("temp_max").ToString();
                var feelsLike = JObject.Parse(main).GetValue("feels_like").ToString();
                var humidity = JObject.Parse(main).GetValue("humidity").ToString();

            var wind = JObject.Parse(apiWeaResponse).GetValue("wind").ToString();
                var speed = JObject.Parse(wind).GetValue("speed").ToString();

            var weather = JObject.Parse(apiWeaResponse).GetValue("weather").ToString();
                var conditions = "";

            JArray parsedWeather = JArray.Parse(weather);
            foreach (JObject jObject in parsedWeather)
            {
                conditions = $"{(string)jObject["description"]}";
            }

            var forecast = $"The current weather forecast in {city} is: \n" +
                $"{conditions}\n" +
                $"Temperature: {temp}\n" +
                $"Low: {tempMin}\n" +
                $"High: {tempMax}\n" +
                $"Feels Like: {feelsLike}\n" +
                $"Humidity: {humidity}\n" +
                $"Wind Speed: {speed}\n";

            Console.WriteLine(forecast);
            //Not clean, need to move forecast and geolocator to classes and find a way to more efficiently parse. Serialize? JSON document?
        }
    }
}