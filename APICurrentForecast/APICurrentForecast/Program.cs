using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.IO;
using System.Net.Http.Headers;

namespace APICurrentForecast
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key).GetValue("APIKey").ToString();
            var client = new HttpClient();
            string country = "USA";
            var lat = "";
            var lon = "";

            Console.WriteLine("Please enter a city name:");
            string userCity = Console.ReadLine();

            Console.WriteLine("Please enter the two letter abbreviation for the state the city is in:");
            string userState = Console.ReadLine();

            var locatorUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={userCity},{userState},{country}&limit={1}&appid={APIKey}";
            var apiGeoResponse = client.GetStringAsync(locatorUrl).Result;

            while (apiGeoResponse == "[]")
            {
                Console.WriteLine($"\nOur records show:\nCITY: {userCity}   &   STATE: {userState}\n\n" +
                $"Does not currently exist in the US...Yikes! \nWe really want to help you get the forecast you need," +
                $" could you confirm your input/spelling and try again? \nIf that doesn't work, maybe contact your local " +
                $"cartographer? They know what's where. \n" +
                $"If that's not really your thing, you could always try turning it off and then back on again.\n");

                Console.WriteLine("Please enter the name of your city:");
                userCity = Console.ReadLine();

                Console.WriteLine("Please enter the two letter abbreviation for your state:");
                userState = Console.ReadLine();

                locatorUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={userCity},{userState},{country}&limit={1}&appid={APIKey}";
                apiGeoResponse = client.GetStringAsync(locatorUrl).Result;
                if (apiGeoResponse != "[]") break;
            }

            JArray parsedApi = JArray.Parse(apiGeoResponse);
            foreach (JObject jObject in parsedApi)
            {
                lat = $"{(string)jObject["lat"]}";
                lon = $"{(string)jObject["lon"]}";
            }

            var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIKey}&units=imperial";
            var apiWeaResponse = client.GetStringAsync(weatherUrl).Result;

            var main = JObject.Parse(apiWeaResponse).GetValue("main").ToString();
            var temp = JObject.Parse(main).GetValue("temp").ToString();
            var tempMin = JObject.Parse(main).GetValue("temp_min").ToString();
            var tempMax = JObject.Parse(main).GetValue("temp_max").ToString();
            var feelsLike = JObject.Parse(main).GetValue("feels_like").ToString();
            var humidity = JObject.Parse(main).GetValue("humidity").ToString();

            var wind = JObject.Parse(apiWeaResponse).GetValue("wind").ToString();
            var windSpeed = JObject.Parse(wind).GetValue("speed").ToString();

            var weather = JObject.Parse(apiWeaResponse).GetValue("weather").ToString();
            var weatherConditions = "";

            JArray parsedWeather = JArray.Parse(weather);
            foreach (JObject jObject in parsedWeather)
            {
                weatherConditions = $"{(string)jObject["description"]}";
            }

            var forecast = $"\nThe current weather forecast in {userCity} is:\n" +
                $"{weatherConditions}\n" +
                $"Temperature: {temp}\n" +
                $"Feels Like: {feelsLike}\n" +
                $"Today's Low: {tempMin}\n" +
                $"Today's High: {tempMax}\n" +
                $"Wind Speed: {windSpeed}\n" +
                $"Humidity: {humidity}\n";

            Console.WriteLine(forecast);
        }
    }
}