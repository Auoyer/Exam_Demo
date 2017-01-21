using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        ///  新增消费规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        int AddConsumptionPlan(ConsumptionPlan filter);

        /// <summary>
        /// 查询消费规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        List<ConsumptionPlan> AccordingIdSelectConsumptionPlan(CustomFilter filter);

        /// <summary>
        /// 查询消费规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        ConsumptionPlan SelectConsumptionPlanGetObj(int Id);

        /// <summary>
        /// 查询消费规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        ConsumptionPlan AdoptProposalIdSelectCPlanGetObj(int ProposalId);


        /// <summary>
        /// 修改消费规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateConsumptionPlan(ConsumptionPlan model);
    }
}
