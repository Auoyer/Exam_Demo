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
        #region cww 新增方法

        /// <summary>
        /// 判断竞赛是否有考试内容，返回true=有题目
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType">竞赛类型；1=单项理论赛；2=单项实训赛；3=复合赛</param>
        /// <returns></returns>
        public bool IsMatchHaveExam(int matchId, int matchType)
        {
            return MyService.IsMatchHaveExam(matchId, matchType);
        }

        /// <summary>
        /// 查询竞赛用户成绩
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public List<PaperUserSummaryVM> GetUserScore(int matchId)
        {
            return MyService.GetUserScore(matchId).MapList<PaperUserSummaryVM, PaperUserSummary>();
        }
        #endregion
    }
}
