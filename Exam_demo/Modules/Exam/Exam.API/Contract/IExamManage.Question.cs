using System.Collections.Generic;
using System.ServiceModel;

namespace Exam.API
{
    public partial interface IExamManage
    {
        #region 增/删
        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddQuestion(Question model);
        /// <summary>
        /// 批量新增题目
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        [OperationContract]
        List<Question> BatchAddQuestion(List<Question> questions);
        /// <summary>
        /// 批量删除题目
        /// </summary>
        /// <param name="ids">伪删除试题集合</param>
        /// <returns>未组卷的试题集合</returns>
        [OperationContract]
        List<int> RemoveQuestion(List<int> ids);

        /// <summary>
        /// 删除题目(单个)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteQuestion(int id);

        #endregion

        #region 改

        /// <summary>
        /// 编辑问题状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        bool EditQuestionStatus(int questionId, int status);


        /// <summary>
        /// 编辑问题状态
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        bool EditQuestionStatus2(List<int> Id);

        /// <summary>
        /// 屏蔽题目
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="questionId"></param>
        /// <param name="hiddenOrShow"></param>
        /// <returns></returns>
        [OperationContract]
        int HiddenQuestion(int userId, int questionId, bool hiddenOrShow, bool IsDelete);

        #endregion

        #region 查

        /// <summary>
        /// 获取单个question的信息
        /// </summary>
        /// <param name="questionId">questionID</param>
        /// <returns></returns>
        [OperationContract]
        Question GetQuestion(int questionId);

        /// <summary>
        /// 分页查询题目
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<Question> GetQuestionList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 分页获取题目屏蔽信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<QuestionHidden> GetQuestionHiddenList(CustomFilter filter, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 获取题目信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<Question> GetSomeQuestionList(CustomFilter filter);
        #endregion

        #region 用户答题部分

        /// <summary>
        /// 统计答题结果数量
        /// </summary>
        /// <param name="questionArray">待统计题目Id列表</param>
        /// <param name="paperId">试卷ID</param>
        /// <param name="classId">班级ID</param>
        /// <param name="status">答题结果：正确or错误</param>
        /// <returns>题目ID和数目对应</returns>
        [OperationContract]
        Dictionary<int, int> CountByStatus(List<int> questionArray, int paperId, int? classId, int status);

        #endregion

        /// <summary>
        /// 修改习题查看状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool ChangeQuestionViewStatus(int id);
    }
}
