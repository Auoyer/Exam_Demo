using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Training.API
{
  
    /// <summary>
    /// 柴志明 财务分析-现金流量接口
    /// 2015-07-21
    /// </summary>
    public partial interface ITrainingManage
    {
        /// <summary>
        ///  新增现金流量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddCashFlow(CashFlow filter);

        /// <summary>
        /// 查询现金流量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<CashFlow> AccordingIdSelectCashFlow(CustomFilter filter);

        /// <summary>
        /// 查询现金流量2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        CashFlow SelectCashFlowGetObj(int Id);

        /// <summary>
        /// 修改现金流量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateCashFolw(CashFlow model);
    }
}
