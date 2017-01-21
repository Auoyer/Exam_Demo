using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Exam.API
{
    public partial interface IExamManage
    { 

        #region 查

        /// <summary>
        /// 获取单个试卷的信息
        /// </summary>
        /// <param name="paperID"></param> 
        /// <returns></returns>
        [OperationContract]
        Paper GetPaperChapter(int paperID); 
        #endregion
    }
}
