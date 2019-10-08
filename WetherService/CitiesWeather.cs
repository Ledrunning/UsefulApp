using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using Newtonsoft.Json;
using TotalContract;

namespace WetherService
{
    public class CitiesWeather : IWeatherServiceContract
    {
        /// <summary>
        ///     Method for test this app
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public string Get(string city)
        {
            //string message = $"В {city} солнечно +27С";
            var message = string.Format("В {0} солнечно +27С", city);
            return message;
        }

        /// <summary>
        ///     Method for deserialize JSON from openweather api
        /// </summary>
        /// <returns></returns>
        public string GetWeatherFromOpenWeatherApi()
        {
            var url =
                "http://api.openweathermap.org/data/2.5/weather?q=Simferopol&units=metric&APPID=919b481735b6123752027a1cb1c00fdc";
            string result;
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
            var httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();

            string response;

            using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                try
                {
                    response = streamReader.ReadToEnd();
                }
                catch (Exception err)
                {
                    throw new FaultException(err.Message);
                }
            }

            try
            {
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                result =
                    $"Temperature in {weatherResponse.Name}: {weatherResponse.Main.Temp} °C\n Humidity: {weatherResponse.Main.Humidity} %" +
                    $"\n Pressure: {weatherResponse.Main.Pressure} Mm Hg";
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }

            return result;
        }
    }
}