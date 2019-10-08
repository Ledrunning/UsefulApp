using System;
using System.ServiceModel;
using TotalContract;

namespace NoteService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Сервис Заметки";
            Console.ForegroundColor = ConsoleColor.Green;

            var address = new Uri("net.tcp://127.0.0.1:6002/Icontract");
            var binding = new NetTcpBinding();
            var contract = typeof(INoteServiceContract);
            var host = new ServiceHost(typeof(Notes));
            host.AddServiceEndpoint(contract, binding, address);
            //host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            //host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://127.0.0.1:6002/Icontract/mex");

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