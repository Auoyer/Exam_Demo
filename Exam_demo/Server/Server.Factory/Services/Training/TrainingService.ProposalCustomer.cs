using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 新增建议书客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddProposalCustomer(ProposalCustomerVM model)
        {
            return MyService.AddProposalCustomer(model.Map<ProposalCustomer, ProposalCustomerVM>());
        }

        /// <summary>
        /// 修改建议书客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProposalCustomer(ProposalCustomerVM model)
        {
            return MyService.UpdateProposalCustomer(model.Map<ProposalCustomer, ProposalCustomerVM>());
        }

        /// <summary>
        /// 获取建议书客户信息
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        public ProposalCustomerVM GetProposalCustomer(int ProposalId)
        {
            ProposalCustomer model = MyService.GetProposalCustomer(ProposalId);
            return model.Map<ProposalCustomerVM, ProposalCustomer>();
        }

    }
}
