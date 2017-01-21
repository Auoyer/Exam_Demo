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
        private ProposalCustomerDAL FinancialPlanningDAL = new ProposalCustomerDAL();
        /// <summary>
        /// 分页获取理财规划列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<FinancialPlanning> GetFinancialPlanningList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<FinancialPlanning> list = new List<FinancialPlanning>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<FinancialPlanning>(pageIndex.Value, pageSize.Value, FinancialPlanningDAL.GetFinancialPlanningPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetFinancialPlanningList方法出错", ex);
            }
            return list;
        }
        /// <summary>
        /// 自主实训查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<FinancialPlanning> GetFinancialPlanningPageSelfTrain(CustomFilter filter,int?pageIndex,int?pageSize,out int totalCount)
        {
            totalCount = 0;
            List<FinancialPlanning> list = new List<FinancialPlanning>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    list = DBHelper.GetPageList<FinancialPlanning>(pageIndex.Value, pageSize.Value, FinancialPlanningDAL.GetFinancialPlanningPageSelfTrain(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetFinancialPlanningPageSelfTrain", ex);
            }
            return list;
        }
    }
}
