using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;
using Utils;

namespace Training.Svr
{
    public partial class TrainingManage : ITrainingManage
    {
        private TrainExamUserDAL trainExamUserDAL = new TrainExamUserDAL();

        /// <summary>
        /// 新增销售机会-用户关系
        /// </summary>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool AddTrainExamUser(int TrainExamId, int UserId)
        {
            bool result = false;
            try
            {
                result = trainExamUserDAL.Add(TrainExamId, UserId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("AddTrainExamUser方法出错", ex);
            }
            return result;
        }
    }
}
