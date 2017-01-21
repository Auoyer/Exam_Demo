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
       /// 获取实训平均分列表
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       [OperationContract]
       List<TrainClassScore> GetClassScoreList(int userId);

       /// <summary>
       /// 教师端首页统计销售机会/实训待审批数据
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="examTypeId"></param>
       /// <param name="trainExamStatus"></param>
       /// <returns></returns>
       [OperationContract]
       int GetTrainExamNum(int userId, int examTypeId, int trainExamStatus);

    }


   
}
