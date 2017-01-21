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
        private CashPlanDAL cashPlanDAL = new CashPlanDAL();

        /// <summary>
        /// 新增现金规划
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>Id</returns>
        public int AddCashPlan(CashPlan model)
        {
            int result = 0;
            try
            {
                result = cashPlanDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainCashPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新现金规划
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateCashPlan(CashPlan model)
        {

            bool result = false;
            try
            {
                result = cashPlanDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTrainCashPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取现金规划全部数据
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>现金规划实体列表</returns>
        public List<CashPlan> GetCashPlanList(CustomFilter filter)
        {

            List<CashPlan> result = new List<CashPlan>();
            try
            {
                result = cashPlanDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetLiabilityList方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取现金规划
        /// </summary>
        /// <param name="Id">现金规划Id</param>
        /// <returns>现金规划实体</returns>
        public CashPlan GetCashPlan(int Id)
        {
            CashPlan result = null;
            try
            {
                result = cashPlanDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCashPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取现金规划---根据建议书Id
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns>现金规划实体</returns>
        public CashPlan GetCashPlanByProposalId(int ProposalId)
        {
            CashPlan result = null;
            try
            {
                result = cashPlanDAL.GetModelByProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCashPlanByProposalId方法出错", ex);
            }
            return result;
        }
    }
}
