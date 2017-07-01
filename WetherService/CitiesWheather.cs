using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralContract;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.ServiceModel;

namespace WetherService
{
    public class CitiesWheather : IWtServiceContract
    {
        /// <summary>
        /// Method for test this app
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public string Get(string city)
        {
            //string message = $"В {city} солнечно +27С";
            string message = String.Format("В {0} солнечно +27С", city);
            return message;
        }

        /// <summary>
        /// Method for deserialize JSON from openweather api
        /// </summary>
        /// <returns></returns>
        public string GetWeatherFromOpenWeatherApi()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Simferopol&units=metric&APPID=919b481735b6123752027a1cb1c00fdc";
            string result;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                try
                {
                    response = streamReader.ReadToEnd();
                }
                catch(Exception err)
                {
                    throw new FaultException(err.Message);
                }
                
            }

            try
            {
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                result = $"Temperature in {weatherResponse.Name}: {weatherResponse.Main.Temp} °C\n Humidity: {weatherResponse.Main.Humidity} %" + 
                                            $"\n Pressure: {weatherResponse.Main.Pressure} Mm Hg";
                //result = String.Format("Temperature in {0}: {1} °C", weatherResponse.Name, weatherResponse.Main.Temp);
            }
            catch(Exception err)
            {
                throw new FaultException(err.Message);
            }
            return result;
        }
    }
}
