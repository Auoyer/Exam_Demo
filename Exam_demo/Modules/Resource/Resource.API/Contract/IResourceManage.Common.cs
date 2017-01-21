using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Resource.API
{
    [ServiceContract]
    public partial interface IResourceManage
    {
        [OperationContract]
        void Test();
    }
}
