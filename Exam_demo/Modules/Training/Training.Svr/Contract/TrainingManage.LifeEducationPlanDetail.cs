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
    /// 柴志明  生涯规划-教育规划详细信息方法实现
    /// 2015-07-24
    /// </summary>
    public partial class TrainingManage : ITrainingManage
    {
        private LifeEducationPlanDetailDAL EPD = new LifeEducationPlanDetailDAL();
        /// <summary>
        /// 新增教育规划详细信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddEPD(LifeEducationPlanDetail model)
        {
            int result = 0;
            try
            {
                result = EPD.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddEP方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新删除教育规划详细信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteEPD(int Id)
        {
            bool result = false;
            try
            {
                result = EPD.Delete(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddEP方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询教育规划详细信息
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<LifeEducationPlanDetail> AccordingIdSelectEPD(CustomFilter filter)
        {
            List<LifeEducationPlanDetail> LEP = new List<LifeEducationPlanDetail>();
            try
            {
                LEP = EPD.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectEP方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询教育规划2详细信息
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public LifeEducationPlanDetail SelectEPDGetObj(int ProposalId)
        {
            LifeEducationPlanDetail LEP = new LifeEducationPlanDetail();
            try
            {
                LEP = EPD.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectEPGetObj方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 修改教育规划详细信息
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateEPD(LifeEducationPlanDetail model)
        {
            bool bo = false;
            try
            {
                bo = EPD.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateEP方法出错", ex);
            }
            return bo;
        }
    }
}
