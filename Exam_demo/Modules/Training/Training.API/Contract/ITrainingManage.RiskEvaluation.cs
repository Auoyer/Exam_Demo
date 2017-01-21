using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 根据建议号获取风险评测信息
        /// </summary>
        /// <param name="proposalId">建议号</param>
        /// <returns></returns>
        [OperationContract]
        RiskIndex GetRiskEvaluationInfo(int proposalId);
        /// <summary>
        /// 更新风险评测
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRiskIndexInfo(RiskIndex Model);
        /// <summary>
        /// 创建风险评测
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [OperationContract]
        int CreateRiskIndexInfo(RiskIndex Model);
    }
}
