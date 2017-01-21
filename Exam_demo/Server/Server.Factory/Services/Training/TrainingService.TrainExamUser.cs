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
        /// 新增销售机会-用户关系
        /// </summary>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool AddTrainExamUser(int TrainExamId, int UserId)
        {
            return MyService.AddTrainExamUser(TrainExamId, UserId);
        }
    }
}
