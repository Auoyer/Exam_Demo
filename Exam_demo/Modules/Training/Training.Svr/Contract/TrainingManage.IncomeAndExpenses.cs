using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Training.API;

namespace Training.Svr
{
    public partial class TrainingManage:ITrainingManage
    {

        private IncomeAndExpensesDAL incomeAndExpensesDAL = new IncomeAndExpensesDAL();

        /// <summary>
        /// 新增财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>Id</returns>
        public int AddTrainIncomeAndExpenses(IncomeAndExpenses model)
        {
            int result = 0;
            try
            {
                result = incomeAndExpensesDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainIncomeAndExpenses方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateTrainIncomeAndExpenses(IncomeAndExpenses model)
        {

            bool result = false;
            try
            {
                result = incomeAndExpensesDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTrainIncomeAndExpenses方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取财务分析全部数据
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>财务分析实体列表</returns>
        public List<IncomeAndExpenses> GetAllTrainIncomeAndExpenses(CustomFilter filter)
        {

            List<IncomeAndExpenses> result = new List<IncomeAndExpenses>();
            try
            {
                result = incomeAndExpensesDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAllTrainIncomeAndExpenses方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取财务分析
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>Liability实体</returns>
        public IncomeAndExpenses GetIncomeAndExpenses(int Id)
        {
            IncomeAndExpenses result = null;
            try
            {
                result = incomeAndExpensesDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetIncomeAndExpenses方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取财务分析---根据建议书ID
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public IncomeAndExpenses GetIncomeAndExpensesByProposalId(int ProposalId)
        {
            IncomeAndExpenses result = null;
            try
            {
                result = incomeAndExpensesDAL.GetModelByProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetIncomeAndExpensesByProposalId方法出错", ex);
            }
            return result;
        }

    }
}
