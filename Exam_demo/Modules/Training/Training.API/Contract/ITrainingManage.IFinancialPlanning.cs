using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    public partial interface ITrainingManage
    {

        /// <summary>
        /// 分页获取理财规划列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<FinancialPlanning> GetFinancialPlanningList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);
        /// <summary>
        /// 分页获取自主实训列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<FinancialPlanning> GetFinancialPlanningPageSelfTrain(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

    }
}
