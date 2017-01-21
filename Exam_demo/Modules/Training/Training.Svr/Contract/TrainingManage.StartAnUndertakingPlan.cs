using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage
    {
        private StartAnUndertakingPlanDAL LEPD = new StartAnUndertakingPlanDAL();
        /// <summary>
        /// 新增创业规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddSUP(StartAnUndertakingPlan model)
        {
            int result = 0;
            try
            {
                result = LEPD.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddSUP方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新删除创业规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteSUP(int Id)
        {
            bool result = false;
            try
            {
                result = LEPD.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteSUP方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询创业规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<StartAnUndertakingPlan> AccordingIdSelectSUP(CustomFilter filter)
        {
            List<StartAnUndertakingPlan> LEP = new List<StartAnUndertakingPlan>();
            try
            {
                LEP = LEPD.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectSUP方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询创业规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public StartAnUndertakingPlan SelectSUPGetObj(int Id)
        {
            StartAnUndertakingPlan LEP = new StartAnUndertakingPlan();
            try
            {
                LEP = LEPD.GetModel(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectSUPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询创业规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public StartAnUndertakingPlan SelectSUPProposalId(int ProposalId)
        {
            StartAnUndertakingPlan LEP = new StartAnUndertakingPlan();
            try
            {
                LEP = LEPD.GetModelProposalId(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectSUPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 修改创业规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateSUP(StartAnUndertakingPlan model)
        {
            bool bo = false;
            try
            {
                bo = LEPD.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateSUP方法出错", ex);
            }
            return bo;
        }
    }
}
