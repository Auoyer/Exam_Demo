using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:Calendar
    /// </summary>
    public partial class CalendarDAL
    {
        public CalendarDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Calendar where Id=@Id ");

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
        public int Add(Calendar model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Calendar(");
            strSql.Append("StuCustomerId,CustomerName,ServiceType,Context,OrderDate,UserId)");

            strSql.Append(" values (");
            strSql.Append("@StuCustomerId,@CustomerName,@ServiceType,@Context,@OrderDate,@UserId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@StuCustomerId", model.StuCustomerId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@ServiceType", model.ServiceType, dbType: DbType.Int32);
                param.Add("@Context", model.Context, dbType: DbType.String);
                param.Add("@OrderDate", model.OrderDate, dbType: DbType.DateTime);
                param.Add("@UserId",model.UserId,dbType:DbType.Int32);
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
        public bool Update(Calendar model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Calendar set ");
            strSql.Append("StuCustomerId=@StuCustomerId,");
            strSql.Append("CustomerName=@CustomerName,");
            strSql.Append("ServiceType=@ServiceType,");
            strSql.Append("Context=@Context,");
            strSql.Append("OrderDate=@OrderDate");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@StuCustomerId", model.StuCustomerId, dbType: DbType.Int32);
                param.Add("@CustomerName", model.CustomerName, dbType: DbType.String);
                param.Add("@ServiceType", model.ServiceType, dbType: DbType.Int32);
                param.Add("@Context", model.Context, dbType: DbType.String);
                param.Add("@OrderDate", model.OrderDate, dbType: DbType.DateTime);
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
            strSql.Append("delete from Calendar ");
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete2(int StuCustomerId, int UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Calendar ");
            strSql.Append(" where StuCustomerId=@StuCustomerId  and UserId=@UserId");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@StuCustomerId", StuCustomerId, dbType: DbType.Int32);
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }
        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Calendar GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,StuCustomerId,CustomerName,ServiceType,Context,OrderDate from Calendar ");
            strSql.Append(" where Id=@Id ");

            Calendar model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Calendar>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Calendar> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,StuCustomerId,CustomerName,ServiceType,Context,OrderDate ");
            strSql.Append(" FROM Calendar ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Calendar> list = new List<Calendar>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Calendar>(strSql.ToString()).ToList();
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
            if (filter.UserId.HasValue)
            {
                strSql.Append(" and UserId="+filter.UserId);
            }
            if (!string.IsNullOrWhiteSpace(filter.ChoseTime.ToString()) && filter.ChoseType == 1)//当月
            {
                strSql.AppendFormat(" and DATEDIFF(MM,OrderDate,'{0}')=0",filter.ChoseTime);
 
            }
            if (!string.IsNullOrWhiteSpace(filter.ChoseTime.ToString()) && filter.ChoseType == 2)//当周
            {
                strSql.AppendFormat(" and DATEDIFF(WEEK,OrderDate,'{0}')=0", filter.ChoseTime);

            }
            if (!string.IsNullOrWhiteSpace(filter.ChoseTime.ToString()) && filter.ChoseType == 3)//当天
            {
                strSql.AppendFormat(" and DATEDIFF(DAY,OrderDate,'{0}')=0", filter.ChoseTime);

            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetCalendarPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Calendar";
            model.PKey = "Id";
            model.Sort = "OrderDate";
            model.Fields = "Id,StuCustomerId,CustomerName,ServiceType,Context,OrderDate";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

