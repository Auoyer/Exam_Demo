using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Structure.API
{
    [ServiceContract]
    public partial interface IStructureManage
    {
        /// <summary>
        /// (勿删)通道测试专用方法
        /// </summary>
        [OperationContract]
        void Test();
    }
}
