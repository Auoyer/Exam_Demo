using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{

    /// <summary>
    /// 柴志明 财务分析-财务比例分析接口
    /// 2015-07-21
    /// </summary>
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 新增财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddFinalcialRatios(FinancialRatios model);

        /// <summary>
        /// 修改财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateFinalcialRatios(FinancialRatios model);

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<FinancialRatios> AccordingIdGetList(CustomFilter filter);

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        FinancialRatios AccordingIdGetFinalcialRatios(int Id);

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        FinancialRatios SelectFinalcialRatiosGetObj(int ProposalId);
    }
}
