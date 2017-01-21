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

        private InvestmentPlanProductDAL InvestmentPlanProductDAL = new InvestmentPlanProductDAL();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public int AddInvestmentPlanProduct(InvestmentPlanProduct model)
        {
            int result = 0;
            try
            {
                result = InvestmentPlanProductDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddInvestmentPlanProduct方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public bool UpdateInvestmentPlanProduct(InvestmentPlanProduct model)
        {
            bool result = false;
            try
            {
                result = InvestmentPlanProductDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateInvestmentPlanProduct方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取---根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public InvestmentPlanProduct GetInvestmentPlanProductById(int id)
        {
            InvestmentPlanProduct entity = null;
            try
            { entity = InvestmentPlanProductDAL.GetModel(id); }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetInvestmentPlanProductById方法出错", ex);
            }
            return entity;
        }

        /// <summary>
        /// 获取--根据建议书
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>

        public List<InvestmentPlanProduct> GetInvestmentPlanProductByProposalId(int ProposalId)
        {
            List<InvestmentPlanProduct> entity = null;
            try
            { entity = InvestmentPlanProductDAL.GetModelByProposalId(ProposalId); }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetInvestmentPlanProductByProposalId方法出错", ex);
            }
            return entity;

        }

        /// <summary>
        /// 获取保单号集合
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<InvestmentPlanProduct> GetInvestmentPlanProductList(CustomFilter filter)
        {
           List<InvestmentPlanProduct> entity = null;
            try
            { entity = InvestmentPlanProductDAL.GetList(filter); }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetInvestmentPlanProductList方法出错", ex);
            }
            return entity;

        }



    }
}
