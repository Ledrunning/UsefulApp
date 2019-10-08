using System.ServiceModel;
using TotalContract;

namespace Client
{
    /// <summary>
    ///     Class for creating service channels
    /// </summary>
    public class FactoryAndChannels
    {
        private readonly ChannelFactory<IExServiceContract> firstFactory = new ChannelFactory<IExServiceContract>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://127.0.0.1:6000/Icontract"));

        private readonly ChannelFactory<IWeatherServiceContract> secondFactory = new ChannelFactory<IWeatherServiceContract>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://127.0.0.1:6001/Icontract"));

        private readonly ChannelFactory<INoteServiceContract> thirdFactory = new ChannelFactory<INoteServiceContract>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://127.0.0.1:6002/Icontract"));

        /// <summary>
        ///     Weather service method
        /// </summary>
        /// <returns></returns>
        public IWeatherServiceContract CreateWeatherFactory()
        {
            var weatherService = secondFactory.CreateChannel();
            return weatherService;
        }

        /// <summary>
        ///     Exchange service method
        /// </summary>
        /// <returns></returns>
        public IExServiceContract CreateExchangeFactory()
        {
            var exService = firstFactory.CreateChannel();
            return exService;
        }

        /// <summary>
        ///     Method for creating note factory
        /// </summary>
        /// <returns></returns>
        public INoteServiceContract CreateNotesFactory()
        {
            var noteService = thirdFactory.CreateChannel();
            return noteService;
        }
    }
}