using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    public partial interface IMatchManage
    {
        /// <summary>
        /// 验证竞赛名称是否重复
        /// </summary>
        /// <param name="matchName">竞赛名称，不重复返回true</param>
        /// <returns>不重复返回true</returns>
        [OperationContract]
        bool IsMatchNameRepeat(string matchName, int collegeId);

        /// <summary>
        /// 创建大赛，关联评委
        /// </summary>
        [OperationContract]
        bool CreateMatch(Competition model, List<int> listJudgeId);


        /// <summary>
        /// 判断用户是否有正在进行的比赛，true=没有正在进行的比赛
        /// </summary>
        /// <param name="listJudgeId">List 评委ID</param>
        /// <param name="userType">用户类型，1=评委，2=参赛用户</param>
        /// <returns></returns>
        [OperationContract]
        bool IsJudgeConductMatch(List<int> listJudgeId, int userType, int collegeId);


        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<Competition> GetMatchLsit(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        [OperationContract]
        Competition GetModel(int Id);

        /// <summary>
        /// 编辑竞赛信息，及评委信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMatch(Competition vm);

        /// <summary>
        /// 查询竞赛评审Ids
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        List<int> GetMatchJudgeListId(int matchId);

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_MatchUser> GetMatchUser(int CompetitionId);

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        [OperationContract]
        V_MatchUser GetMatchUser2(int CompetitionId, int UserId);

        /// <summary>
        /// 提交保存分组信息
        /// </summary>
        /// <param name="list">提交分组的用户信息</param>
        /// <param name="groupId">groupId！=0表示只修改该分组用户信息，groupId==0：先删除全部手动分组用户，然后在添加分组用户</param>
        /// <param name="competitionId">竞赛Id</param>
        /// <returns></returns>
        [OperationContract]
        bool AddGroupUser(List<V_MatchUser> list, int groupId, int competitionId, out string errCode);


        /// <summary>
        /// 删除指定分组下的用户关联信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteGroupUser(int matchId, int groupId);

        /// <summary>
        /// 判断用户是否已有分组
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsHaveGroup(int userId, int matchId);


        /// <summary>
        /// 删除比赛信息，逻辑删除竞赛信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="collegeId"></param>
        /// <param name="isDelete">更新竞赛信息表字段IsDelete,1=竞赛管理员删除，0=正常，2=超管删除</param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMatch(int matchId, int collegeId, int isDelete);

        /// <summary>
        /// 编辑竞赛信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [OperationContract]
        bool Update(Competition vm);

        /// <summary>
        /// 修改竞赛用户组的审核信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateGroupAudit(int matchId, int groupId, int isAudit);

        /// <summary>
        /// 复合赛-初赛成绩发布，设置入围人员
        /// </summary>
        /// <param name="vm">修改竞赛信息</param>
        /// <param name="groupIds">入围复赛的分组Ids</param>
        /// <returns></returns>
        [OperationContract]
        bool SetResult(Competition vm, string groupIds);



        /// <summary>
        /// 获取报名待审核用户数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetSiginupNotAduitNum(int collegeId);


        /// <summary>
        /// 获取报名待审核要跳转的竞赛ID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetSiginupNotAduitMatchId(int collegeId);

        /// <summary>
        /// 查询竞赛下的参赛用户分组
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_MatchUser> GetAllMatchUser();

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<Competition> GetMatchListWithCurUser(CustomFilter filter);

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
        [OperationContract]
        List<Competition> GetMatchListNotJoin(int userId, int collegeId, int sortType, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 待报名大赛数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetMatchListNotJoinNum(int userId, int collegeId);

        /// <summary>
        /// 待参加大赛数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collegeId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetMatchListHasJoinNum(int userId, int collegeId);

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_MatchResult> GetMatchResultList(CustomFilter filter);

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_UserScore> GetUserScoreList(CustomFilter filter);

        /// <summary>
        /// 获取考核成绩列表（用于统计分析）
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_UserMatchScore> GetUserMatchScoreList(CustomFilter filter);

        /// <summary>
        /// 根据条件查询竞赛报名信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<MatchApply> GetMatchApplyList(CustomFilter filter);

        /// <summary>
        /// 添加竞赛报名信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddApply(List<MatchApply> model);

        /// <summary>
        /// 删除竞赛报名信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool DelApply(CustomFilter filter);

        /// <summary>
        /// 修改竞赛报名状态
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdApplyStatue(CustomFilter filter, List<MatchApply> applyUser, int status);

        /// <summary>
        /// 进入复赛时，修改其他组员的状态
        /// </summary>
        /// <param name="MatchId"></param>
        /// <param name="GroupId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdFinalStatus(int MatchId, int GroupId, int UserId);


        /// <summary>
        /// 批量修改竞赛用户组的审核信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateBatchGroupAudit(int matchId, List<int> groupId, int isAudit);

        /// <summary>
        /// 加载人员信息和成绩信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<V_UserScore> GetUserScorePageList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);


        [OperationContract]
        List<int> GetIndexNum(int userId, int collegeId);
    }
}
