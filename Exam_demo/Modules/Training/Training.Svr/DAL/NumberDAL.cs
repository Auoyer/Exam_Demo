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
    /// 数据访问类:Number
    /// </summary>
    public partial class NumberDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Number where Id=@Id ");

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
        public int Add(Number model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Number(");
            strSql.Append("NumberType,Prefix,CurrentDate,UsedMaxCode,Figure)");

            strSql.Append(" values (");
            strSql.Append("@NumberType,@Prefix,@CurrentDate,@UsedMaxCode,@Figure)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@NumberType", model.NumberType, dbType: DbType.Int32);
                param.Add("@Prefix", model.Prefix, dbType: DbType.String);
                param.Add("@CurrentDate", model.CurrentDate, dbType: DbType.DateTime);
                param.Add("@UsedMaxCode", model.UsedMaxCode, dbType: DbType.Int64);
                param.Add("@Figure", model.Figure, dbType: DbType.Int32);
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
        public bool Update(Number model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Number set ");
            strSql.Append("NumberType=@NumberType,");
            strSql.Append("Prefix=@Prefix,");
            strSql.Append("CurrentDate=@CurrentDate,");
            strSql.Append("UsedMaxCode=@UsedMaxCode,");
            strSql.Append("Figure=@Figure");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@NumberType", model.NumberType, dbType: DbType.Int32);
                param.Add("@Prefix", model.Prefix, dbType: DbType.String);
                param.Add("@CurrentDate", model.CurrentDate, dbType: DbType.DateTime);
                param.Add("@UsedMaxCode", model.UsedMaxCode, dbType: DbType.Int64);
                param.Add("@Figure", model.Figure, dbType: DbType.Int32);
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
            strSql.Append("delete from Number ");
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
        public Number GetModel(int NumberType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Id,NumberType,Prefix,CurrentDate,UsedMaxCode,Figure from Number ");
            strSql.Append(" where NumberType=@NumberType ");
            strSql.Append(" order by CurrentDate desc ");

            Number model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@NumberType", NumberType, dbType: DbType.Int32);
                model = conn.Query<Number>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Number> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,NumberType,Prefix,CurrentDate,UsedMaxCode ");
            strSql.Append(" FROM Number ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Number> list = new List<Number>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Number>(strSql.ToString()).ToList();
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
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetNumberPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Number";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,NumberType,Prefix,CurrentDate,UsedMaxCode";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

