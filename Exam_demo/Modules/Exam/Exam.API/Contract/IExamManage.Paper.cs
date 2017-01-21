using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Exam.API
{
    public partial interface IExamManage
    {
        #region 增/删

        /// <summary>
        /// 添加一张试卷
        /// </summary>
        /// <param name="model">试卷实体</param>
        /// <returns></returns>
        [OperationContract]
        int AddPaper(Paper model);

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeletePaper(int id);

        /// <summary>
        /// 添加试卷试题
        /// </summary>
        /// <param name="model">试卷实体</param>
        /// <returns></returns>
        [OperationContract]
        int AddPaperDetail2(PaperDetail model);

        #endregion

        #region 改

        /// <summary>
        /// 编辑试卷基本信息（不包含状态）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool EditPaper(Paper model);
        /// <summary>
        /// 编辑试卷状态
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        bool EditPaperStatus(int paperId, int status);

        /// <summary>
        /// 用户领取试卷
        /// </summary>
        /// <param name="model">得分初始化实体</param>
        /// <returns></returns>
        [OperationContract]
        int TakeUpPaper(PaperUserSummary model);

        /// <summary>
        /// 完成试卷
        /// </summary>
        /// <param name="model">得分初始化实体</param>
        /// <returns></returns>
        [OperationContract]
        bool UpDatePaperSummary(PaperUserSummary model);

        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        [OperationContract]
        int AnswerQuetion(List<PaperUserAnswer> answers, PaperUserAnswerResult summary);

        /// <summary>
        /// 更新（新增）用户答题结果（教师评分、标记状态）
        /// </summary>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        [OperationContract]
        int EditAnswerResult(PaperUserAnswerResult summary);

        /// <summary>
        /// 更新用户试卷得分情况（教师评分）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePaperUserSummary(PaperUserSummary model);

        #endregion

        #region 查

        /// <summary>
        /// 获取单个试卷的信息
        /// </summary>
        /// <param name="paperID"></param>
        /// <param name="pFilter"></param>
        /// <returns></returns>
        [OperationContract]
        Paper GetPaper(int paperID, PaperFilter pFilter);

        /// <summary>
        /// 获取大赛的理论试卷Id
        /// </summary>
        /// <param name="paperID"></param>
        /// <param name="pFilter"></param>
        /// <returns></returns>
        [OperationContract]
        int GetPaperIdByMatch(int matchId);

        /// <summary>
        /// 获取试卷列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Paper> GetAllPaperList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<Paper> GetExamPaperList(CustomFilter filter, PaperFilter pFilter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<Paper> GetPaperList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 分页获取答题试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<PaperUserSummary> GetUserSummaryList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 获取试卷章节列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<PaperCharpter> GetPaperCharpterList(CustomFilter filter);

        /// <summary>
        /// 分页获取答题试卷答案列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<PaperUserAnswerResult> GetUserAnswerResultList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 多表联查分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<PaperUserAnswerResult> GetUserAnswerResultList1(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 班级集合中，是否有正在进行的卷子
        /// </summary>
        /// <param name="classId">班级集合</param>
        /// <returns></returns>
        [OperationContract]
        bool IsPaperPublish(List<int> classId);

        /// <summary>
        /// 获取试卷的详细信息
        /// </summary>
        /// <param name="paperID"></param>
        /// <param name="pFilter"></param>
        /// <returns></returns>
        [OperationContract]
        bool GetPaperDetail(int QuestionId);

        #endregion

        /// <summary>
        /// 更新考点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddPaperDetail(List<PaperDetail> list);

        /// <summary>
        /// 更新考点内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePaperDetail(PaperDetail model);

        /// <summary>
        /// 更新考点内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeletePaperDetail(PaperDetail model);

        /// <summary>
        /// 更新考题数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePaperScore(PaperScore model);

        /// <summary>
        /// 删除题型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeletePaperScore(int id);

        /// <summary>
        /// 添加考题数量
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [OperationContract]
        bool SavePaperScore(List<PaperScore> List);
        /// <summary>
        /// 指量更新考题数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePaperScoreList(List<PaperScore> list);

        /// <summary>
        /// 获取班级理论考试月平局分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        List<TheoryClassScore> GetTheoryClassScoreList(int userId);

        /// <summary>
        /// 统计教师下未评分的理论考核/认证考试试卷
        /// </summary>
        /// <param name="userId">教师Id</param>
        /// <param name="libraryID">理论考核/认证考试</param>
        /// <returns></returns>
        [OperationContract]
        int CountSubmittedPaper(int userId, int libraryID);

        /// <summary>
        /// 获取用户试卷概况
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        [OperationContract]
        PaperUserSummary GetPUSummary(int userId, int competitionId);


        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        [OperationContract]
        int AnswerQuetion2(List<PaperUserAnswer> answers, PaperUserAnswerResult summary);

        /// <summary>
        /// 获取用户考试试卷（存储过程）
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        Paper GetUserPaperProc(int paperId, int userId);
    }
}
