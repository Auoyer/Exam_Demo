using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    /// <summary>
    /// 大赛相关接口
    /// </summary>
    /// 
    [ServiceContract]
    public partial interface IMatchManage
    {
        [OperationContract]
        void Test();
    }
}
