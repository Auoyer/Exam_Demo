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
        private ProposalDAL proposalDAL = new ProposalDAL();

        /// <summary>
        /// 新增建议书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddProposal(Proposal model)
        {
            int result=0;
            try
            {
                result = proposalDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddProposal方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改建议书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProposal(Proposal model)
        {
            bool result = false;
            try
            {
                result = proposalDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateProposal方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除建议书(伪删)
        /// 会同步修改以下内容：
        /// 1.客户信息中的建议书状态与建议书数量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool DeleteProposal(int Id)
        {
            bool result = false;
            try
            {
                result = proposalDAL.DeleteProposal(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteProposal方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 删除建议书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteAllProposalIds(int ProposalId)
        {
            bool result = false;
            try
            {
                result = proposalDAL.DeleteAllProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteAllProposalId方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 修改建议书目前的状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool UpdateProposalStatus(int Id, int Status)
        {
            bool result = false;
            try
            {
                result = proposalDAL.UpdateStatus(Id, Status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateProposalStatus方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取建议书列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Proposal> proposalCustomerList(CustomFilter model)
        {
            List<Proposal> result = new List<Proposal>();
            try
            {
                result = proposalDAL.GetList(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("proposalCustomerList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public Proposal GetProposal(int Id)
        {
            Proposal result = null;
            try
            {
                result = proposalDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetProposal方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public Proposal GetProposalInfo(CustomFilter filter)
        {
            Proposal result = null;
            try
            {
                result = proposalDAL.GetModel(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetProposal方法出错", ex);
            }
            return result;
        }
        /// <summary>
        /// 更新建议书时间
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <param name="UpdateDate">更新时间</param>
        /// <returns></returns>
        public bool UpdateProposalDate(int Id, DateTime UpdateDate)
        {
            bool result = false;
            try
            {
                result = proposalDAL.UpdateProposalDate(Id, UpdateDate);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateProposalDate方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据客户Id统计有效（未提交、未审核、已审核）的建议书数量
        /// </summary>
        /// <param name="StuCustomerId">客户Id</param>
        /// <returns></returns>
        public int CountProposal(int StuCustomerId)
        {
            int result = 0;
            try
            {
                result = proposalDAL.CountProposal(StuCustomerId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CountProposal方法出错", ex);
            }
            return result;
        }
    }
}
