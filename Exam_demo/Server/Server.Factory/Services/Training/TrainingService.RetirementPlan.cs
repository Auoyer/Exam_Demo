using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Training.API;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 获取退休计划--根据建议id
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        public RetirementPlanVM GetRetirementPlanByProposalId(int proposalId)
        {
            var model = MyService.GetRetirementPlanByProposalId(proposalId);
            return model.Map<RetirementPlanVM, RetirementPlan>();

        }

        /// <summary>
        /// 获取退休计划--根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RetirementPlanVM GetRetirementPlanById(int id)
        {
            var model = MyService.GetRetirementPlanById(id);
            return model.Map<RetirementPlanVM, RetirementPlan>();
        }

        /// <summary>
        /// 添加退休规划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddRetirementPlan(RetirementPlanVM model)
        {
            RetirementPlan entity = model.Map<RetirementPlan, RetirementPlanVM>();
            int index = MyService.AddRetirementPlan(entity);
            return index;
        }

        /// <summary>
        /// 修改退休计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRetirementPlan(RetirementPlanVM model)
        {
            RetirementPlan entity = model.Map<RetirementPlan, RetirementPlanVM>();
            bool result = MyService.UpdateRetirementPlan(entity);
            return result;
        }

    }
}
