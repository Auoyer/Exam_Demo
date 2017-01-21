using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.API;

namespace Training.Svr
{
    public class PaperUserSummaryDAL
    {
        public PaperUserSummaryDAL()
        {
        }

        #region cww扩展

        /// <summary>
        /// 查询竞赛用户成绩
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public List<PaperUserSummary> GetUserScore(int matchId)
        {
            string str = "select sum(Score) as Score,GroupId from PaperUserSummary a,[GTA_FPBT_Match_V1.5].dbo.CompetitionUser b";
            str += " where a.CompetitionId=b.CompetitionId and a.UserId=b.UserId and b.CompetitionId=" + matchId;
            str += " group by b.GroupId order by sum(Score) desc";
            List<PaperUserSummary> list = new List<PaperUserSummary>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                list = conn.Query<PaperUserSummary>(str).ToList();
            }
            return list;
        }


        #endregion
    }
}
