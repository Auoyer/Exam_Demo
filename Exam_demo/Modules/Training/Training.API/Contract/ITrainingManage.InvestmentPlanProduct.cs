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
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
      int AddInvestmentPlanProduct(InvestmentPlanProduct model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateInvestmentPlanProduct(InvestmentPlanProduct model);

        /// <summary>
        /// 获取---根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        InvestmentPlanProduct GetInvestmentPlanProductById(int id);

        /// <summary>
        /// 获取--根据建议书
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        [OperationContract]
        List<InvestmentPlanProduct> GetInvestmentPlanProductByProposalId(int ProposalId);

        /// <summary>
        /// 获取保单号集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<InvestmentPlanProduct> GetInvestmentPlanProductList(CustomFilter filter);

    }
}
