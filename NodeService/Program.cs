using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralContract;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace NoteService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Сервис Заметки";
            Console.ForegroundColor = ConsoleColor.Green;

            Uri address = new Uri("net.tcp://127.0.0.1:6002/Icontract");
            NetTcpBinding binding = new NetTcpBinding();
            Type contract = typeof(INoteServiceContract);
            ServiceHost host = new ServiceHost(typeof(Notes));
            host.AddServiceEndpoint(contract, binding, address);
            //host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            //host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://127.0.0.1:6002/Icontract/mex");

            try
            {
                host.Open();
                Console.WriteLine("Сервис с адресом {0} запущен!", address);
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
                host.Close();
            }

            Console.ReadKey();

        }
    }
}
