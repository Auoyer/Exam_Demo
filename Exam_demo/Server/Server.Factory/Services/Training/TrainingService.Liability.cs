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
        /// 新增财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>财务分析Id</returns>
        public int AddTrainLiability(LiabilityVM model)
        {
            Liability entity = model.Map<Liability, LiabilityVM>();
            int Id = MyService.AddTrainLiability(entity);
            return Id;
        }

        /// <summary>
        /// 更新财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateTrainLiability(LiabilityVM model)
        {
            Liability entity = model.Map<Liability, LiabilityVM>();
            bool result = MyService.UpdateTrainLiability(entity);
            return result;
        }

        /// <summary>
        /// 获取财务分析
        /// </summary>
        /// <param name="Id">财务分析Id</param>
        /// <returns></returns>
        public LiabilityVM GetLiability(int Id)
        {
            var model = MyService.GetLiability(Id);
            return model.Map<LiabilityVM, Liability>();
        }

        public LiabilityVM GetLiabilityByProposalId(int proposalId)
        {
            var model = MyService.GetLiabilityByProposalId(proposalId);
            return model.Map<LiabilityVM, Liability>();
        }

    }
}
