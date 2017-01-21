using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Resource.API;

namespace Resource.Svr
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class ResourceManage : IResourceManage
    {
        /// <summary>
        /// 测试连接
        /// </summary>
        public void Test()
        {
            //
        }
    }
}
