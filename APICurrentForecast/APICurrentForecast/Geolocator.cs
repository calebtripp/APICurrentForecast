//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Metrics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace APICurrentForecast
//{
//    internal class Geolocator
//    {
//        var locatorUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{state},{country}&limit={1}&appid={"269fcd68b6d3d0d501d5f58d3d61fc61"}&units=imperial";
//        // var weatherUrl = "http://api.openweathermap.org/geo/1.0/direct?q={london},{state code},{country code}&limit={1}&appid={269fcd68b6d3d0d501d5f58d3d61fc61}&units=imperial";
//        //var weatherUrl = $"https://api.openweathermap.org/geo/1.0/direct?q=London&limit=5&appid={"269fcd68b6d3d0d501d5f58d3d61fc61"}";
//        var apiResponse = client.GetStringAsync(locatorUrl).Result;
//        var lat = JArray.Parse(apiResponse).GetValue("lat").ToString();

//        // var lon = JArray.Parse(apiResponse).GetValue("lon").ToString();  
//        //
//        //  JArray FromObject(object o)
//    }
//}

//// use below as example for fore loop to get json from array to object
//foreach (JObject jObject in jArray)
//{
//    Console.WriteLine($"{(string)jObject["name"]} -> {(string)jObject["email"]}");