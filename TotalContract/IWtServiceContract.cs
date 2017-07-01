using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneralContract
{
    [ServiceContract]
    public interface IWtServiceContract
    {
        [OperationContract]
        string Get(string City);
        [OperationContract]
        string GetWeatherFromOpenWeatherApi();
    }
}
