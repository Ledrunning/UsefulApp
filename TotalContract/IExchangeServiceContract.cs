using System.ServiceModel;

namespace TotalContract
{
    [ServiceContract]
    public interface IExchangeServiceContract
    {
        [OperationContract]
        decimal Get(decimal money, string inValue, string outValue);
    }
}