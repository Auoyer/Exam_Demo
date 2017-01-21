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
        private HeritageDAL heritageDAL = new HeritageDAL();

        /// <summary>
        /// 获取财产传承实体
        /// </summary>
        /// <param name="ProposalId">计划书Id</param>
        /// <returns></returns>
        public Heritage GetHeritage(int ProposalId)
        {
            Heritage result = null;
            try
            {
                result = heritageDAL.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetHeritage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 新增财产传承
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int AddHeritage(Heritage model)
        {
            int result = 0;
            try
            {
                result = heritageDAL.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddHeritage方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 查询财产传承
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public List<Heritage> AccordingIdSelectHeritage(CustomFilter filter)
        {
            List<Heritage> LEP = new List<Heritage>();
            try
            {
                LEP = heritageDAL.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AccordingIdSelectHeritage方法出错", ex);
            }

            return LEP;
        }

        /// <summary>
        /// 查询财产传承2
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public Heritage SelectHeritageGetObj(int ProposalId)
        {
            Heritage LEP = new Heritage();
            try
            {
                LEP = heritageDAL.GetModel(ProposalId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SelectHeritageGetObj方法出错", ex);
            }

            return LEP;
        }


        /// <summary>
        /// 修改财产传承
        /// </summary>
        /// <param name="Id">建议书Id</param>
        /// <returns></returns>
        public bool UpdateHeritage(Heritage model)
        {
            bool bo = false;
            try
            {
                bo = heritageDAL.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateHeritage方法出错", ex);
            }
            return bo;
        }
    }
}
