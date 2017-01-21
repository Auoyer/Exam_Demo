using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>Id</returns>
        public int AddInvestmentPlan(InvestmentPlanVM model)
        {
            InvestmentPlan entity = model.Map<InvestmentPlan, InvestmentPlanVM>();
            int Id = MyService.AddInvestmentPlan(entity);
            return Id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateInvestmentPlan(InvestmentPlanVM model)
        {
            InvestmentPlan entity = model.Map<InvestmentPlan, InvestmentPlanVM>();
            bool result = MyService.UpdateInvestmentPlan(entity);
            return result;

        }

        /// <summary>
        /// 获取---根据建议书ID
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public InvestmentPlanVM GetInvestmentPlanByProposalId(int ProposalId)
        {
            InvestmentPlan result = MyService.GetInvestmentPlanByProposalId(ProposalId);
            return result.Map<InvestmentPlanVM, InvestmentPlan>();

        }
    }
}
