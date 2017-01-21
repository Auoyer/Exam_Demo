using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using VM;

namespace Server.Factory
{
    public partial class TrainingService : IService<ITrainingManage>
    {
        /// <summary>
        /// 获取案例
        /// </summary>
        /// <param name="Id">销售机会/实训考核Id</param>
        /// <returns>案例实体</returns>
        public ExamCaseVM GetExamCaseByTrainExamId(int TrainExamId)
        {
            var model = MyService.GetExamCaseByTrainExamId(TrainExamId);
            return model.Map<ExamCaseVM, ExamCase>();
        }
    }
}
