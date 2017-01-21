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
    public partial class ClassScoreDAL
    {
        /// <summary>
        /// 获取实训平均分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TrainClassScore> GetClassScoreList(int userId)
        {
            List<TrainClassScore> result = new List<TrainClassScore>();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", userId, dbType: DbType.Int32);

                result = conn.Query<TrainClassScore>("Proc_ClassSorce", param, commandType: CommandType.StoredProcedure).ToList();
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
            StringBuilder strSql = new StringBuilder();
            var param =new DynamicParameters();
            strSql.Append("select COUNT(*) from TrainExam as A join AssessmentResults as B on A.Id=B.TrainExamId ");
            strSql.Append(" where A.UserId=@UserId");
            strSql.Append(" and A.ExamTypeId=@ExamTypeId");
            strSql.Append(" and B.TrainExamStatus=@TrainExamStatus");
            strSql.Append(" and A.EndDate<=GETDATE()");//可能有问题，数据库服务器的时间和客户的时间不一致
            param.Add("@UserId",userId,dbType:DbType.Int32);
            param.Add("@ExamTypeId",examTypeId,dbType:DbType.Int32);
            param.Add("@TrainExamStatus",trainExamStatus,dbType:DbType.Int32);
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Query<int>(strSql.ToString(),param).FirstOrDefault();
            }
            return result;
        }

    }
}
