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
        private ExamModuleDAL examModuleDAL = new ExamModuleDAL();

        /// <summary>
        /// 获取考核内容分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        public List<ExamModule> GetExamModulePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<ExamModule> result = new List<ExamModule>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<ExamModule>(pageIndex.Value, pageSize.Value, examModuleDAL.GetExamModulePageParams(filter), out totalCount);
                }
                else
                {
                    result = examModuleDAL.GetList(filter);
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
