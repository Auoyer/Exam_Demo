using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM;
using Training.API;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {

        public RiskIndexVM GetRiskEvaluationInfo(int proposalId)
        {
            var model = MyService.GetRiskEvaluationInfo(proposalId);
            return model.Map<RiskIndexVM, RiskIndex>();
        }

        /// <summary>
        /// 更新风险评测
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool UpdateRiskIndexInfo(RiskIndexVM Model)
        {
            RiskIndex entity = Model.Map<RiskIndex, RiskIndexVM>();
            bool result = MyService.UpdateRiskIndexInfo(entity);
            return result;
        }

        /// <summary>
        /// 添加风险评测
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int CreateRiskIndexInfo(RiskIndexVM Model)
        {
            RiskIndex entity = Model.Map<RiskIndex, RiskIndexVM>();
            return MyService.CreateRiskIndexInfo(entity); 
        }
    }
}
