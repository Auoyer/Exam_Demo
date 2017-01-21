using Match.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Match.Svr
{
    public partial class MatchManage
    {
        #region 超管端-获取历史大赛数量 int GetCompetitionNum()
        /// <summary>
        /// 超管端-获取历史大赛数量
        /// </summary>
        /// <returns></returns>
        public int GetCompetitionNum()
        {
            int result = 0;
            try
            {
                result = matchDAL.GetCompetitionNum();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCompetitionNum方法出错", ex);

            }
            return result;
        }
        #endregion

        #region 获取最近大赛列表 List<Competition> GetLatestCompetitionList(int num)
        /// <summary>
        /// 获取最近大赛列表
        /// </summary>
        /// <param name="num">大赛数目</param>
        /// <returns></returns>
        public List<Competition> GetLatestCompetitionList(int num, int collegeId)
        {
            List<Competition> result = new List<Competition>();
            try
            {
                result = matchDAL.GetLatestCompetitionList(num, collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetLatestCompetitionList方法出错", ex);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 判断用户是否有正在进行的比赛，true=没有正在进行的比赛
        /// </summary>
        /// <param name="listJudgeId">List<评委ID></param>
        /// <param name="userType">用户类型，1=评委，2=参赛用户</param>
        /// <returns></returns>
        public bool IsComAdminConductMatch(List<int> comAdminId)
        {
            return matchDAL.IsComAdminConductMatch(comAdminId);
        }

        /// <summary>
        /// 获取大赛状态为已结束的比赛列表
        /// </summary>
        /// <author>zuheng</author>
        public List<Competition> GetHasEndCompetionList(int collegeId)
        {
            List<Competition> result = new List<Competition>();
            try
            {
                result = matchDAL.GetHasEndCompetionList(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetLatestCompetitionList方法出错", ex);
            }
            return result;
        }

        #region 非超管端-获取历史大赛数量2 int GetCompetitionNum2()
        /// <summary>
        /// 非超管端-获取历史大赛数量2(非超管)
        /// </summary>
        /// <returns></returns>
        public int GetCompetitionNum2(int collegeId)
        {
            int result = 0;
            try
            {
                result = matchDAL.GetCompetitionNum2(collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetCompetitionNum2方法出错", ex);

            }
            return result;
        }
        #endregion

        #region 获取最近大赛列表2 List<Competition> GetLatestCompetitionList2(int num)
        /// <summary>
        /// 获取最近大赛列表2(非超管)
        /// </summary>
        /// <param name="num">大赛数目</param>
        /// <returns></returns>
        public List<Competition> GetLatestCompetitionList2(int num, int collegeId)
        {
            List<Competition> result = new List<Competition>();
            try
            {
                result = matchDAL.GetLatestCompetitionList2(num, collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetLatestCompetitionList2方法出错", ex);
            }
            return result;
        }
        #endregion

          /// <summary>
        /// 根据大赛的ID获取各分数段的人数
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        public List<PersonScore> GetPersonScores(int competitionId)
        {
            List<PersonScore> result = new List<PersonScore>();
            try
            {
                result = matchDAL.GetPersonScores(competitionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPersonScores方法出错", ex);
            }
            return result;
        }
    }
}
