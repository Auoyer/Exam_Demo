using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{

    /// <summary>
    /// 柴志明 财务分析-财务比例分析方法实现
    /// 2015-07-21
    /// </summary>
    public partial class TrainingManage : ITrainingManage
    {
        private FinancialRatiosDAL financialRatios = new FinancialRatiosDAL();

        /// <summary>
        /// 新增财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddFinalcialRatios(FinancialRatios model)
        {
            int result = 0;
            try
            {
                result = financialRatios.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddFinalcialRatios方法出错", ex);
            }
            return result;

        }

        /// <summary>
        /// 修改财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFinalcialRatios(FinancialRatios model)
        {
            bool bo = false;
            try
            {
                bo = financialRatios.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateFinalcialRatios方法出错", ex);
            }
            return bo;
        }

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<FinancialRatios> AccordingIdGetList(CustomFilter filter)
        {
            List<FinancialRatios> FR = new List<FinancialRatios>();
            try
            {
                FR = financialRatios.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdGetList方法出错", ex);
            }
            return FR;
        }

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public FinancialRatios AccordingIdGetFinalcialRatios(int Id)
        {
            FinancialRatios FR = new FinancialRatios();
            try
            {
                FR = financialRatios.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdGetFinalcialRatios方法出错", ex);
            }
            return FR;
        }

        /// <summary>
        /// 查询财务比例分析
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public FinancialRatios SelectFinalcialRatiosGetObj(int ProposalId)
        {
            FinancialRatios FR = new FinancialRatios();
            try
            {
                FR = financialRatios.GetModel2(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectFinalcialRatiosGetObj方法出错", ex);
            }
            return FR;
        }
    }        
    
}
