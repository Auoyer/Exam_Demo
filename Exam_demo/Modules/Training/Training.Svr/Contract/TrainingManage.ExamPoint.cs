using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private ExamPointDAL examPointDAL = new ExamPointDAL();

        /// <summary>
        /// 获取考核点分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ExamPoint> GetExamPointPage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<ExamPoint> result = new List<ExamPoint>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<ExamPoint>(pageIndex.Value, pageSize.Value, examPointDAL.GetExamPointPageParams(filter), out totalCount);
                }
                else
                {
                    result = examPointDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCasePage方法出错", ex);
            }
            return result;
        }



    }
}
