using System.Collections.Generic;
using System.ServiceModel;

namespace Training.API
{
    public partial interface ITrainingManage
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="TrainExamId"></param>
        /// <returns></returns>
        [OperationContract]
        int AddEntryAssessment(EntryAssessment model);
         
          
        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<EntryAssessment> GetEntryAssessmentList(CustomFilter filter);

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateEntryAssessment(EntryAssessment model); 
    }
}
