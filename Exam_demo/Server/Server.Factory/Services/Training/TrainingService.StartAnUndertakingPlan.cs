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
        /// 新增创业规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddSUP(StartAnUndertakingPlanVM model)
        {
            StartAnUndertakingPlan entity = model.Map<StartAnUndertakingPlan, StartAnUndertakingPlanVM>();
            int Id = MyService.AddSUP(entity);
            return Id;
        }

        /// <summary>
        /// 查询创业规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public StartAnUndertakingPlanVM GetModelProposalId(int ProposalId)
        {
            var model = MyService.SelectSUPProposalId(ProposalId);
            return model.Map<StartAnUndertakingPlanVM, StartAnUndertakingPlan>();
        }

        /// <summary>
        /// 修改创业规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateSUP(StartAnUndertakingPlanVM model)
        {
            bool bo = false;
            StartAnUndertakingPlan entity = model.Map<StartAnUndertakingPlan, StartAnUndertakingPlanVM>();
            bo = MyService.UpdateSUP(entity);
            return bo;
        }
    }
}
