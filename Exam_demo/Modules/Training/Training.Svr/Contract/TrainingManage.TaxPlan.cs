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
        private TaxPlanDAL TP = new TaxPlanDAL();
        /// <summary>
        /// 新增教育规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddTaxPlan(TaxPlan model)
        {
            int result = 0;
            try
            {
                result = TP.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTaxPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        ///  删除税收筹划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteTaxPlan(int Id)
        {
            bool result = false;
            try
            {
                result = TP.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTaxPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<TaxPlan> AccordingIdSelectTaxPlan(CustomFilter filter)
        {
            List<TaxPlan> LEP = new List<TaxPlan>();
            try
            {
                LEP = TP.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectTaxPlan方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询教育规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public TaxPlan SelectTaxPlanGetObj(int ProposalId)
        {
            TaxPlan LEP = new TaxPlan();
            try
            {
                LEP = TP.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectTaxPlanGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询教育规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public TaxPlan AdoptProposalIdSelectTaxPlanGetObj(int ProposalId)
        {
            TaxPlan LEP = new TaxPlan();
            try
            {
                LEP = TP.GetModel2(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AdoptProposalIdSelectTaxPlanGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 修改教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateTaxPlan(TaxPlan model)
        {
            bool bo = false;
            try
            {
                bo = TP.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTaxPlan方法出错", ex);
            }
            return bo;
        }
    }
}
