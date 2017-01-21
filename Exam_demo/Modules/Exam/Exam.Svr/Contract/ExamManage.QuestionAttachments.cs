using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Utils;
using System.Linq;
using Exam.Svr;
using System;



namespace Exam.API
{
    public partial class ExamManage
    {

        private QuestionAttachmentsDAL Attachment = new QuestionAttachmentsDAL();

        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddQuestionAttachments(QuestionAttachments model)
        {
            int rtnValue = 0;
            try
            {
                rtnValue = Attachment.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddQuestionAttachments", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 获取题目附件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<QuestionAttachments> GetQuestionAttachmentsList(CustomFilter filter)
        {
            List<QuestionAttachments> result = new List<QuestionAttachments>();
            try
            {

                result = Attachment.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetQuestionAttachmentsList方法出错", ex);
            }
            return result;
        }
    }
}
