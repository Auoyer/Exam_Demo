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
        private LiabilityDAL liabilityDAL = new LiabilityDAL();

        /// <summary>
        /// 新增财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>Id</returns>
        public int AddTrainLiability(Liability model)
        {
            int result = 0;
            try
            {
                result = liabilityDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainLiability方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新财务分析
        /// </summary>
        /// <param name="model">财务分析实体</param>
        /// <returns>是否成功</returns>
        public bool UpdateTrainLiability(Liability model)
        {

            bool result = false;
            try
            {
                result = liabilityDAL.Update(model);
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
        public List<Liability> GetLiabilityList(CustomFilter filter)
        {
      
            List<Liability> result = new List<Liability>();
            try
            {
                result = liabilityDAL.GetList(filter);
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
        public Liability GetLiability(int Id)
        {
            Liability result = null;
            try
            {
                result = liabilityDAL.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetLiability方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 获取财务分析---根据建议号
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public Liability GetLiabilityByProposalId(int ProposalId)
        {
            Liability result = null;
            try
            {
                result = liabilityDAL.GetModelByProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetModelByProposalId方法出错", ex);
            }
            return result;

        }
    }
}
