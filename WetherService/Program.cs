using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using TotalContract;

namespace WetherService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Сервис погоды";
            Console.ForegroundColor = ConsoleColor.Green;

            var address = new Uri("net.tcp://127.0.0.1:6001/Icontract"); // ADDRESS;
            var binding = new NetTcpBinding(); // BINDING;
            var construct = typeof(IWeatherServiceContract); // CONTRACT; 
            var host = new ServiceHost(typeof(CitiesWeather));
            host.AddServiceEndpoint(construct, binding, address);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(),
                "net.tcp://127.0.0.1:6001/Icontract/mex");

            try
            {
                host.Open();
                Console.WriteLine($"Сервис с адресом {address} запущен!");
            }

            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                host.Close();
            }

            Console.ReadKey();
        }
    }
}