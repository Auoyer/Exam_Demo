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
        ClassScoreDAL classScoreDAL = new ClassScoreDAL();
        /// <summary>
        /// 获取实训平均分列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TrainClassScore> GetClassScoreList(int userId)
        {
            List<TrainClassScore> result = new List<TrainClassScore>();
            try
            {
                result = classScoreDAL.GetClassScoreList(userId);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetClassScoreList方法出错", ex);
            }

            return result;
        }

        /// <summary>
        /// 教师端首页统计销售机会/实训待审批数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="examTypeId"></param>
        /// <param name="trainExamStatus"></param>
        /// <returns></returns>
        public int GetTrainExamNum(int userId, int examTypeId, int trainExamStatus)
        {
            int result = 0;
            try
            {
                result = classScoreDAL.GetTrainExamNum(userId, examTypeId, trainExamStatus);
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("GetTrainExamNum方法出错", ex);
 
            }
            return result;
        }
    }
}
