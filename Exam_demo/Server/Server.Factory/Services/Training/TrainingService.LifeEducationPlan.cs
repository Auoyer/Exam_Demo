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
        public int AddEP(LifeEducationPlanVM model)
        {
            LifeEducationPlan entity = model.Map<LifeEducationPlan, LifeEducationPlanVM>();
            int Id = MyService.AddEP(entity);
            return Id;
        }

        /// <summary>
        /// 查询教育规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public LifeEducationPlanVM AdoptProposalIdSelectEPGetObj(int ProposalId)
        {
            var model = MyService.AdoptProposalIdSelectEPGetObj(ProposalId);
            return model.Map<LifeEducationPlanVM, LifeEducationPlan>();
        }

        /// <summary>
        /// 修改教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateEP(LifeEducationPlanVM model)
        {           

            bool bo = false;
            LifeEducationPlan entity = model.Map<LifeEducationPlan, LifeEducationPlanVM>();
            bo = MyService.UpdateEP(entity);
            return bo;
        }
    }
}
