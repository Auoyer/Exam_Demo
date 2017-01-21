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
    /// 数据访问类:ExamPoint
    /// </summary>
    public partial class ExamPointDAL
    {
        public ExamPointDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ExamPoint where Id=@Id ");

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
        public int Add(ExamPoint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ExamPoint(");
            strSql.Append("ExamModuleId,ExamPointName,ExamPointType,TableName,FieldName,TypeName)");

            strSql.Append(" values (");
            strSql.Append("@ExamModuleId,@ExamPointName,@ExamPointType,@TableName,@FieldName,@TypeName)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamModuleId", model.ExamModuleId, dbType: DbType.Int32);
                param.Add("@ExamPointName", model.ExamPointName, dbType: DbType.String);
                param.Add("@ExamPointType", model.ExamPointType, dbType: DbType.Int32);
                param.Add("@TableName", model.TableName, dbType: DbType.String);
                param.Add("@FieldName", model.FieldName, dbType: DbType.String);
                param.Add("@TypeName", model.TypeName, dbType: DbType.String);
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
        public bool Update(ExamPoint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExamPoint set ");
            strSql.Append("ExamModuleId=@ExamModuleId,");
            strSql.Append("ExamPointName=@ExamPointName,");
            strSql.Append("ExamPointType=@ExamPointType,");
            strSql.Append("TableName=@TableName,");
            strSql.Append("FieldName=@FieldName,");
            strSql.Append("TypeName=@TypeName");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamModuleId", model.ExamModuleId, dbType: DbType.Int32);
                param.Add("@ExamPointName", model.ExamPointName, dbType: DbType.String);
                param.Add("@ExamPointType", model.ExamPointType, dbType: DbType.Int32);
                param.Add("@TableName", model.TableName, dbType: DbType.String);
                param.Add("@FieldName", model.FieldName, dbType: DbType.String);
                param.Add("@TypeName", model.TypeName, dbType: DbType.String);
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
            strSql.Append("delete from ExamPoint ");
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
        public ExamPoint GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ExamModuleId,ExamPointName,ExamPointType,TableName,FieldName,TypeName from ExamPoint ");
            strSql.Append(" where Id=@Id ");

            ExamPoint model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ExamPoint>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExamPoint> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ExamModuleId,ExamPointName,ExamPointType,TableName,FieldName,TypeName ");
            strSql.Append(" FROM ExamPoint ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ExamPoint> list = new List<ExamPoint>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ExamPoint>(strSql.ToString()).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据CustomFilter获取where语句
        /// </summary>
        private string GetStrWhere(CustomFilter filter)
        {
            if (filter == null)
            {
                return "";
            }

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
        public PageModel GetExamPointPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ExamPoint";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ExamModuleId,ExamPointName,ExamPointType,TableName,FieldName,TypeName";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

