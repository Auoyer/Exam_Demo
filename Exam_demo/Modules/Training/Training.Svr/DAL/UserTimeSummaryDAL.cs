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
    /// <summary>
    /// 数据访问类:UserTimeSummary
    /// </summary>
    public partial class UserTimeSummaryDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserTimeSummary where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }

        #endregion

        #region  新增

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserTimeSummary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserTimeSummary(");
            strSql.Append("UserId,SummaryType,UsedTime)");

            strSql.Append(" values (");
            strSql.Append("@UserId,@SummaryType,@UsedTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@SummaryType", model.SummaryType, dbType: DbType.Int32);
                param.Add("@UsedTime", model.UsedTime, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserTimeSummary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserTimeSummary set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("SummaryType=@SummaryType,");
            strSql.Append("UsedTime=@UsedTime");
            strSql.Append(" where UserId=@UserId and  SummaryType=@SummaryType");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@SummaryType", model.SummaryType, dbType: DbType.Int32);
                param.Add("@UsedTime", model.UsedTime, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserTimeSummary ");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserTimeSummary GetModel(int UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,SummaryType,UsedTime from UserTimeSummary ");
            strSql.Append(" where UserId=@UserId ");

            UserTimeSummary model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                model = conn.Query<UserTimeSummary>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<UserTimeSummary> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,SummaryType,UsedTime ");
            strSql.Append(" FROM UserTimeSummary ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<UserTimeSummary> list = new List<UserTimeSummary>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<UserTimeSummary>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            if (filter.Id.HasValue)
            {
                strSql.Append(" and Id=" + filter.Id);
            }
            if (filter.SummaryType.HasValue)
            {
                strSql.Append(" and SummaryType=" + filter.SummaryType);
            }
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId=" + filter.UserId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetUserTimeSummaryPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "UserTimeSummary";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,UserId,SummaryType,UsedTime";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

