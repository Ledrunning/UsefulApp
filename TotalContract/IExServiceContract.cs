using System.ServiceModel;

namespace TotalContract
{
    [ServiceContract]
    public interface IExServiceContract
    {
        [OperationContract]
        decimal Get(decimal money, string inValue, string outValue);
    }
}