using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.IO;

namespace APICurrentForecast
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key).GetValue("APIKey").ToString();

            Console.WriteLine("Please enter the name of your city:");
            string userCity = Console.ReadLine();

            Console.WriteLine("Please enter the two letter abbreviation for your state:");
            string userState = Console.ReadLine();                  
            

            var client = new HttpClient();
            var city = userCity;
            var state = userState;
            string country = "USA";


            //GEOLOCATOR////////////////////////// Move to it's own class?
            var locatorUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{state},{country}&limit={1}&appid={APIKey}";
            var apiGeoResponse = client.GetStringAsync(locatorUrl).Result;

            var lat = "";
            var lon = "";
            JArray parsedApi = JArray.Parse(apiGeoResponse);
            foreach (JObject jObject in parsedApi)
            {
                lat = $"{(string)jObject["lat"]}";
                lon = $"{(string)jObject["lon"]}";            }


            //FORECAST//////////////////////////// Move to it's own class?
            //var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIKey}&units=imperial";
            //var apiWeaResponse = client.GetStringAsync(weatherUrl).Result;


            //var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIKey}&units=imperial";
            //var apiWeaResponse = client.GetStringAsync(weatherUrl).Result;

            //try
            //{
            //    var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIKey}&units=imperial";
            //    var apiWeaResponse = client.GetStringAsync(weatherUrl).Result;
            //}

            //catch (System.AggregateException)
            //{

            //    Console.WriteLine("We were not able to find that location in the USA, please confirm your city and state spelling and try again ");
            //}

            var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIKey}&units=imperial";
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

            var forecast = $"\nThe current weather forecast in {userCity} is:\n" +
                $"{conditions}\n" +
                $"Temperature: {temp}\n" +
                $"Feels Like: {feelsLike}\n" +
                $"Today's Low: {tempMin}\n" +
                $"Today's High: {tempMax}\n" +
                $"Wind Speed: {speed}\n" +
                $"Humidity: {humidity}\n";

            Console.WriteLine(forecast);
        }
    }
}