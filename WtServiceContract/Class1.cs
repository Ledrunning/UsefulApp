using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WtServiceContract
{
    [ServiceContract]
    public interface WtServiceContract
    {
        [OperationContract]
        int Add(int a, int b);
    }
}
