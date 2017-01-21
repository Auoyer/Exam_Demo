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
        /// 新增销售机会与考核点关联表功能
        /// </summary>
        /// <param name="model">销售机会与考核点关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        int AddTrainExamDetail(TrainExamDetail model);

        /// <summary>
        /// 修改销售机会与考核点关联表功能
        /// </summary>
        /// <param name="model">销售机会与考核点关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTrainExamDetail(TrainExamDetail model);


        /// <summary>
        /// 修改销售机会答案
        /// </summary>
        /// <param name="trainExam">销售机会表</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTrainExamDetail2(List<ExamPointAnswer> AnserList, int TrainExamId);

        /// <summary>
        /// 删除销售机会与考核点关联表功能
        /// </summary>
        /// <param name="Id">销售机会与考核点关联表ID</param>
        /// <returns></returns>
        [OperationContract]
        bool DelTrainExamDetail(int Id);

        /// <summary>
        /// 获取GetTrainExamDetail列表
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        List<TrainExamDetail> GetTrainExamDetail(CustomFilter filter);

        /// <summary>
        /// 获取GetTrainExamDetail对象
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        TrainExamDetail GetTrainExamDetailObj(int TrainExamId);

        /// <summary>
        /// ExamPointIds考核点集合获取
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        List<int> GetExamPointIds(CustomFilter filter);
    }
}
