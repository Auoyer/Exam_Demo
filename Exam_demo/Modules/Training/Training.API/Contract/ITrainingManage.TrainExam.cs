using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 分页销售机会列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TrainExam> GetTrainExamList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 获取销售机会列表全部数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TrainExam> GetAllTrainExamList(CustomFilter filter);


        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetTrainExamListUnit(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);


        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetTrainExamAndProposal(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 学生端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetTrainExamAndAssessmentResults(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 教师端销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetTecTrainExamListUnit(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 教师端学生成绩列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<TrainExamAssessment> GetTranExamAssessmentByUserId(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 自主实训获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<TrainExamAssessment> GettTranExamAssessmentPageSelfTrain(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);


        /// <summary>
        /// 销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetList2(CustomFilter model);


        /// <summary>
        /// 销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetList3(CustomFilter model);

        /// <summary>
        /// 销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetList4(CustomFilter model);

        /// <summary>
        /// 销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetList5(CustomFilter model);

        /// <summary>
        /// 销售机会列表联表获取
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<UnitTrainExam> GetExamAndClass(CustomFilter model);

        /// <summary>
        /// 新增销售机会
        /// </summary>
        /// <param name="trainExam"></param>
        /// <returns></returns>
        [OperationContract]
        int AddTrainExam(TrainExam trainExam);

        /// <summary>
        /// 修改销售机会
        /// </summary>
        /// <param name="trainExam">销售机会表</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTrainExam(TrainExam trainExam);

        /// <summary>
        /// 删除销售机会
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteTrainExam(int id);

        /// <summary>
        /// 销售机会是否存在
        /// </summary>
        /// <param name="id">销售机会ID</param>
        /// <returns></returns>
        [OperationContract]
        bool Exists(int id);

        /// <summary>
        /// 获取销售机会实体
        /// </summary>
        /// <param name="id">销售机会ID</param>
        /// <returns></returns>
        [OperationContract]
        TrainExam GetTrainExam(int id);

        /// <summary>
        /// 编辑销售机会/实训考核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool EditTrainExam(TrainExam model);

        /// <summary>
        /// 获取考核内容
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        [OperationContract]
        TrainExam GetTrainExamInfo(int TrainExamId);

        /// <summary>
        /// 获取未评分考核ID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<int> GetNoScoreExamId();

        /// <summary>
        /// 班级集合中，是否有已发布的销售机会/实训考核
        /// </summary>
        /// <param name="classId">班级集合</param>
        /// <returns></returns>
        [OperationContract]
        bool IsPublish(List<int> classId);

        /// <summary>
        /// 根据条件统计销售机会/实训考核
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        [OperationContract]
        int CountTrainExam(CustomFilter filter);

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<TrainExam> GetTrainExamWithDetail(int CompetitionId);

        /// <summary>
        /// 获取大赛的案例列表
        /// </summary>
        /// <param name="CompetitionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<TrainExam> GetTrainExamByMatch(int CompetitionId);
    }
}
