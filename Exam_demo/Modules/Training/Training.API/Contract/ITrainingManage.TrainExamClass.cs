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
        /// 新增销售机会与班级关联表功能
        /// </summary>
        /// <param name="model">销售机会与班级关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        int AddTrainExamClass(TrainExamClass model);

        /// <summary>
        /// 修改销售机会与班级关联表功能
        /// </summary>
        /// <param name="model">销售机会与班级关联表实体类</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateTrainExamClass(TrainExamClass model);

        /// <summary>
        /// 删除销售机会与班级关联表功能
        /// </summary>
        /// <param name="Id">销售机会与班级关联表实体类ID</param>
        /// <returns></returns>
        [OperationContract]
        bool DelTrainExamClass(int Id);

        /// <summary>
        /// 获取TrainExamClass列表
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        List<TrainExamClass> GetTrainExamClass(CustomFilter filter);

        /// <summary>
        /// 获取TrainExamClass对象
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        [OperationContract]
        TrainExamClass GetTrainExamClassObj(int TrainExamId);

        /// <summary>
        /// 获取ClassIds集合
        /// </summary>
        /// <param name="filter">查询条件过滤</param>
        /// <returns></returns>
        [OperationContract]
        List<int> GetClassIds(CustomFilter filter);
    }
}
