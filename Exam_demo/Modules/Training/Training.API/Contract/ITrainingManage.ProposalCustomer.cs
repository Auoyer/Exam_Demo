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
        /// 新增建议书客户信息
        /// </summary>
        /// <param name="model">建议书客户信息实体</param>
        /// <returns></returns>
        [OperationContract]
        int AddProposalCustomer(ProposalCustomer proposalCustomer);

        /// <summary>
        /// 更新建议书客户信息
        /// </summary>
        /// <param name="proposalCustomer">建议书客户信息实体</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateProposalCustomer(ProposalCustomer proposalCustomer);

        /// <summary>
        /// 获取建议书客户信息
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        ProposalCustomer GetProposalCustomer(int ProposalId);

    }
}
