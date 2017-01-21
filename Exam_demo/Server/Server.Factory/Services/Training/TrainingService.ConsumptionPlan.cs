using System;
using System.Collections.Generic;
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
        /// 新增教育规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddConsumptionPlan(ConsumptionPlanVM model)
        {
            ConsumptionPlan entity = model.Map<ConsumptionPlan, ConsumptionPlanVM>();
            int Id = MyService.AddConsumptionPlan(entity);
            return Id;
        }

        /// <summary>
        /// 查询教育规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public ConsumptionPlanVM AdoptProposalIdSelectCPlanGetObj(int ProposalId)
        {
            var model = MyService.AdoptProposalIdSelectCPlanGetObj(ProposalId);
            return model.Map<ConsumptionPlanVM, ConsumptionPlan>();
        }

        /// <summary>
        /// 修改教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateConsumptionPlan(ConsumptionPlanVM model)
        {

            bool bo = false;
            ConsumptionPlan entity = model.Map<ConsumptionPlan, ConsumptionPlanVM>();
            bo = MyService.UpdateConsumptionPlan(entity);
            return bo;
        }
    }
}
