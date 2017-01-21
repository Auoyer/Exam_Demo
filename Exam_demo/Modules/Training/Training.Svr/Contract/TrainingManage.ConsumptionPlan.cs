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
        private ConsumptionPlanDAL CP = new ConsumptionPlanDAL();
        /// <summary>
        /// 新增消费规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddConsumptionPlan(ConsumptionPlan model)
        {
            int result = 0;
            try
            {
                result = CP.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddEP方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询消费规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<ConsumptionPlan> AccordingIdSelectConsumptionPlan(CustomFilter filter)
        {
            List<ConsumptionPlan> LEP = new List<ConsumptionPlan>();
            try
            {
                LEP = CP.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectEP方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询消费规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public ConsumptionPlan SelectConsumptionPlanGetObj(int ProposalId)
        {
            ConsumptionPlan LEP = new ConsumptionPlan();
            try
            {
                LEP = CP.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectEPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询消费规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public ConsumptionPlan AdoptProposalIdSelectCPlanGetObj(int ProposalId)
        {
            ConsumptionPlan LEP = new ConsumptionPlan();
            try
            {
                LEP = CP.GetModel2(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectEPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 修改消费规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateConsumptionPlan(ConsumptionPlan model)
        {
            bool bo = false;
            try
            {
                bo = CP.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateConsumptionPlan方法出错", ex);
            }
            return bo;
        }
    }
}
