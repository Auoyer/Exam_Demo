using Exam.Svr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Exam.API
{
    public partial class ExamManage
    {

        private PaperDAL paperDal = new PaperDAL();
        private PaperUserSummaryDAL paperUserSummDal = new PaperUserSummaryDAL();
        private PaperUserAnswerDAL PAnswer = new PaperUserAnswerDAL();
        private PaperUserAnswerResultDAL PUAResult = new PaperUserAnswerResultDAL();

        #region 增/删

        /// <summary>
        /// 添加一张试卷
        /// </summary>
        /// <param name="model">试卷实体</param>
        /// <returns></returns>
        public int AddPaper(Paper model)
        {
            int rtnValue = 0;
            try
            {
                rtnValue = paperDal.AddPaper(model);
            }
            catch (Exception ex)
            {

                LogHelper.Log.WriteError("AddPaper", ex);
            }
            return rtnValue;

        }

        /// <summary>
        /// 添加一张试卷
        /// </summary>
        /// <param name="model">试卷实体</param>
        /// <returns></returns>
        public int AddPaperDetail2(PaperDetail model)
        {
            int rtnValue = 0;
            try
            {
                rtnValue = paperDal.Add2(model);
            }
            catch (Exception ex)
            {

                LogHelper.Log.WriteError("AddPaperDetail", ex);
            }
            return rtnValue;

        }



        /// <summary>
        /// 新增试卷题目答案
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public List<PaperUserAnswer> BatchAddPaperUserAnswer(List<PaperUserAnswer> PaperUserAnswer)
        {
            List<PaperUserAnswer> rtnValue = null;
            try
            {
                rtnValue = PAnswer.BatchAddPaperUserAnswer(PaperUserAnswer);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("BatchAddPaperUserAnswer", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePaper(int id)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = paperDal.DeletePaper(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeletePaper", ex);
            }
            return rtnValue;
        }

        #endregion

        #region 改

        /// <summary>
        /// 编辑试卷基本信息（不包含状态）----试卷管理  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditPaper(Paper model)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = paperDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("EditPaper", ex);
            }
            return rtnValue;
        }
        /// <summary>
        /// 编辑试卷状态
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool EditPaperStatus(int paperId, int status)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = paperDal.UpdatePaperStatus(paperId, status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("EditPaperStatus", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 用户领取试卷
        /// </summary>
        /// <param name="model">得分初始化实体</param>
        /// <returns></returns>
        public int TakeUpPaper(PaperUserSummary model)
        {
            int rtnValue = 0;
            try
            {
                rtnValue = paperUserSummDal.Add(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("TakeUpPaper", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 用户完成试卷
        /// </summary>
        /// <param name="model">得分初始化实体</param>
        /// <returns></returns>
        public bool UpDatePaperSummary(PaperUserSummary model)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = paperUserSummDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpDatePaper", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="anResult">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        public int AnswerQuetion(List<PaperUserAnswer> answers, PaperUserAnswerResult anResult)
        {
            int rtnValue = 0;
            try
            {
                PaperUserAnswerResultDAL dal = new PaperUserAnswerResultDAL();
                rtnValue = dal.AnswerQuetion(answers, anResult);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AnswerQuetion", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 更新（新增）用户答题结果（教师评分、标记状态）
        /// </summary>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        public int EditAnswerResult(PaperUserAnswerResult summary)
        {
            int rtnValue = 0;
            try
            {
                PaperUserAnswerResultDAL dal = new PaperUserAnswerResultDAL();
                if (summary.Id > 0)
                {
                    rtnValue = dal.Update(summary) ? 1 : 0;
                }
                else
                {
                    rtnValue = dal.Add(summary);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("EditAnswerResult", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 更新用户试卷得分情况（教师评分）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePaperUserSummary(PaperUserSummary model)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = paperUserSummDal.Update(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdatePaperUserSummary", ex);
            }
            return rtnValue;
 
        }

        #endregion

        #region 查

        public Paper GetPaper(int paperID, PaperFilter pFilter)
        {
            Paper rtnValue = null;
            try
            {
                rtnValue = paperDal.GetPaper(paperID, pFilter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPaper", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 获取大赛的理论试卷Id
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public int GetPaperIdByMatch(int matchId)
        {
            int result = 0;
            try
            {
                result = paperDal.GetPaperIdByMatch(matchId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPaperIdByMatch", ex);
            }
            return result;
        }


        public bool GetPaperDetail(int QuestionId)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = paperDal.Exists2(QuestionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPaperDetal", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public List<Paper> GetExamPaperList(CustomFilter filter, PaperFilter pFilter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Paper> result = new List<Paper>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return result;
                }
                result = DBHelper.GetPageList<Paper>(pageIndex, pageSize, paperDal.GetPaperPageParams(filter, pFilter), out totalCount);
                if (pFilter != null)
                {
                    var IdList = result.Select(l => l.Id).ToList();

                    Paper tmpMod = new Paper();

                    paperDal.GetPaperExInfo(IdList, pFilter, tmpMod);
                    Parallel.ForEach(result, m =>
                    {
                        m.CharpterList = pFilter.CharpterList ? tmpMod.CharpterList.Where(l => l.PaperID == m.Id).ToList() : null;
                        m.Details = pFilter.Details ? tmpMod.Details.Where(l => l.ExamPaperId == m.Id).ToList() : null;
                        m.ScoreInfo = pFilter.ScoreInfo ? tmpMod.ScoreInfo.Where(l => l.PaperID == m.Id).ToList() : null;
                        m.ClassList = pFilter.ClassList ? tmpMod.ClassList.Where(l => l.ExamPaperId == m.Id).ToList() : null;
                        m.UserAnswer = pFilter.UserAnswer ? tmpMod.UserAnswer.Where(l => l.ExamPaperId == m.Id).ToList() : null;
                        m.UserAnswerResult = pFilter.UserAnswerResult ? tmpMod.UserAnswerResult.Where(l => l.ExamPaperId == m.Id).ToList() : null;
                        m.UserSummary = pFilter.UserSummary ? tmpMod.UserSummary.Where(l => l.ExamPaperId == m.Id).ToList() : null;
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetExamPaperList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public List<Paper> GetPaperList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Paper> result = new List<Paper>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return result;
                }
                result = DBHelper.GetPageList<Paper>(pageIndex, pageSize, paperDal.GetPaperList(filter), out totalCount);

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPaperList方法出错", ex);
            }
            return result;
        }



        /// <summary>
        /// 分页查询题目
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Paper> GetAllPaperList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Paper> result = new List<Paper>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    return result;
                }
                if (filter == null)
                {
                    filter = new CustomFilter();
                }
                result = DBHelper.GetPageList<Paper>(pageIndex, pageSize, paperDal.GetList(filter), out totalCount);

                if (result != null && result.Count > 0)
                {
                    var IdList = result.Select(l => l.Id).ToList();
                    Paper tmpMod = new Paper();

                    PaperFilter pap = new PaperFilter();
                    pap.CharpterList = true;
                    pap.Details = true;
                    pap.ScoreInfo = true;
                    //pap.ClassList = true;
                    //pap.UserAnswer = true;
                    //pap.UserAnswerResult = true;
                    //pap.UserSummary = true;

                    paperDal.GetPaperExInfo(IdList, pap, tmpMod);

                    Parallel.ForEach(result, m =>
                    {
                        m.CharpterList = tmpMod.CharpterList.Where(l => l.PaperID == m.Id).ToList();
                        m.Details = tmpMod.Details.Where(l => l.ExamPaperId == m.Id).ToList();
                        m.ScoreInfo = tmpMod.ScoreInfo.Where(l => l.PaperID == m.Id).ToList();
                        //m.ClassList = tmpMod.ClassList.Where(l => l.ExamPaperId == m.Id).ToList();
                        //m.UserAnswer = tmpMod.UserAnswer.Where(l => l.ExamPaperId == m.Id).ToList();
                        //m.UserAnswerResult = tmpMod.UserAnswerResult.Where(l => l.ExamPaperId == m.Id).ToList();
                        //m.UserSummary = tmpMod.UserSummary.Where(l => l.ExamPaperId == m.Id).ToList();
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetAllPaperList方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public List<PaperUserSummary> GetUserSummaryList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount)
        {
            totalCount = 0;
            List<PaperUserSummary> result = new List<PaperUserSummary>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    result = paperUserSummDal.GetList(filter);
                }
                else
                {
                    result = DBHelper.GetPageList<PaperUserSummary>(pageIndex.Value, pageSize.Value, paperUserSummDal.GetPaperUserSummaryPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserSummaryList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 分页获取试卷章节列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public List<PaperCharpter> GetPaperCharpterList(CustomFilter filter)
        {
           
            List<PaperCharpter> result = new List<PaperCharpter>();
            try
            {
                result = paperUserSummDal.GetPaperCharpterList(filter);               
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetPaperCharpterList方法出错", ex);
            }
            return result;
        }
         
        /// <summary>
        /// 分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public List<PaperUserAnswerResult> GetUserAnswerResultList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<PaperUserAnswerResult> result = new List<PaperUserAnswerResult>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    result = PUAResult.GetList(filter);
                }
                else
                {
                    result = DBHelper.GetPageList<PaperUserAnswerResult>(pageIndex, pageSize, PUAResult.GetPaperUserAnswerResultPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserAnswerResultList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 多表关联分页获取试卷列表
        /// </summary>
        /// <param name="filter">查询过滤器</param>
        /// <param name="pFilter">试卷查询过滤器</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public List<PaperUserAnswerResult> GetUserAnswerResultList1(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<PaperUserAnswerResult> result = new List<PaperUserAnswerResult>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    result = PUAResult.GetList1(filter);
                }
                else
                {
                    result = DBHelper.GetPageList<PaperUserAnswerResult>(pageIndex, pageSize, PUAResult.GetPaperUserAnswerResultPageParams1(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserAnswerResultList1方法出错", ex);
            }
            return result;
        }


        /// <summary>
        /// 班级集合中，是否有正在进行的卷子
        /// </summary>
        /// <param name="classId">班级集合</param>
        /// <returns></returns>
        public bool IsPaperPublish(List<int> classId)
        {
            bool result = false;
            try
            {
                result = paperDal.IsPaperPublish(classId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("IsPaperPublish方法出错", ex);
            }
            return result;
        }

        #endregion
        /// <summary>
        /// 添加考点内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPaperDetail(List<PaperDetail> list)
        {
            bool result = false;
            try
            {
                result = paperDal.AddPaperDetail(list);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddPaperDetail", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新考点内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePaperDetail(PaperDetail model)
        {
            bool result = false;
            try
            {
                result = paperDal.UpdatePaperDetail(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdatePaperDetail", ex);
            }
            return result;
        }
        /// <summary>
        /// 更新考点内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeletePaperDetail(PaperDetail model)
        {
            bool result = false;
            try
            {
                result = paperDal.DeletePaperDetail(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeletePaperDetail", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新数量内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePaperScore(PaperScore model)
        {
            bool result = false;
            try
            {
                result = paperDal.UpdatePaperScore(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdatePaperScore", ex);
            }
            return result;
        }
        /// <summary>
        /// 删除题型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeletePaperScore(int id)
        {
            bool result = false;
            try
            {
                result = paperDal.DeletePaperScore(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeletePaperScore", ex);
            }
            return result;
        }

        /// <summary>
        /// 保存内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SavePaperScore(List<PaperScore> list)
        {
            bool result = false;
            try
            {
                result = paperDal.SavePaperScore(list);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("SavePaperScore", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新数量内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdatePaperScoreList(List<PaperScore> list)
        {
            bool result = false;
            try
            {
                result = paperDal.UpdatePaperScoreList(list);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("UpdatePaperScoreList", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取班级理论考试月平局分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TheoryClassScore> GetTheoryClassScoreList(int userId)
        {
            List<TheoryClassScore> result = new List<TheoryClassScore>();
            try
            {
                result = paperDal.GetTheoryClassScoreList(userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTheoryClassScoreList", ex);
 
            }

            return result;
 
        }

        /// <summary>
        /// 统计教师下未评分的理论考核/认证考试试卷
        /// </summary>
        /// <param name="userId">教师Id</param>
        /// <param name="libraryID">理论考核/认证考试</param>
        /// <returns></returns>
        public int CountSubmittedPaper(int userId, int libraryID)
        {
            int result = 0;
            try
            {
                result = paperDal.CountSubmittedPaper(userId,libraryID);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("CountSubmittedPaper", ex);

            }
            return result;
        }

        /// <summary>
        /// 获取用户试卷概况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PaperUserSummary GetPUSummary(int userId, int competitionId)
        {
            try
            {
                return paperUserSummDal.GetModel(userId, competitionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddPaperDetail", ex);
            }
            return null;
        }

        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="anResult">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        public int AnswerQuetion2(List<PaperUserAnswer> answers, PaperUserAnswerResult anResult)
        {
            int rtnValue = 0;
            try
            {
                PaperUserAnswerResultDAL dal = new PaperUserAnswerResultDAL();
                rtnValue = dal.AnswerQuetion2(answers, anResult);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AnswerQuetion2", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 获取用户考试试卷（存储过程）
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Paper GetUserPaperProc(int paperId, int userId)
        {
            Paper rtnValue = null;
            try
            {
                rtnValue = paperDal.GetUserPaperProc(paperId, userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetUserPaperProc", ex);
            }
            return rtnValue;
        }

    }
}
