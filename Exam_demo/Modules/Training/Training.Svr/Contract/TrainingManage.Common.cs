using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Training.API;

namespace Training.Svr
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class TrainingManage : ITrainingManage
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
