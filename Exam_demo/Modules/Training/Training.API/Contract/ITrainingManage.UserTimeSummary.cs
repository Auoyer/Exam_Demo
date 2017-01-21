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
        /// 获取对象
        /// </summary>
        /// <param name="UserId">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        UserTimeSummary GetUserTimeSummaryObj(int UserId);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="UserId">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        List<UserTimeSummary> GetUserTimeSummarylist(CustomFilter filter);

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int AddUserTimeSummary(UserTimeSummary USummary);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateUserTimeSummary(UserTimeSummary model);
    }
}
