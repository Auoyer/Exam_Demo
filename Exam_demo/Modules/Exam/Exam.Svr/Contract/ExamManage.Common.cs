using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Exam.API
{
    /// <summary>
    /// 考试服务相关接口
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class ExamManage : IExamManage
    {
        public void Test()
        {
#if DEBUG
            //Test Action,Do nothing
            LogHelper.Log.WriteInfo("[IExamManage]Someone Testing Connection!");
#endif
        }
    }
}
