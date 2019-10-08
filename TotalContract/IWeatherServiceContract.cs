using System.ServiceModel;

namespace TotalContract
{
    [ServiceContract]
    public interface IWeatherServiceContract
    {
        [OperationContract]
        string Get(string City);

        [OperationContract]
        string GetWeatherFromOpenWeatherApi();
    }
}