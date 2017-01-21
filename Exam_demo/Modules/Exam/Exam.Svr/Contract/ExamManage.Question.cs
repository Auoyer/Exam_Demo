using Exam.Svr;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Utils;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.API
{
    public partial class ExamManage
    {
        private QuestionDAL questionDal = new QuestionDAL();
        private QuestionHiddenDAL qHiddenDal = new QuestionHiddenDAL();

        #region 增/删
        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddQuestion(Question model)
        {
            int rtnValue = 0;
            try
            {
                rtnValue = questionDal.AddQuestion(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddQuestion", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 批量新增题目
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public List<Question> BatchAddQuestion(List<Question> questions)
        {
            List<Question> rtnValue = null;
            try
            {
                rtnValue = questionDal.BatchAddQuestion(questions);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("BatchAddQuestion", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 批量删除题目
        /// </summary>
        /// <param name="ids">伪删除试题集合</param>
        /// <returns>未组卷的试题集合</returns>
        public List<int> RemoveQuestion(List<int> ids)
        {
            List<int> rtnValue = new List<int>();
            try
            {
                rtnValue = questionDal.RemoveQuestion(ids);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("RemoveQuestion", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 删除题目（当）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteQuestion(int id)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = questionDal.Delete(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("DeleteQuestion", ex);
            }
            return rtnValue;
        }

        #endregion

        #region 改

        /// <summary>
        /// 编辑问题状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool EditQuestionStatus(int questionId, int status)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = questionDal.EditQuestionStatus(questionId, status);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("EditQuestionStatus", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 编辑问题状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool EditQuestionStatus2(List<int> Id)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = questionDal.EditQuestionStatus2(Id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("EditQuestionStatus2", ex);
            }
            return rtnValue;
        }

        /// <summary>
        /// 屏蔽题目
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="questionId"></param>
        /// <param name="hiddenOrShow"></param>
        /// <returns></returns>
        public int HiddenQuestion(int userId, int questionId, bool hiddenOrShow, bool IsDelete)
        {
            int rtnValue = 0;
            try
            {
                if (hiddenOrShow)
                {
                    qHiddenDal.Delete(userId, questionId);
                }
                else
                {
                    QuestionHidden m = new QuestionHidden
                    {
                        QuestionId = questionId,
                        UserId = userId,
                        IsDelete=IsDelete
                    };
                    rtnValue = qHiddenDal.Add(m);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("HiddenQuestion:" + hiddenOrShow.ToString(), ex);
            }
            return rtnValue;
        }

        #endregion

        #region 查
        /// <summary>
        /// 获取单个question的信息
        /// </summary>
        /// <param name="questionId">questionID</param>
        /// <returns></returns>
        public Question GetQuestion(int questionId)
        {
            Question rtnValue = null;
            try
            {
                rtnValue = questionDal.GetQuestion(questionId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetQuestion", ex);
            }
            return rtnValue;
        }


        /// <summary>
        /// 分页获取题目屏蔽信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<QuestionHidden> GetQuestionHiddenList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<QuestionHidden> result = new List<QuestionHidden>();
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    result = qHiddenDal.GetList(filter);
                }
                else
                {
                    result = DBHelper.GetPageList<QuestionHidden>(pageIndex, pageSize, qHiddenDal.GetQuestionHiddenPageParams(filter), out totalCount);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetQuestionHiddenList方法出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取题目信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Question> GetSomeQuestionList(CustomFilter filter)
        {

            List<Question> result = new List<Question>();
            try
            {

                result = questionDal.GetList(filter);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetSomeQuestionList方法出错", ex);
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
        public List<Question> GetQuestionList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<Question> result = new List<Question>();
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
                result = DBHelper.GetPageList<Question>(pageIndex, pageSize, questionDal.GetQuestionPageParams(filter), out totalCount);
                if (result != null && result.Count > 0)
                {
                    var IdList = result.Select(l => l.Id).ToList();
                    Question tmpMod = new Question();
                    questionDal.GetQuestionExInfo(IdList, tmpMod);

                    Parallel.ForEach(result, m =>
                    {
                        m.OptionList = tmpMod.OptionList.Where(l => l.QuestionId == m.Id).ToList();
                        m.AnswerList = tmpMod.AnswerList.Where(l => l.QuestionId == m.Id).OrderBy(l => l.Answer).ToList();
                        m.AttachmentList = tmpMod.AttachmentList.Where(l => l.QuestionId == m.Id).ToList();
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetQuestionList方法出错", ex);
            }
            return result;
        }

        #endregion

        #region 用户答题部分

        /// <summary>
        /// 统计答题结果数量
        /// </summary>
        public Dictionary<int, int> CountByStatus(List<int> questionArray, int paperId, int? classId, int status)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            try
            {
                PaperUserAnswerResultDAL dal = new PaperUserAnswerResultDAL();
                result = dal.Count(questionArray, paperId, classId, status).GroupBy(l => l.QuesionId).ToDictionary(m => m.Key, n => n.FirstOrDefault().Result);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetSomeQuestionList方法出错", ex);
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 修改习题查看状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ChangeQuestionViewStatus(int id)
        {
            bool rtnValue = false;
            try
            {
                rtnValue = questionDal.ChangeQuestionViewStatus(id);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("ChangeQuestionViewStatus", ex);
            }
            return rtnValue;
        }
    }
}
