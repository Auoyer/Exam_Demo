using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 新增现金规划
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>财务分析Id</returns>
        public int AddCashPlan(CashPlanVM model)
        {
            CashPlan entity = model.Map<CashPlan, CashPlanVM>();
            int Id = MyService.AddCashPlan(entity);
            return Id;
        }

        /// <summary>
        /// 更新现金规划
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateCashPlan(CashPlanVM model)
        {
            CashPlan entity = model.Map<CashPlan, CashPlanVM>();
            bool result = MyService.UpdateCashPlan(entity);
            return result;
        }

        /// <summary>
        /// 获取现金规划
        /// </summary>
        /// <param name="Id">现金规划Id</param>
        /// <returns></returns>
        public CashPlanVM GetCashPlan(int Id)
        {
            var model = MyService.GetCashPlan(Id);
            return model.Map<CashPlanVM, CashPlan>();
        }

        /// <summary>
        /// 获取现金规划---根据建议书Id
        /// </summary>
        /// <param name="proposalId">建议书Id</param>
        /// <returns></returns>
        public CashPlanVM GetCashPlanByProposalId(int proposalId)
        {
            var model = MyService.GetCashPlanByProposalId(proposalId);
            return model.Map<CashPlanVM, CashPlan>();
        }
    }
}
