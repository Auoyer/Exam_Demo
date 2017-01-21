using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    public partial interface IMatchManage
    {
        /// <summary>
        /// 超管端-获取历史大赛数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetCompetitionNum();

        /// <summary>
        /// 超管端-获取最近大赛列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        List<Competition> GetLatestCompetitionList(int num, int collegeId);

        /// <summary>
        /// 判断用户是否有正在进行的比赛，true=没有正在进行的比赛
        /// </summary>

        /// <returns></returns>
        [OperationContract]
        bool IsComAdminConductMatch(List<int> comAdminId);

        /// <summary>
        /// 获取大赛状态为已结束的比赛列表
        /// </summary>
        /// <author>zuheng</author>
        /// 
        [OperationContract]
        List<Competition> GetHasEndCompetionList(int collegeId);

        /// <summary>
        /// 非超管端-获取历史大赛数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetCompetitionNum2(int collegeId);

        /// <summary>
        /// 超管端-获取最近大赛列表2(非超管)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [OperationContract]
        List<Competition> GetLatestCompetitionList2(int num, int collegeId);

          /// <summary>
        /// 根据大赛的ID获取各分数段的人数
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        List<PersonScore> GetPersonScores(int competitionId);
    }
}
