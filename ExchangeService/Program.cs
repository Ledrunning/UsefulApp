using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using TotalContract;

namespace ExchangeService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Сервис конвертера валют";
            Console.ForegroundColor = ConsoleColor.Green;

            Uri address = new Uri("net.tcp://127.0.0.1:6000/Icontract");  // ADDRESS
            NetTcpBinding binding = new NetTcpBinding();                  // BINDING
            Type contract = typeof(IExServiceContract);                   //CONTRACT; 
            ServiceHost host = new ServiceHost(typeof(MoneyConverter));
            host.AddServiceEndpoint(contract, binding, address);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://127.0.0.1:6000/Icontract/mex");

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
