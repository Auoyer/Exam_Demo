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
       /// 获取退休规划--根据建议书ID
       /// </summary>
       /// <param name="ProposalId"></param>
       /// <returns></returns>
       [OperationContract]
       RetirementPlan GetRetirementPlanByProposalId(int proposalId);
       /// <summary>
       /// 获取退休规划--根据ID
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       [OperationContract]
       RetirementPlan GetRetirementPlanById(int id);

       /// <summary>
       /// 获取退休规划列表
       /// </summary>
       /// <param name="filter"></param>
       /// <returns></returns>
       [OperationContract]
       List<RetirementPlan> GetRetirementPlanList(CustomFilter filter);

       /// <summary>
       /// 添加退休规划
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       [OperationContract]
       int AddRetirementPlan(RetirementPlan model);

       /// <summary>
       /// 修改退休规划
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       [OperationContract]
       bool UpdateRetirementPlan(RetirementPlan model);
    }
}
