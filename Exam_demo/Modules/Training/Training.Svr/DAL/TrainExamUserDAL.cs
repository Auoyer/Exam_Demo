using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;
using Utils;

namespace Training.Svr
{
    public class TrainExamUserDAL
    {
        /// <summary>
        /// 新增销售机会-用户关系
        /// </summary>
        /// <param name="TrainExamId">销售机会Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool Add(int TrainExamId, int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TrainExamUser(");
            strSql.Append("TrainExamId,UserId)");
            strSql.Append(" values (");
            strSql.Append("@TrainExamId,@UserId)");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", TrainExamId, dbType: DbType.Int32);
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }
    }
}
