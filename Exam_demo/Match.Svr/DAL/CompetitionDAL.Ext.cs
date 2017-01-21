using Dapper;
using Match.API;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Match.Svr
{
    public partial class CompetitionDAL
    {
        #region 超管端-获取历史大赛数量 bool GetCompetitionNum()
        /// <summary>
        /// 超管端-获取历史大赛数量
        /// </summary>
        /// <returns></returns>
        public int GetCompetitionNum()
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            strSql.Append("select COUNT(1) from Competition where IsRelease=2 and IsDelete=0");//比赛状态为已结束 

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region 获取最近大赛列表 List<Competition> GetLatestCompetitionList(int num)
        /// <summary>
        /// 获取最近大赛列表
        /// </summary>
        /// <param name="num">大赛数目</param>
        /// <returns></returns>
        public List<Competition> GetLatestCompetitionList(int num, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top {0} Id, PreliminaryStartTime, PreliminaryEndTime, RematchStartTime, RematchEndTime, PreliminaryResultType, PreliminaryResultTime, RematchResultType, RematchResultTime, ScoreStartTime, ScoreEndTime, CollegeId, Information, FinalistNumber, IsRelease, IsDelete, Name, AddUserId, AddTime, Type, State, RegistrationStartTime,AddUserName, RegistrationEndTime  ");
            strSql.Append("  from Competition where IsDelete=0 and RegistrationStartTime is not null");
            if (collegeId != 0)
                strSql.Append(" and collegeId=" + collegeId);
            strSql.Append(" order by RegistrationStartTime desc");

            string sql = string.Format(strSql.ToString(), num);
            List<Competition> list = new List<Competition>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Competition>(sql).ToList();
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 判断用户是否有正在进行的比赛，true=没有正在进行的比赛
        /// </summary>
        /// <param name="listComAdminId">List<竞赛管理员ID></param>
        /// <param name="userType">用户类型</param>
        /// <returns></returns>
        public bool IsComAdminConductMatch(List<int> comAdminId)
        {
            //string q = "in {" + string.Join(",", listComAdminId) + "}";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [GTA_FPBT_Match_V1.5].[dbo].[Competition] a inner join [GTA_FPBT_Structure_V1.5].[dbo].[UserInfo] b");
            strSql.Append(" on  a.AddUserId=b.Id and a.IsRelease<>2 ");
            strSql.Append("and b.Id in (" + string.Join(",", comAdminId) + ")");
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                int result = conn.Query<int>(strSql.ToString()).FirstOrDefault();

                if (result == 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取大赛状态为已结束的比赛列表
        /// </summary>
        /// <author>zuheng</author>
        public List<Competition> GetHasEndCompetionList(int collegeId)
        {
            List<Competition> list = new List<Competition>();
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select * from Competition where IsRelease=2 and IsDelete=0");//比赛状态为已结束 
            strSql.Append(" and CollegeId=@CollegeId");
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CollegeId", collegeId, dbType: DbType.Int32);
                list = conn.Query<Competition>(strSql.ToString(), param).ToList();
            }
            return list;
        }

        #region 非超管端-获取历史大赛数量2 bool GetCompetitionNum2()
        /// <summary>
        /// 非超管端-获取历史大赛数量2
        /// </summary>
        /// <returns></returns>
        public int GetCompetitionNum2(int collegeId)
        {
            int result = 0;
            StringBuilder strSql = new StringBuilder();
            var param = new DynamicParameters();
            //strSql.AppendFormat("select COUNT(1) from Competition where IsRelease=2 and IsDelete=0 and CollegeId in (0,{0})", collegeId);//比赛状态为已结束 
            strSql.AppendFormat("select COUNT(1) from Competition where IsRelease=2 and IsDelete=0 and CollegeId ={0}", collegeId);//比赛状态为已结束 

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region 获取最近大赛列表2 List<Competition> GetLatestCompetitionList2(int num)
        /// <summary>
        /// 获取最近大赛列表2(非超管)
        /// </summary>
        /// <param name="num">大赛数目</param>
        /// <returns></returns>
        public List<Competition> GetLatestCompetitionList2(int num, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top {0} Id, PreliminaryStartTime, PreliminaryEndTime, RematchStartTime, RematchEndTime, PreliminaryResultType, PreliminaryResultTime, RematchResultType, RematchResultTime, ScoreStartTime, ScoreEndTime, CollegeId, Information, FinalistNumber, IsRelease, IsDelete, Name, AddUserId, AddTime, Type, State, RegistrationStartTime,AddUserName, RegistrationEndTime  ");
            strSql.Append("  from Competition where IsDelete=0 and CollegeId in (0,{1})");
            strSql.Append(" order by PreliminaryStartTime");

            string sql = string.Format(strSql.ToString(), num, collegeId);
            List<Competition> list = new List<Competition>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Competition>(sql).ToList();
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 根据大赛的ID获取各分数段的人数
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        public List<PersonScore> GetPersonScores(int competitionId)
        {

            Statisctic1 st = GetStatisctic1(competitionId);
            var totalScore = 0M;
            List<PersonScore> list = new List<PersonScore>();
            StringBuilder strSql = new StringBuilder();
            string sql = "";

            if (st != null)
            {
                //取初赛成绩
                if (st.Type == 2)
                {
                    totalScore = st.AllScore;
                    var scoreSegment1 = 0 + "-" + String.Format("{0:F}", (1 * totalScore) / 10);
                    var scoreSegment2 = String.Format("{0:F}", (1 * totalScore) / 10) + "-" + String.Format("{0:F}", (2 * totalScore) / 10);
                    var scoreSegment3 = Math.Round((2 * totalScore) / 10, 2) + "-" + Math.Round((3 * totalScore) / 10, 2);
                    var scoreSegment4 = Math.Round((3 * totalScore) / 10, 2) + "-" + Math.Round((4 * totalScore) / 10, 2);
                    var scoreSegment5 = Math.Round((4 * totalScore) / 10, 2) + "-" + Math.Round((5 * totalScore) / 10, 2);
                    var scoreSegment6 = Math.Round((5 * totalScore) / 10, 2) + "-" + Math.Round((6 * totalScore) / 10, 2);
                    var scoreSegment7 = Math.Round((6 * totalScore) / 10, 2) + "-" + Math.Round((7 * totalScore) / 10, 2);
                    var scoreSegment8 = Math.Round((7 * totalScore) / 10, 2) + "-" + Math.Round((8 * totalScore) / 10, 2);
                    var scoreSegment9 = Math.Round((8 * totalScore) / 10, 2) + "-" + Math.Round((9 * totalScore) / 10, 2);
                    var scoreSegment10 = Math.Round((9 * totalScore) / 10, 2) + "-" + totalScore;

                    strSql.Append(@"select case when (SubjectiveResults+ObjectiveResults) >= 0 and (SubjectiveResults+ObjectiveResults) < (1*{0})/10 then '{1}'
                                when (SubjectiveResults+ObjectiveResults) >= (1*{0})/10 and (SubjectiveResults+ObjectiveResults) < (2*{0})/10 then '{2}'
                                when (SubjectiveResults+ObjectiveResults) >= (2*{0})/10 and (SubjectiveResults+ObjectiveResults) < (3*{0})/10 then '{3}'
                                when (SubjectiveResults+ObjectiveResults) >= (3*{0})/10 and (SubjectiveResults+ObjectiveResults) < (4*{0})/10 then '{4}'
                                when (SubjectiveResults+ObjectiveResults) >= (4*{0})/10 and (SubjectiveResults+ObjectiveResults) < (5*{0})/10 then '{5}'
                                when (SubjectiveResults+ObjectiveResults) >= (5*{0})/10 and (SubjectiveResults+ObjectiveResults) < (6*{0})/10 then '{6}'
                                when (SubjectiveResults+ObjectiveResults) >= (6*{0})/10 and (SubjectiveResults+ObjectiveResults) < (7*{0})/10 then '{7}'
                                when (SubjectiveResults+ObjectiveResults) >= (7*{0})/10 and (SubjectiveResults+ObjectiveResults) < (8*{0})/10 then '{8}'
			                    when (SubjectiveResults+ObjectiveResults) >= (8*{0})/10 and (SubjectiveResults+ObjectiveResults) < (9*{0})/10 then '{9}'
			                    when (SubjectiveResults+ObjectiveResults) >= (9*{0})/10 and (SubjectiveResults+ObjectiveResults) < {0} then '{10}'
                                else '{0}以上'  end as ScoreSegment, count(*) Persons,max((SubjectiveResults+ObjectiveResults)) as MaxScore
                    from [dbo].[V_MatchResult] where  CompetitionId=@CompetitionId and (SubjectiveResults+ObjectiveResults) is not null
                    group by case when (SubjectiveResults+ObjectiveResults) >= 0 and (SubjectiveResults+ObjectiveResults) < (1*{0})/10 then '{1}'
                                when (SubjectiveResults+ObjectiveResults) >= (1*{0})/10 and (SubjectiveResults+ObjectiveResults) < (2*{0})/10 then '{2}'
                                when (SubjectiveResults+ObjectiveResults) >= (2*{0})/10 and (SubjectiveResults+ObjectiveResults) < (3*{0})/10 then '{3}'
                                when (SubjectiveResults+ObjectiveResults) >= (3*{0})/10 and (SubjectiveResults+ObjectiveResults) < (4*{0})/10 then '{4}'
                                when (SubjectiveResults+ObjectiveResults) >= (4*{0})/10 and (SubjectiveResults+ObjectiveResults) < (5*{0})/10 then '{5}'
                                when (SubjectiveResults+ObjectiveResults) >= (5*{0})/10 and (SubjectiveResults+ObjectiveResults) < (6*{0})/10 then '{6}'
                                when (SubjectiveResults+ObjectiveResults) >= (6*{0})/10 and (SubjectiveResults+ObjectiveResults) < (7*{0})/10 then '{7}'
                                when (SubjectiveResults+ObjectiveResults) >= (7*{0})/10 and (SubjectiveResults+ObjectiveResults) < (8*{0})/10 then '{8}'
			                    when (SubjectiveResults+ObjectiveResults) >= (8*{0})/10 and (SubjectiveResults+ObjectiveResults) < (9*{0})/10 then '{9}'
			                    when (SubjectiveResults+ObjectiveResults) >= (9*{0})/10 and (SubjectiveResults+ObjectiveResults) < {0} then '{10}'
                                else '{0}以上' end
                    order by MaxScore");
                    sql = string.Format(strSql.ToString(), totalScore, scoreSegment1, scoreSegment2, scoreSegment3, scoreSegment4, scoreSegment5, scoreSegment6, scoreSegment7, scoreSegment8, scoreSegment9, scoreSegment10);
                }
                else if (st.Type == 1 || st.Type == 3)
                {
                    totalScore = st.TotalScore;

                    var scoreSegment1 = 0 + "-" + String.Format("{0:F}", (1 * totalScore) / 10);
                    var scoreSegment2 = String.Format("{0:F}", (1 * totalScore) / 10) + "-" + String.Format("{0:F}", (2 * totalScore) / 10);
                    var scoreSegment3 = Math.Round((2 * totalScore) / 10, 2) + "-" + Math.Round((3 * totalScore) / 10, 2);
                    var scoreSegment4 = Math.Round((3 * totalScore) / 10, 2) + "-" + Math.Round((4 * totalScore) / 10, 2);
                    var scoreSegment5 = Math.Round((4 * totalScore) / 10, 2) + "-" + Math.Round((5 * totalScore) / 10, 2);
                    var scoreSegment6 = Math.Round((5 * totalScore) / 10, 2) + "-" + Math.Round((6 * totalScore) / 10, 2);
                    var scoreSegment7 = Math.Round((6 * totalScore) / 10, 2) + "-" + Math.Round((7 * totalScore) / 10, 2);
                    var scoreSegment8 = Math.Round((7 * totalScore) / 10, 2) + "-" + Math.Round((8 * totalScore) / 10, 2);
                    var scoreSegment9 = Math.Round((8 * totalScore) / 10, 2) + "-" + Math.Round((9 * totalScore) / 10, 2);
                    var scoreSegment10 = Math.Round((9 * totalScore) / 10, 2) + "-" + totalScore;

                    strSql.Append(@"select case when Score >= 0 and Score < (1*{0})/10 then '{1}'
                                when Score >= (1*{0})/10 and Score < (2*{0})/10 then '{2}'
                                when Score >= (2*{0})/10 and Score < (3*{0})/10 then '{3}'
                                when Score >= (3*{0})/10 and Score < (4*{0})/10 then '{4}'
                                when Score >= (4*{0})/10 and Score < (5*{0})/10 then '{5}'
                                when Score >= (5*{0})/10 and Score < (6*{0})/10 then '{6}'
                                when Score >= (6*{0})/10 and Score < (7*{0})/10 then '{7}'
                                when Score >= (7*{0})/10 and Score < (8*{0})/10 then '{8}'
			                    when Score >= (8*{0})/10 and Score < (9*{0})/10 then '{9}'
			                    when Score >= (9*{0})/10 and Score < {0} then '{10}'
                                else '{0}以上'  end as ScoreSegment, count(*) Persons,max(Score) as MaxScore
                    from [dbo].[V_MatchResult] where  CompetitionId=@CompetitionId and Score is not null
                    group by case when Score >= 0 and Score < (1*{0})/10 then '{1}'
                                when Score >= (1*{0})/10 and Score < (2*{0})/10 then '{2}'
                                when Score >= (2*{0})/10 and Score < (3*{0})/10 then '{3}'
                                when Score >= (3*{0})/10 and Score < (4*{0})/10 then '{4}'
                                when Score >= (4*{0})/10 and Score < (5*{0})/10 then '{5}'
                                when Score >= (5*{0})/10 and Score < (6*{0})/10 then '{6}'
                                when Score >= (6*{0})/10 and Score < (7*{0})/10 then '{7}'
                                when Score >= (7*{0})/10 and Score < (8*{0})/10 then '{8}'
			                    when Score >= (8*{0})/10 and Score < (9*{0})/10 then '{9}'
			                    when Score >= (9*{0})/10 and Score < {0} then '{10}'
                                else '{0}以上' end
                    order by MaxScore");
                    sql = string.Format(strSql.ToString(), totalScore, scoreSegment1, scoreSegment2, scoreSegment3, scoreSegment4, scoreSegment5, scoreSegment6, scoreSegment7, scoreSegment8, scoreSegment9, scoreSegment10);
                }
            }
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                //param.Add("{0}", totalScore, dbType: DbType.Decimal);
                list = conn.Query<PersonScore>(sql, param).ToList();
            }

            return list;
        }

        /// <summary>
        /// 根据大赛的ID获取本次大赛的总分
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns></returns>
        public Statisctic1 GetStatisctic1(int competitionId)
        {
            StringBuilder strSql = new StringBuilder();
            Statisctic1 res = new Statisctic1();
            var param = new DynamicParameters();
            strSql.Append(@"select a.Id,a.Type,b.AllScore,c.TotalScore from competition a left join              [GTA_FPBT_Training_V1.5].[dbo].TrainExam b
                on a.Id = b.CompetitionId
                left join [GTA_FPBT_Training_V1.5].[dbo].[Paper] c
                on a.Id = c.CompetitionId where a.Id=@CompetitionId");

            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CompetitionId", competitionId, dbType: DbType.Int32);
                res = conn.Query<Statisctic1>(strSql.ToString(), param).FirstOrDefault();
            }
            return res;
        }
    }
}
