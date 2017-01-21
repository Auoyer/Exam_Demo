using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    { 

        /// <summary>
        /// 获取成绩明细
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<AssessmentResultsDetail> GetAssessmentResultsDetailList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);


        /// <summary>
        /// 获取销售机会/实训考核正确数
        /// </summary>
        /// <param name="pointArray">考核点Id集合</param>
        /// <param name="trainId">考核Id</param>
        /// <param name="classId">班级Id，为空时查全部</param>
        /// <param name="status">结果：正确or错误</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<int, int> CountRight(List<int> pointArray, int trainId, int? classId, int status);

        /// <summary>
        /// 获取考核结果
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<AssessmentResultsDetail> GetExamResultScore(CustomFilter filter);

        /// <summary>
        /// 添加考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        int AddAssessmentResultsDetailInfo(AssessmentResultsDetail model);

        /// <summary>
        /// 更新考核结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateAssessmentResultsDetailInfo(AssessmentResultsDetail model);
    }
}
