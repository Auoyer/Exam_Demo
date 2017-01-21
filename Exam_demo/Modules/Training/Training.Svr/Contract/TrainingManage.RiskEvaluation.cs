using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private RiskIndexDAL dal = new RiskIndexDAL();
        /// <summary>
        /// 获取财务分析---根据建议号
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public RiskIndex GetRiskEvaluationInfo(int ProposalId)
        {
            RiskIndex result = null;
            try
            {
                result = dal.GetRiskEvaluationInfo(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetRiskEvaluationInfo方法出错", ex);
            }
            return result;

        }

        public bool UpdateRiskIndexInfo(RiskIndex Model)
        {
            bool Result = false;
            try
            {
                Result = dal.Update(Model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateRiskIndexInfo方法出错", ex);
            }
            return Result;
        }

        public int CreateRiskIndexInfo(RiskIndex Model)
        {
            int Result = 0;
            try
            { 
                 Result= dal.Add(Model); 
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CreateRiskIndexInfo方法出错", ex);
            }
            return Result;
        }
    }
}
