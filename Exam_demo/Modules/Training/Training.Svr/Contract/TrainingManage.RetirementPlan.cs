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

        private RetirementPlanDAL retirementPlanDAL = new RetirementPlanDAL();
        /// <summary>
        /// 获取退休规划--根据建议书ID
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public RetirementPlan GetRetirementPlanByProposalId(int proposalId)
        {
            RetirementPlan result = null;
            try
            {
                  result =  retirementPlanDAL.GetModelByProposalId(proposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModelByProposalId方法出错", ex);
            }
            return result;

        }
        /// <summary>
        /// 获取退休规划--根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      public RetirementPlan GetRetirementPlanById(int id)
        {
            RetirementPlan result = null;
            try
            {
                result = retirementPlanDAL.GetModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModel方法错误", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取退休规划列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
       public List<RetirementPlan> GetRetirementPlanList(CustomFilter filter)
        {
            List<RetirementPlan> list = new List<RetirementPlan>();
            try
            {
                list = retirementPlanDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetList方法错误", ex);

            }
            return list;
        }

        /// <summary>
        /// 添加退休规划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public int AddRetirementPlan(RetirementPlan model)
       {
           int index = 0;
           try
           {
              index=  retirementPlanDAL.Add(model);
           }
           catch(Exception ex)
           {
               LogHelper.Log.WriteError("Add方法出错", ex);
           }
           return index;
       }

        /// <summary>
        /// 修改退休规划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRetirementPlan(RetirementPlan model)
        {
            bool result = false;
            try
            {
                result = retirementPlanDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("Update方法错误", ex);
            }
            return result;
        }

    }
}
