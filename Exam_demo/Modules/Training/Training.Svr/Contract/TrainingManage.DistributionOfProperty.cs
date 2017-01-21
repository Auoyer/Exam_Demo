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
        private DistributionOfPropertyDAL DistributionOfPropertyDAL = new DistributionOfPropertyDAL();

        /// <summary>
        /// 新增现金规划
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>Id</returns>
        public int AddDistributionOfProperty(DistributionOfProperty model)
        {
            int result = 0;
            try
            {
                result = DistributionOfPropertyDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddDistributionOfProperty方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新现金规划
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateDistributionOfProperty(DistributionOfProperty model)
        {

            bool result = false;
            try
            {
                result = DistributionOfPropertyDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateDistributionOfProperty方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取现金规划全部数据
        /// </summary>
        /// <param name="model">现金规划实体</param>
        /// <returns>现金规划实体列表</returns>
        public List<DistributionOfProperty> GetDistributionOfPropertyList(CustomFilter filter)
        {

            List<DistributionOfProperty> result = new List<DistributionOfProperty>();
            try
            {
                result = DistributionOfPropertyDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetDistributionOfPropertyList方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取现金规划
        /// </summary>
        /// <param name="Id">现金规划Id</param>
        /// <returns>现金规划实体</returns>
        public DistributionOfProperty GetDistributionOfProperty(int Id)
        {
            DistributionOfProperty result = null;
            try
            {
                result = DistributionOfPropertyDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DistributionOfPropertyPlan方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取现金规划---根据建议书Id
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns>现金规划实体</returns>
        public DistributionOfProperty GetDistributionOfPropertyByProposalId(int ProposalId)
        {
            DistributionOfProperty result = null;
            try
            {
                result = DistributionOfPropertyDAL.GetModelByProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetDistributionOfPropertyByProposalId方法出错", ex);
            }
            return result;
        }
    }
}
