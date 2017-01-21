using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Exam.API;

namespace Exam.Svr
{
    /// <summary>
    /// 数据访问类:QuestionHidden
    /// </summary>
    public partial class QuestionHiddenDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from QuestionHidden where Id=@Id ");

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
        public int Add(QuestionHidden model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QuestionHidden(");
            strSql.Append("QuestionId,UserId,IsDelete)");

            strSql.Append(" values (");
            strSql.Append("@QuestionId,@UserId,@IsDelete)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@QuestionId", model.QuestionId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@IsDelete", model.IsDelete, dbType: DbType.Boolean);
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
        public bool Update(QuestionHidden model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update QuestionHidden set ");
            strSql.Append("QuestionId=@QuestionId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("IsDelete=@IsDelete");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@QuestionId", model.QuestionId, dbType: DbType.Int32);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@IsDelete", model.IsDelete, dbType: DbType.Boolean);
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
            strSql.Append("delete from QuestionHidden ");
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
        public QuestionHidden GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,QuestionId,UserId,IsDelete from QuestionHidden ");
            strSql.Append(" where Id=@Id ");

            QuestionHidden model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<QuestionHidden>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<QuestionHidden> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,QuestionId,UserId,IsDelete ");
            strSql.Append(" FROM QuestionHidden ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<QuestionHidden> list = new List<QuestionHidden>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<QuestionHidden>(strSql.ToString()).ToList();
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
        public PageModel GetQuestionHiddenPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "QuestionHidden";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,QuestionId,UserId,IsDelete";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion


        /*=======================自定义分界线=====================*/

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int userId, int questionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from QuestionHidden ");
            strSql.Append(" where QuestionId =@QuestionId and UserId=@UserId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@QuestionId", questionId, dbType: DbType.Int32);
                param.Add("@UserId", userId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }
    }
}

