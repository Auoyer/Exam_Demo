using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Exam.API
{
    public partial interface IExamManage
    {
        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddQuestionAttachments(QuestionAttachments model);

        /// <summary>
        /// 查询题目附件
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<QuestionAttachments> GetQuestionAttachmentsList(CustomFilter filter);
    }
}
