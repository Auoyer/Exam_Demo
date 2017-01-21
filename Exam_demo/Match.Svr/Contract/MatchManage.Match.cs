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
        CompetitionDAL matchDAL = new CompetitionDAL();

        /// <summary>
        /// 验证竞赛名称是否重复
        /// </summary>
        /// <param name="matchName">竞赛名称</param>
        /// <returns>不重复返回true</returns>
        public bool IsMatchNameRepeat(string matchName, int collegeId)
        {
            bool flag = matchDAL.IsMatchNameRepeat(matchName, collegeId);

            return flag;
        }


        /// <summary>
        /// 创建大赛，关联评委
        /// </summary>
        public bool CreateMatch(Competition model, List<int> listJudgeId)
        {
            return matchDAL.CreateMatch(model, listJudgeId);
        }

        /// <summary>
        /// 判断用户是否有正在进行的比赛，true=没有正在进行的比赛
        /// </summary>
        /// <param name="listJudgeId">List<评委ID></param>
        /// <param name="userType">用户类型，1=评委，2=参赛用户</param>
        /// <returns></returns>
        public bool IsJudgeConductMatch(List<int> listJudgeId, int userType, int collegeId)
        {
            return matchDAL.IsJudgeConductMatch(listJudgeId, userType, collegeId);
        }

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Competition> GetMatchLsit(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Competition> result = new List<Competition>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<Competition>(pageIndex.Value, pageSize.Value, matchDAL.GetMatchLsit(filter), out totalCount);
                }
                else
                {
                    result = matchDAL.GetList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchLsit方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Competition GetModel(int Id)
        {
            return matchDAL.GetModel(Id);
        }

        /// <summary>
        /// 编辑竞赛信息，及关联评委信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool UpdateMatch(Competition vm)
        {
            return matchDAL.UpdateMatch(vm);
        }

        /// <summary>
        /// 查询竞赛评审Ids
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public List<int> GetMatchJudgeListId(int matchId)
        {
            return matchDAL.GetMatchJudgeListId(matchId);

        }


        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<V_MatchUser> GetMatchUser(int CompetitionId)
        {
            return matchDAL.GetMatchUser(CompetitionId);
        }

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public V_MatchUser GetMatchUser2(int CompetitionId, int UserId)
        {
            return matchDAL.GetMatchUser2(CompetitionId, UserId);
        }

        /// <summary>
        /// 提交保存分组信息
        /// </summary>
        /// <param name="list">提交分组的用户信息</param>
        /// <param name="groupId">groupId！=0表示只修改该分组用户信息，groupId==0：先删除全部手动分组用户，然后在添加分组用户</param>
        /// <param name="competitionId">竞赛Id</param>
        /// <returns></returns>
        public bool AddGroupUser(List<V_MatchUser> list, int groupId, int competitionId, out string errCode)
        {
            return matchDAL.AddGroupUser(list, groupId, competitionId, out  errCode);
        }

        /// <summary>
        /// 删除指定分组下的用户关联信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool DeleteGroupUser(int matchId, int groupId)
        {
            return matchDAL.DeleteGroupUser(matchId, groupId);
        }


        /// <summary>
        /// 判断用户是否已有分组
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public bool IsHaveGroup(int userId, int matchId)
        {
            return matchDAL.IsHaveGroup(userId, matchId);
        }

        /// <summary>
        /// 删除比赛信息，逻辑删除竞赛信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="collegeId"></param>
        /// <param name="isDelete">更新竞赛信息表字段IsDelete,1=竞赛管理员删除，0=正常，2=超管删除</param>
        /// <returns></returns>
        public bool DeleteMatch(int matchId, int collegeId, int isDelete)
        {
            return matchDAL.DeleteMatch(matchId, collegeId, isDelete);
        }


        /// <summary>
        /// 编辑竞赛信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool Update(Competition vm)
        {
            return matchDAL.Update(vm);
        }

        /// <summary>
        /// 修改竞赛用户组的审核信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool UpdateGroupAudit(int matchId, int groupId, int isAudit)
        {
            return matchDAL.UpdateGroupAudit(matchId, groupId, isAudit);
        }

        /// <summary>
        /// 复合赛-初赛成绩发布，设置入围人员
        /// </summary>
        /// <param name="vm">修改竞赛信息</param>
        /// <param name="groupIds">入围复赛的分组Ids</param>
        /// <returns></returns>
        public bool SetResult(Competition vm, string groupIds)
        {
            return matchDAL.SetResult(vm, groupIds);
        }

        /// <summary>
        /// 获取报名待审核用户数量
        /// </summary>
        /// <returns></returns>
        public int GetSiginupNotAduitNum(int collegeId)
        {
            return matchDAL.GetSiginupNotAduitNum(collegeId);
        }

        /// <summary>
        /// 获取报名待审核要跳转的竞赛ID
        /// </summary>
        /// <returns></returns>
        public int GetSiginupNotAduitMatchId(int collegeId)
        {
            return matchDAL.GetSiginupNotAduitMatchId(collegeId);
        }

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        public List<V_MatchUser> GetAllMatchUser()
        {
            return matchDAL.GetAllMatchUser();
        }

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Competition> GetMatchListWithCurUser(CustomFilter filter)
        {
            List<Competition> result = new List<Competition>();
            try
            {
                result = matchDAL.GetMatchListWithCurUser(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchListWithCurUser方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 待报名大赛列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <param name="sortType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Competition> GetMatchListNotJoin(int userId, int collegeId, int sortType, int pageIndex, int pageSize, out int totalCount)
        {
            List<Competition> result = new List<Competition>();
            totalCount = 0;
            try
            {
                result = DBHelper.GetPageList<Competition>(pageIndex, pageSize, matchDAL.GetMatchListNotJoin(userId, collegeId, sortType), out totalCount);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchListNotJoin方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 待报名大赛数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public int GetMatchListNotJoinNum(int userId, int collegeId)
        {
            int result = 0;
            try
            {
                result = matchDAL.GetMatchListNotJoinNum(userId, collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchListNotJoinNum方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 待参加大赛数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        public int GetMatchListHasJoinNum(int userId, int collegeId)
        {
            int result = 0;
            try
            {
                result = matchDAL.GetMatchListHasJoinNum(userId, collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchListHasJoinNum方法出错", ex);

            }
            return result;
        }

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<V_MatchResult> GetMatchResultList(CustomFilter filter)
        {
            List<V_MatchResult> result = new List<V_MatchResult>();
            try
            {
                result = matchDAL.GetMatchResultList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchResultUser方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        public List<V_UserScore> GetUserScoreList(CustomFilter filter)
        {
            List<V_UserScore> result = new List<V_UserScore>();
            try
            {
                result = matchDAL.GetUserScoreList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserScoreList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询竞赛报名信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<MatchApply> GetMatchApplyList(CustomFilter filter)
        {
            List<MatchApply> result = new List<MatchApply>();
            try
            {
                result = matchDAL.GetMatchApplyList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetMatchApplyList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 添加竞赛报名信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddApply(List<MatchApply> model)
        {
            List<MatchApply> result = new List<MatchApply>();
            try
            {
                return matchDAL.AddApply(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddApply方法出错", ex);
                return false;
            }
        }

        /// <summary>
        /// 删除竞赛报名信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DelApply(CustomFilter filter)
        {
            List<MatchApply> result = new List<MatchApply>();
            try
            {
                return matchDAL.DelApply(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DelApply方法出错", ex);
                return false;
            }
        }

        /// <summary>
        /// 修改竞赛报名状态
        /// </summary>
        /// <param name="search"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdApplyStatue(CustomFilter filter, List<MatchApply> applyUser, int status)
        {
            List<MatchApply> result = new List<MatchApply>();
            try
            {
                return matchDAL.UpdApplyStatue(filter, applyUser, status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdApplyStatue方法出错", ex);
                return false;
            }
        }

        /// <summary>
        /// 进入复赛时，修改其他组员的状态
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="GroupId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool UpdFinalStatus(int MatchId, int GroupId, int UserId)
        {
            List<MatchApply> result = new List<MatchApply>();
            try
            {
                return matchDAL.UpdFinalStatus(MatchId, GroupId, UserId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdFinalStatus方法出错", ex);
                return false;
            }
        }

        /// <summary>
        /// 批量修改竞赛用户组的审核信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool UpdateBatchGroupAudit(int matchId, List<int> groupId, int isAudit)
        {
            List<MatchApply> result = new List<MatchApply>();
            try
            {
                return matchDAL.UpdateBatchGroupAudit(matchId, groupId, isAudit);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdateBatchGroupAudit方法出错", ex);
                return false;
            }
        }

        /// <summary>
        /// 统计分析时候使用（加载人员信息和成绩信息）
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<V_UserScore> GetUserScorePageList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<V_UserScore> result = new List<V_UserScore>();
            try
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = DBHelper.GetPageList<V_UserScore>(pageIndex.Value, pageSize.Value, matchDAL.GetUserScoreParams(filter), out totalCount);
                }
                else
                {
                    result = matchDAL.GetUserScoreList(filter);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserScoreList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取考核成绩列表（用于统计分析）
        /// </summary>
        public List<V_UserMatchScore> GetUserMatchScoreList(CustomFilter filter)
        {
            List<V_UserMatchScore> result = new List<V_UserMatchScore>();
            try
            {
                result = matchDAL.GetUserMatchScoreList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserMatchScoreList方法出错", ex);
            }
            return result;
        }


        public List<int> GetIndexNum(int userId, int collegeId)
        {
            List<int> result = new List<int>();
            try
            {
                result = matchDAL.GetIndexNum(userId, collegeId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetIndexNum方法出错", ex);

            }
            return result;
        }
    }
}
