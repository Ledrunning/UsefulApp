using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralContract;
using System.ServiceModel;

namespace Client
{
    /// <summary>
    /// Class for creating service channels 
    /// </summary>
    public class FactoryAndChannels
    {
        private ChannelFactory<IExServiceContract> firstFactory = new ChannelFactory<IExServiceContract>(new NetTcpBinding(),
                                                new EndpointAddress("net.tcp://127.0.0.1:6000/Icontract"));
        private ChannelFactory<IWtServiceContract> secondFactory = new ChannelFactory<IWtServiceContract>(new NetTcpBinding(),
                                                new EndpointAddress("net.tcp://127.0.0.1:6001/Icontract"));

        private ChannelFactory<INoteServiceContract> thirdFactory = new ChannelFactory<INoteServiceContract>(new NetTcpBinding(),
                                               new EndpointAddress("net.tcp://127.0.0.1:6002/Icontract"));

        /// <summary>
        /// Weather service method
        /// </summary>
        /// <returns></returns>
        public IWtServiceContract CreateWeatherFactory()
        {
            IWtServiceContract weatherService = secondFactory.CreateChannel();
            return weatherService;
        }

        /// <summary>
        /// Exchange service method
        /// </summary>
        /// <returns></returns>
        public IExServiceContract CreateExchangeFactory()
        {
            IExServiceContract exService = firstFactory.CreateChannel();
            return exService;
        }

        /// <summary>
        /// Method for creating note factory
        /// </summary>
        /// <returns></returns>
        public INoteServiceContract CreateNotesFactory()
        {
            INoteServiceContract noteService = thirdFactory.CreateChannel();
            return noteService;
        }
    }
}
