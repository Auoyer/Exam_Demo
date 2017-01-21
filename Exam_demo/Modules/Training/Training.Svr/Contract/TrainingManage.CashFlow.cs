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
    /// 柴志明  财务分析-现金流量方法实现
    /// 2015-07-21
    /// </summary>
    public partial class TrainingManage : ITrainingManage
    {

        private CashFlowDAL CashFlow = new CashFlowDAL();
        /// <summary>
        /// 新增现金流量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddCashFlow(CashFlow model)
        {
            int result = 0;
            try
            {
                result = CashFlow.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddCashFlow方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询现金流量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<CashFlow> AccordingIdSelectCashFlow(CustomFilter filter)
        {
            List<CashFlow> cashFlow = new List<CashFlow>();
            try
            {
                cashFlow = CashFlow.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectCashFlow方法出错", ex);
            }

            return cashFlow;
        }

        /// <summary>
        /// 查询现金流量2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public CashFlow SelectCashFlowGetObj(int ProposalId)
        {
            CashFlow cashFlow = new CashFlow();
            try
            {
                cashFlow = CashFlow.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectCashFlowGetObj方法出错", ex);
            }

            return cashFlow;
        }

        /// <summary>
        /// 修改现金流量
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateCashFolw(CashFlow model)
        {
            bool bo = false;
            try
            {
                bo = CashFlow.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateCashFolw方法出错", ex);
            }
            return bo;
        }
    }
}
