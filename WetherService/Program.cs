using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using TotalContract;

namespace WetherService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Сервис погоды";
            Console.ForegroundColor = ConsoleColor.Green;

            Uri address = new Uri("net.tcp://127.0.0.1:6001/Icontract");        // ADDRESS;
            NetTcpBinding binding = new NetTcpBinding();                        // BINDING;
            Type construct = typeof(IWeatherServiceContract);                        // CONTRACT; 
            ServiceHost host = new ServiceHost(typeof(CitiesWeather));
            host.AddServiceEndpoint(construct, binding, address);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://127.0.0.1:6001/Icontract/mex");

            try
            {
                host.Open();
                Console.WriteLine("Сервис с адресом {0} запущен!", address);
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
