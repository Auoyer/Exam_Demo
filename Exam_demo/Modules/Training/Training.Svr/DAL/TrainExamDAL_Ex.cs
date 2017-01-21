using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.API;

namespace Training.Svr
{
    public partial class TrainExamDAL
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
            StringBuilder strSql = new StringBuilder();

            int result = 0;
            var param = new DynamicParameters();

            if (matchType == 1)
            {
                // 查询单项理论考试题目表 & 总分不能小于零
                strSql.Append("select count(1) from Paper where CompetitionId=@CompetitionId and TotalScore>0");
                using (var conn = DBHelper.CreateConnection())
                {
                    conn.Open();
                    param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                    result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
                }
                return result > 0;
            }
            else if (matchType == 2)
            {
                // 查询单项实训 & 总分不能小于零
                strSql.Append("select count(1) from TrainExam where CompetitionId=@CompetitionId and AllScore>0");
                using (var conn = DBHelper.CreateConnection())
                {
                    conn.Open();
                    param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                    result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
                }
                return result > 0;
            }
            else
            {
                // 复合赛，先查理论，在查实训
                strSql.Append("select count(1) from Paper where CompetitionId=@CompetitionId ");
                using (var conn = DBHelper.CreateConnection())
                {
                    conn.Open();
                    param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                    result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
                    if (result > 0)
                    {
                        strSql.Clear();
                        strSql.Append("select count(1) from TrainExam where CompetitionId=@CompetitionId ");
                        param.Add("@CompetitionId", matchId, dbType: DbType.Int32);
                        result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
                        return result > 0;
                    }
                    else
                        return false;
                }
            }



        }

        #endregion

    }
}
