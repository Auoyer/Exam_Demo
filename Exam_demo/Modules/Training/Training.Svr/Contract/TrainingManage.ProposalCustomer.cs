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
        private ProposalCustomerDAL proposalCustomerDAL = new ProposalCustomerDAL();

        /// <summary>
        /// 新增建议书客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddProposalCustomer(ProposalCustomer model)
        {
            int  result = 0;
            try
            {
                result = proposalCustomerDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddProposalCustomer方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改建议书客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProposalCustomer(ProposalCustomer model)
        {
            bool result = false;
            try
            {
                result = proposalCustomerDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateProposalCustomer方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取建议书客户信息
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        public ProposalCustomer GetProposalCustomer(int ProposalId)
        {
            ProposalCustomer model = null;
            try
            {
                model = proposalCustomerDAL.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetProposalCustomer方法出错", ex);
            }
            return model;
        }



    }
}
