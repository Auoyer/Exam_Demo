using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:Common
    /// </summary>
    public partial class EntryAssessmentDAL
    {
        public EntryAssessmentDAL()
        {
        }
         

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EntryAssessment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into EntryAssessment(");
            strSql.Append("TrainExamId,UserId,CompetitionId,EntryTime)");

            strSql.Append(" values (");
            strSql.Append("@TrainExamId,@UserId,@CompetitionId,@EntryTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@EntryTime", model.EntryTime, dbType: DbType.Date);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

         
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EntryAssessment> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [EntryAssessment] ");
            strSql.Append(" where 1=1 ");
            strSql.Append(" and  EndTime is null");
            strSql.Append(GetStrWhere(filter));

            List<EntryAssessment> list = new List<EntryAssessment>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<EntryAssessment>(strSql.ToString()).ToList();
            }
            return list;
        }
        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId.Value);
            }
            if (filter.TrainExamId.HasValue)
            {
                strSql.Append(" and TrainExamId=" + filter.TrainExamId.Value);
            } 
            return strSql.ToString();
        }
         

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EntryAssessment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [EntryAssessment] set ");
            strSql.Append("TrainExamId=@TrainExamId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CompetitionId=@CompetitionId,");
            strSql.Append("EntryTime=@EntryTime,");
            strSql.Append("EndTime=@EndTime");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CompetitionId", model.CompetitionId, dbType: DbType.Int32);
                param.Add("@TrainExamId", model.TrainExamId, dbType: DbType.Int32);
                param.Add("@EntryTime", model.EntryTime, dbType: DbType.DateTime);
                param.Add("@EndTime", model.EndTime, dbType: DbType.DateTime);  
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }
    }
}

