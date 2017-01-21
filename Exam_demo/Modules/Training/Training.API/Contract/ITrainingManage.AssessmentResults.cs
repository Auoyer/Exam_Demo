using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 获取实训考核
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAssessmentResults(AssessmentResults model);

        /// <summary>
        /// 获取成绩列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<AssessmentResults> GetScoreResultsList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 根据用户ID获取成绩以及详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        AssessmentResults GetModelByUserId(int userId, int TrainExamId);


        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        AssessmentResults GetAssessmentResultsModel(CustomFilter filter);

        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAssessmentResults(AssessmentResults model);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        [OperationContract]
        bool IsExistsNoScore(int TrainExamId, int TrainExamStatus);

        /// <summary>
        /// 获取对应的所有考核点
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        [OperationContract]
        List<AssessmentExamPoint> GetAssessmentExamPointByUserId(CustomFilter filter, int? pageIndex, int? pageSize, out int total);

        /// <summary>
        /// 根据考核Id获取未评分/已评分数量
        /// list[0]：未评分数量
        /// list[1]：已评分数量
        /// </summary>
        /// <param name="TrainExamId">考核Id</param>
        /// <returns></returns>
        [OperationContract]
        List<int> CountTrainExamStatus(int TrainExamId);


        /// <summary>
        /// 根据用户和大赛ID获取成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        AssessmentResults GetARModel(int userId, int competitionId);
    }
}
