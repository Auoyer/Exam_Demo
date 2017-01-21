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

        private InsurancePlanDAL insurancePlanDAL = new InsurancePlanDAL();

        /// <summary>
        /// 新增财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>Id</returns>
        public int AddInsurancePlan(InsurancePlan model)
        {
            int result = 0;
            try
            {
                result = insurancePlanDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddInsurancePlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateInsurancePlan(InsurancePlan model)
        {

            bool result = false;
            try
            {
                result = insurancePlanDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateTrainLiability方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取财务分析全部数据
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>财务分析实体列表</returns>
        public List<InsurancePlan> GetInsurancePlanList(CustomFilter filter)
        {

            List<InsurancePlan> result = new List<InsurancePlan>();
            try
            {
                result = insurancePlanDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPageLiabilityList方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取财务分析
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>Liability实体</returns>
        public InsurancePlan GetInsurancePlanById(int Id)
        {
            InsurancePlan result = null;
            try
            {
                result = insurancePlanDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetLiability方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取财务分析---根据建议书ID
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public InsurancePlan GetInsurancePlanByProposalId(int ProposalId)
        {
            InsurancePlan result = null;
            try
            {
                result = insurancePlanDAL.GetModelByProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModelByProposalId方法出错", ex);
            }
            return result;
        }

    }
}
