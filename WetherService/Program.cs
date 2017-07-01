using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using GeneralContract;
using System.ServiceModel.Description;

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
            Type construct = typeof(IWtServiceContract);                        // CONTRACT; 
            ServiceHost host = new ServiceHost(typeof(CitiesWheather));
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
