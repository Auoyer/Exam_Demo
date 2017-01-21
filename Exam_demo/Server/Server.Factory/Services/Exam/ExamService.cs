using Exam.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using VM;

namespace Server.Factory
{
    public class ExamService : IService<IExamManage>
    {
        protected override string GetTestActionName()
        {
            return "Test";
        }

        #region 试卷部分

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PaperVM> GetAllPaperList(TrainSearch ts)
        {

            List<PaperVM> rtnValue = new List<PaperVM>();

            int pageLength = 500;
            int totalCount = 0;
            var curPage = MyService.GetAllPaperList(null, 1, pageLength, out totalCount);
            if (curPage != null && curPage.Count > 0)
            {
                rtnValue.AddRange(curPage.MapList<PaperVM, Paper>());

                int remainder = totalCount % 500;
                int pageCount = remainder == 0 ? totalCount / 500 : (totalCount - remainder) / 500 + 1;
                for (int i = 2; i <= remainder; i++)
                {
                    curPage = MyService.GetAllPaperList(null, i, pageLength, out totalCount);

                    if (curPage != null && curPage.Count > 0)
                    {
                        rtnValue.AddRange(curPage.MapList<PaperVM, Paper>());
                    }
                }
            }
            return rtnValue;
        }

        public PaperVM GetPaper(int paperId)
        {
            PaperFilter filter = new PaperFilter();
            filter.ScoreInfo = true;
            filter.CharpterList = true;
            //filter.ClassList = true;
            filter.Details = true;
            //filter.UserAnswer = true;
            //filter.UserAnswerResult = true;
            //filter.UserSummary = true;
            //filter.CompetitionList = true;
            return MyService.GetPaper(paperId, filter).Map<PaperVM, Paper>();
        }

        /// <summary>
        /// 获取大赛的理论试卷Id
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public int GetPaperIdByMatch(int matchId)
        {
            return MyService.GetPaperIdByMatch(matchId);
        }

        /// <summary>
        /// 获取算分用试卷信息
        /// </summary>
        /// <param name="paperId"></param>
        /// <returns></returns>
        public PaperVM GetPaperForScore(int paperId)
        {
            PaperFilter filter = new PaperFilter();
            filter.UserAnswerResult = true;
            filter.UserSummary = true;
            return MyService.GetPaper(paperId, filter).Map<PaperVM, Paper>();
        }

        public PaperVM GetUserPaper(int paperId, int UserId)
        {
            PaperFilter filter = new PaperFilter();
            filter.UserAnswer = true;
            filter.UserAnswerResult = true;
            filter.UserSummary = true;
            filter.AnswererID = UserId;
            return MyService.GetPaper(paperId, filter).Map<PaperVM, Paper>();
        }

        public PaperVM GetUserPaper2(int paperId, int UserId)
        {
            PaperFilter filter = new PaperFilter();
            filter.ScoreInfo = true;
            filter.CharpterList = true;
            filter.Details = true;
            filter.UserAnswer = true;
            filter.UserAnswerResult = true;
            filter.UserSummary = true;

            filter.AnswererID = UserId;
            return MyService.GetPaper(paperId, filter).Map<PaperVM, Paper>();
        }

        public PaperVM GetUserPaperFinishExam(int paperId, int UserId)
        {
            PaperFilter filter = new PaperFilter();
            filter.UserAnswerResult = true;
            filter.UserSummary = true;

            filter.AnswererID = UserId;
            return MyService.GetPaper(paperId, filter).Map<PaperVM, Paper>();
        }

        public PaperVM GetUserPaperCheckExam(int paperId, int UserId)
        {
            PaperFilter filter = new PaperFilter();
            filter.UserAnswer = true;
            filter.Details = true;
            filter.AnswererID = UserId;
            return MyService.GetPaper(paperId, filter).Map<PaperVM, Paper>();
        }

        public int AddPaper(PaperVM Model)
        {
            Paper entity = Model.Map<Paper, PaperVM>();
            return MyService.AddPaper(entity);
        }

        /// <summary>
        /// 现在试卷试题
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int AddPaperDetail2(PaperDetailVM Model)
        {
            PaperDetail entity = Model.Map<PaperDetail, PaperDetailVM>();
            return MyService.AddPaperDetail2(entity);
        }

        public bool UpdatePaper(PaperVM Model)
        {
            Paper entity = Model.Map<Paper, PaperVM>();
            return MyService.EditPaper(entity);
        }

        /// <summary>
        /// 领取试卷
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public int TakeUpPaper(PaperUserSummaryVM Model)
        {
            PaperUserSummary entity = Model.Map<PaperUserSummary, PaperUserSummaryVM>();
            return MyService.TakeUpPaper(entity);
        }

        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        public int AnswerQuetion(List<PaperUserAnswerVM> answers, PaperUserAnswerResultVM summary)
        {
            var answerList = answers != null && answers.Count > 0 ? answers.MapList<PaperUserAnswer, PaperUserAnswerVM>() : new List<PaperUserAnswer>();
            return MyService.AnswerQuetion(answerList, summary.Map<PaperUserAnswerResult, PaperUserAnswerResultVM>());
        }

        /// <summary>
        /// 用户完成答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        public bool UpDatePaperSummary(PaperUserSummaryVM summary)
        {
            PaperUserSummary entity = summary.Map<PaperUserSummary, PaperUserSummaryVM>();
            return MyService.UpDatePaperSummary(entity);
        }

        /// <summary>
        /// 更新用户试卷得分情况（教师评分）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePaperUserSummary(PaperUserSummaryVM model)
        {
            return MyService.UpdatePaperUserSummary(model.Map<PaperUserSummary, PaperUserSummaryVM>());
        }

        #endregion

        #region 题目部分

        /// <summary>
        /// 分批获取所有的题目信息(无查询条件)
        /// </summary>
        /// <returns></returns>
        public List<QuestionVM> GetAllQuestions(QuestionSearch search)
        {
            List<QuestionVM> rtnValue = new List<QuestionVM>();
            CustomFilter filter = null;
            if (search != null) {
                filter = new CustomFilter
                {
                    Status = search.Status,
                    CollegeId = search.CollegeId
                };
            }

            int pageLength = 500;
            int totalCount = 0;
            var curPage = MyService.GetQuestionList(filter, 1, pageLength, out totalCount);
            if (curPage != null && curPage.Count > 0)
            {
                rtnValue.AddRange(curPage.MapList<QuestionVM, Question>());

                totalCount = 5569;
                int remainder = totalCount % 500;
                int pageCount = remainder == 0 ? totalCount / 500 : (totalCount - remainder) / 500 + 1;
                for (int i = 2; i <= pageCount; i++)
                {
                    curPage = MyService.GetQuestionList(filter, i, pageLength, out totalCount);
                    if (curPage != null && curPage.Count > 0)
                    {
                        rtnValue.AddRange(curPage.MapList<QuestionVM, Question>());
                    }
                }
            }
            return rtnValue;
        }


        /// <summary>
        /// 批量新增
        /// </summary>
        /// <returns></returns>
        public List<QuestionVM> BatchAddQuestion(List<QuestionVM> questions)
        {
            List<Question> ques = questions.MapList<Question, QuestionVM>();
            return MyService.BatchAddQuestion(ques).MapList<QuestionVM, Question>();
        }

        /// <summary>
        /// 超管-单校题库-分批获取所有的题目信息
        /// </summary>
        /// <returns></returns>
        public List<QuestionVM> GetAllQuestions3(QuestionSearch search, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                CharpterID = search.CharpterID,
                StructType = search.QuestionTypeId,
                Status = search.Status,
                CollegeId = search.CollegeId,
                BySchool = search.BySchool
            };
            List<QuestionVM> rtnValue = new List<QuestionVM>();
            var curPage = MyService.GetQuestionList(filter, pageIndex, pageSize, out totalCount);
            rtnValue = curPage.MapList<QuestionVM, Question>();
            return rtnValue;
        }

        /// <summary>
        /// 分批获取所有的题目信息
        /// </summary>
        /// <returns></returns>
        public List<QuestionVM> GetAllQuestions2(QuestionSearch search, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            CustomFilter filter = new CustomFilter
            {
                CharpterID = search.CharpterID,
                StructType = search.QuestionTypeId,
                Status = search.Status,
                CollegeId = search.CollegeId,
                KeyWords = search.KeyWords,
                Listtypeid = string.Join(",", search.Listtypeid ?? new List<int>())
            };
            List<QuestionVM> rtnValue = new List<QuestionVM>();
            var curPage = MyService.GetQuestionList(filter, pageIndex, pageSize, out totalCount);
            rtnValue = curPage.MapList<QuestionVM, Question>();
            return rtnValue;
        }

        /// <summary>
        /// 分批获取所有的题目信息
        /// </summary>
        /// <returns></returns>
        public List<QuestionVM> GetSomeQuestions(QuestionSearch search)
        {
            CustomFilter filter = new CustomFilter
            {
                Id = search.Id,
                Context = search.Context,
                CharpterID = search.CharpterID,
                CollegeId = search.CollegeId,
                StructType = search.QuestionTypeId,
                isBool = search.isBool,
                Status = search.Status,
                LiburaryId = search.CertificationId
            };
            List<QuestionVM> rtnValue = new List<QuestionVM>();

            var curPage = MyService.GetSomeQuestionList(filter);
            rtnValue = curPage.MapList<QuestionVM, Question>();
            return rtnValue;
        }

        /// <summary>
        /// 获取单个题目的信息
        /// </summary>
        /// <returns></returns>
        public QuestionVM GetQuestion(int questionId)
        {
            QuestionVM rtnValue = null;

            var tmpModel = MyService.GetQuestion(questionId);
            if (tmpModel != null)
            {
                rtnValue = tmpModel.Map<QuestionVM, Question>();
            }
            return rtnValue;
        }

        /// <summary>
        /// 删除单个
        /// </summary>
        /// <returns></returns>
        public bool DeleteQuestion(int id)
        {
            bool rtnValue = true;
            rtnValue = MyService.DeleteQuestion(id);
            return rtnValue;
        }

        /// <summary>
        /// 批量删除题目
        /// 定时器180分钟清除假删习题
        /// </summary>
        /// <param name="ids">伪删除试题集合</param>
        /// <returns>未组卷的试题集合</returns>
        public List<int> RemoveQuestion(List<int> ids)
        {
            List<int> rtnValue = MyService.RemoveQuestion(ids);
            return rtnValue;
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        public bool EditQuestionStatus(int questionId, int status)
        {
            bool rtnValue = true;
            rtnValue = MyService.EditQuestionStatus(questionId, status);
            return rtnValue;
        }

        /// <summary>
        /// 修改状态2（批量逻辑删除习题）
        /// </summary>
        /// <returns></returns>
        public bool EditQuestionStatus2(List<int> Id)
        {
            bool rtnValue = true;
            rtnValue = MyService.EditQuestionStatus2(Id);
            return rtnValue;
        }

        /// <summary>
        /// 判断题目是否存在于试卷中
        /// </summary>
        /// <returns></returns>
        public bool GetPaperDetail(int questionId)
        {
            bool rtnValue = false;
            rtnValue = MyService.GetPaperDetail(questionId);
            return rtnValue;
        }
        #endregion

        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddQuestion(QuestionVM model)
        {
            int result = 0;
            Question models = new Question();
            models = model.Map<Question, QuestionVM>();
            result = MyService.AddQuestion(models);
            return result;
        }

        public bool AddPaperDetail(List<PaperDetailVM> list)
        {
            List<PaperDetail> psList = new List<PaperDetail>();
            psList = list.MapList<PaperDetail, PaperDetailVM>();
            return MyService.AddPaperDetail(psList);
        }

        public bool DeletePaperDetail(PaperDetailVM model)
        {
            PaperDetail entity = new PaperDetail();
            entity = model.Map<PaperDetail, PaperDetailVM>();
            return MyService.DeletePaperDetail(entity);
        }

        public bool UpdatePaperScore(PaperScoreVM model)
        {
            PaperScore entity = new PaperScore();
            entity = model.Map<PaperScore, PaperScoreVM>();
            return MyService.UpdatePaperScore(entity);
        }
        public bool DeletePaperScore(int id)
        {
            return MyService.DeletePaperScore(id);
        }
        public bool SavePaperScore(List<PaperScoreVM> list)
        {
            List<PaperScore> entity = new List<PaperScore>();
            entity = list.MapList<PaperScore, PaperScoreVM>();
            return MyService.SavePaperScore(entity);
        }

        public bool UpdatePaperScoreList(List<PaperScoreVM> list)
        {
            List<PaperScore> psList = new List<PaperScore>();
            psList = list.MapList<PaperScore, PaperScoreVM>();
            return MyService.UpdatePaperScoreList(psList);
        }

        public List<PaperUserSummaryVM> GetAllUserSummaryByPaperId(int paperId)
        {
            int totalCount = 0;
            CustomFilter filter = new CustomFilter() { Id2 = paperId };
            var list = MyService.GetUserSummaryList(filter, 0, 0, out totalCount);
            return list.MapList<PaperUserSummaryVM, PaperUserSummary>();
        }

        /// <summary>
        /// 用户答题
        /// </summary>
        /// <param name="answers">答案列表</param>
        /// <param name="summary">答题结果实体</param>
        /// <returns>答题结果实体ID</returns>
        public int AnswerQuetion2(List<PaperUserAnswerVM> answers, PaperUserAnswerResultVM summary)
        {
            var answerList = answers != null && answers.Count > 0 ? answers.MapList<PaperUserAnswer, PaperUserAnswerVM>() : new List<PaperUserAnswer>();
            return MyService.AnswerQuetion2(answerList, summary.Map<PaperUserAnswerResult, PaperUserAnswerResultVM>());
        }

        /// <summary>
        /// 修改习题查看状态
        /// </summary>
        /// <returns></returns>
        public bool ChangeQuestionViewStatus(int id)
        {
            bool rtnValue = true;
            rtnValue = MyService.ChangeQuestionViewStatus(id);
            return rtnValue;
        }

        /// <summary>
        /// 获取用户考试试卷（存储过程）
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PaperVM GetUserPaperProc(int paperId, int userId)
        {
            return MyService.GetUserPaperProc(paperId, userId).Map<PaperVM, Paper>();
        }
    }
}
