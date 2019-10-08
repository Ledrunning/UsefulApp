using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using TotalContract;

namespace ExchangeService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Сервис конвертера валют";
            Console.ForegroundColor = ConsoleColor.Green;

            var address = new Uri("net.tcp://127.0.0.1:6000/Icontract"); // ADDRESS
            var binding = new NetTcpBinding(); // BINDING
            var contract = typeof(IExServiceContract); //CONTRACT; 
            var host = new ServiceHost(typeof(MoneyConverter));
            host.AddServiceEndpoint(contract, binding, address);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(),
                "net.tcp://127.0.0.1:6000/Icontract/mex");

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