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

        private InvestmentPlanDAL InvestmentPlanDAL = new InvestmentPlanDAL();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddInvestmentPlan(InvestmentPlan model)
        {
            int result = 0;
            try
            {
                result = InvestmentPlanDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddInvestmentPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateInvestmentPlan(InvestmentPlan model)
        {
            bool result = false;
            try
            {
                result = InvestmentPlanDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateInvestmentPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取---根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InvestmentPlan GetInvestmentPlanById(int id)
        {
            InvestmentPlan entity = null;
            try
            { 
                entity = InvestmentPlanDAL.GetModel(id); 
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetInvestmentPlanById方法出错", ex);
            }
            return entity;
        }
        /// <summary>
        /// 获取--根据建议书
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public InvestmentPlan GetInvestmentPlanByProposalId(int ProposalId)
        {
            InvestmentPlan entity = null;
            try
            { 
                entity = InvestmentPlanDAL.GetModelByProposalId(ProposalId); 
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetInvestmentPlanByProposalId方法出错", ex);
            }
            return entity;
        }

        /// <summary>
        /// 获取保单号集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        public List<InvestmentPlan> GetInvestmentPlanList(CustomFilter filter)
        {
            List<InvestmentPlan> entity = null;
            try
            { 
                entity = InvestmentPlanDAL.GetList(filter); 
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetInvestmentPlanList方法出错", ex);
            }
            return entity;

        }

    }
}
