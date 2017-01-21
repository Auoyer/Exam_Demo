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
    /// 数据访问类:ExamModule
    /// </summary>
    public partial class ExamModuleDAL
    {
        public ExamModuleDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ExamModule where Id=@Id ");

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
        public int Add(ExamModule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ExamModule(");
            strSql.Append("ExamContentId,ExamModuleName,Sort)");

            strSql.Append(" values (");
            strSql.Append("@ExamContentId,@ExamModuleName,@Sort)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ExamContentId", model.ExamContentId, dbType: DbType.Int32);
                param.Add("@ExamModuleName", model.ExamModuleName, dbType: DbType.String);
                param.Add("@Sort", model.Sort, dbType: DbType.Int32);
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
        public bool Update(ExamModule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExamModule set ");
            strSql.Append("ExamContentId=@ExamContentId,");
            strSql.Append("ExamModuleName=@ExamModuleName,");
            strSql.Append("Sort=@Sort");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ExamContentId", model.ExamContentId, dbType: DbType.Int32);
                param.Add("@ExamModuleName", model.ExamModuleName, dbType: DbType.String);
                param.Add("@Sort", model.Sort, dbType: DbType.Int32);
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
            strSql.Append("delete from ExamModule ");
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
        public ExamModule GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ExamContentId,ExamModuleName,Sort from ExamModule ");
            strSql.Append(" where Id=@Id ");

            ExamModule model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ExamModule>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ExamModule> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ExamContentId,ExamModuleName,Sort ");
            strSql.Append(" FROM ExamModule ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ExamModule> list = new List<ExamModule>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ExamModule>(strSql.ToString()).ToList();
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
        public PageModel GetExamModulePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ExamModule";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ExamContentId,ExamModuleName,Sort";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

