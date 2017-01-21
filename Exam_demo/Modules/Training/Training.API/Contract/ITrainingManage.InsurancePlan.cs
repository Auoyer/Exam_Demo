using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


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
        int AddInsurancePlan(InsurancePlan model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateInsurancePlan(InsurancePlan model);

        /// <summary>
        /// 获取---根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        InsurancePlan GetInsurancePlanById(int id);

        /// <summary>
        /// 获取--根据建议书
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        [OperationContract]
        InsurancePlan GetInsurancePlanByProposalId(int ProposalId);

        /// <summary>
        /// 获取保单号集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<InsurancePlan> GetInsurancePlanList(CustomFilter filter);

    }
}
