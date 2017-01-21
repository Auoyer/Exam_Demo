using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Training.API;
using VM;

namespace Server.Factory
{
  public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 新增财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>财务分析Id</returns>
        public int AddTrainIncomeAndExpenses(IncomeAndExpensesVM model)
        {
            IncomeAndExpenses entity = model.Map<IncomeAndExpenses, IncomeAndExpensesVM>();
            int Id = MyService.AddTrainIncomeAndExpenses(entity);
            return Id;
        }

        /// <summary>
        /// 更新财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateTrainIncomeAndExpenses(IncomeAndExpensesVM model)
        {
            IncomeAndExpenses entity = model.Map<IncomeAndExpenses, IncomeAndExpensesVM>();
            bool result = MyService.UpdateTrainIncomeAndExpenses(entity);
            return result;
        }

        /// <summary>
        /// 获取财务分析
        /// </summary>
        /// <param name="Id">财务分析Id</param>
        /// <returns></returns>
        public IncomeAndExpensesVM GetIncomeAndExpenses(int Id)
        {
            var model = MyService.GetIncomeAndExpenses(Id);
            return model.Map<IncomeAndExpensesVM, IncomeAndExpenses>();
        }

        /// <summary>
        /// 获取财务分析
        /// </summary>
        /// <param name="Id">财务分析Id</param>
        /// <returns></returns>
        public IncomeAndExpensesVM GetIncomeAndExpensesByProposalId(int ProposalId)
        {
            var model = MyService.GetIncomeAndExpensesByProposalId(ProposalId);
            return model.Map<IncomeAndExpensesVM, IncomeAndExpenses>();
        }

    }
}
