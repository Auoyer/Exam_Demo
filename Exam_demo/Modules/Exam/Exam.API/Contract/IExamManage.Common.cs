using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Exam.API
{
    /// <summary>
    /// 考试服务相关接口
    /// </summary>
    [ServiceContract]
    public partial interface IExamManage
    {
        [OperationContract]
        void Test();
    }
}
