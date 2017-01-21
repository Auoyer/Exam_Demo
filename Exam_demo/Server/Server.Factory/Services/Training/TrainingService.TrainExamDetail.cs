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
        /// 修改销售机会
        /// </summary>
        /// <param name="model">销售机会实体</param>
        /// <returns></returns>
        public bool UpdateTrainExamDetail2(List<ExamPointAnswerVM> AnserList,int TrainExamId)
        {
            return MyService.UpdateTrainExamDetail2(AnserList.Map<List<ExamPointAnswer>, List<ExamPointAnswerVM>>(), TrainExamId);
        }
    }
}
