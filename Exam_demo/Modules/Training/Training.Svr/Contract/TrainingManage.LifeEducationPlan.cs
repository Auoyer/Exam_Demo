using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    /// <summary>
    /// 柴志明  生涯规划-教育规划方法实现
    /// 2015-07-23
    /// </summary>
    public partial class TrainingManage : ITrainingManage
    {
        private LifeEducationPlanDAL EP = new LifeEducationPlanDAL();
        /// <summary>
        /// 新增教育规划
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddEP(LifeEducationPlan model)
        {
            int result = 0;
            try
            {
                result = EP.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddEP方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<LifeEducationPlan> AccordingIdSelectEP(CustomFilter filter)
        {
            List<LifeEducationPlan> LEP = new List<LifeEducationPlan>();
            try
            {
                LEP = EP.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectEP方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询教育规划2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public LifeEducationPlan SelectEPGetObj(int ProposalId)
        {
            LifeEducationPlan LEP = new LifeEducationPlan();
            try
            {
                LEP = EP.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectEPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询教育规划3
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public LifeEducationPlan AdoptProposalIdSelectEPGetObj(int ProposalId)
        {
            LifeEducationPlan LEP = new LifeEducationPlan();
            try
            {
                LEP = EP.GetModel2(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectEPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 修改教育规划
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateEP(LifeEducationPlan model)
        {
            bool bo = false;
            try
            {
                bo = EP.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateEP方法出错", ex);
            }
            return bo;
        }
    }
}
