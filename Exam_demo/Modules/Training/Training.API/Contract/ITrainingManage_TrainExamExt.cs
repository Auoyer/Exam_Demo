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
        #region cww 新增方法

        /// <summary>
        /// 判断竞赛是否有考试内容
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType">竞赛类型；1=单项理论赛；2=单项实训赛；3=复合赛</param>
        /// <returns></returns>
        [OperationContract]
        bool IsMatchHaveExam(int matchId, int matchType);


        /// <summary>
        /// 查询竞赛用户成绩
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        List<PaperUserSummary> GetUserScore(int matchId);
        #endregion
    }
}
