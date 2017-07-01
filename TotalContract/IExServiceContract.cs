using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneralContract
{
    [ServiceContract]
    public interface IExServiceContract
    {
        [OperationContract]
        decimal Get(decimal money, string inValue, string outValue);
    }
}
