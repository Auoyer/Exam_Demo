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
        /// 获取案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns></returns>
        [OperationContract]
        ExamCase GetExamCase(int Id);

        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="TrainExamId">销售机会/实训考核Id</param>
        /// <returns></returns>
        [OperationContract]
        ExamCase GetExamCaseByTrainExamId(int TrainExamId);

        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>Id</returns>
        [OperationContract]
        int AddExamCase(ExamCase model);

        /// <summary>
        /// 更新案例
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool UpdateExamCase(ExamCase model);

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool DeleteExamCase(int Id);

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="Id">案例Id</param>
        /// <returns>是否成功</returns>
        [OperationContract]
        bool ForTrainExamIdDeleteExamCase(int TrainExamId);

        /// <summary>
        /// 获取案例全部数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ExamCase> GetAllExamCaseList(CustomFilter filter);

        /// <summary>
        /// 获取案例分页列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<ExamCase> GetExamCasePage(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);

        /// <summary>
        /// 获取案例分页列表--多表联查
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页(可为空,为空时返回所有数据)</param>
        /// <param name="pageSize">每页页数(可为空,为空时返回所有数据)</param>
        /// <param name="totalCount">总数量</param>
        /// <returns></returns>
        [OperationContract]
        List<ExamCaseTrainExam> GetSalesJudgeList(CustomFilter filter, int? pageIndex, int? pageSize, out int totalCount);
    }
}
