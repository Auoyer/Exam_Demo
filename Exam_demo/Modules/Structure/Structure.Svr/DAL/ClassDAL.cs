using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Structure.API;
using Utils;

namespace Structure.Svr
{
    /// <summary>
    /// 数据访问类:Class
    /// </summary>
    public partial class ClassDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Class where Id=@Id ");

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
        public int Add(Class model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Class(");
            strSql.Append("ClassName,CollegeId,Remark,CreateTime,ModifyTime,Status)");

            strSql.Append(" values (");
            strSql.Append("@ClassName,@CollegeId,@Remark,@CreateTime,@ModifyTime,@Status)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ClassName", model.ClassName, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@Remark", model.Remark, dbType: DbType.String);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        #endregion

        #region  更新

        public bool Update(Class model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class set ");
            strSql.Append("ClassName=@ClassName,");
            strSql.Append("CollegeId=@CollegeId,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifyTime=@ModifyTime,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ClassName", model.ClassName, dbType: DbType.String);
                param.Add("@CollegeId", model.CollegeId, dbType: DbType.Int32);
                param.Add("@Remark", model.Remark, dbType: DbType.String);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
                param.Add("@Status", model.Status, dbType: DbType.Int32);
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
            strSql.Append("delete from Class ");
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

        public bool DeleteBulk(List<int> ids)
        {
            //有问题，删除时需要判断是否组卷

            int result = 0;
            StringBuilder strSql = new StringBuilder();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    strSql.Append("delete from Class where id in @ids ");

                    result = conn.Execute(strSql.ToString(), new { ids = ids.ToArray() }, tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("ClassDAL DeleteBulk", ex);
                    tran.Rollback();

                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
            return result > 0;
        }
        #endregion

        #region  获取实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Class GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ClassName,CollegeId,SchoolId,Remark,CreateTime,ModifyTime,Status from Class ");
            strSql.Append(" where Id=@Id ");

            Class model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Class>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Class> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ClassName,CollegeId,Remark,CreateTime,ModifyTime,Status ");
            strSql.Append(" FROM Class ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Class> list = new List<Class>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Class>(strSql.ToString()).ToList();
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
        public PageModel GetClassPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Class";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ClassName,CollegeId,Remark,CreateTime,ModifyTime,Status";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /*========================================================================*/
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddUserClass(UserClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserClass(");
            strSql.Append("ClassName,CollegeId,Remark,CreateTime,ModifyTime,Status)");

            strSql.Append(" values (");
            strSql.Append("@UserId,@ClassId,@RoleId)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ClassId", model.ClassId, dbType: DbType.Int32);
                param.Add("@RoleId", model.RoleId, dbType: DbType.String);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 删除用户和班级关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteUserClass(UserClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserClass ");
            strSql.Append(" where UserId=@UserId ");
            strSql.Append(" and ClassId=@ClassId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@ClassId", model.ClassId, dbType: DbType.Int32);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        public bool UpdateBulk(List<int> classIdList, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Class set ModifyTime=@ModifyTime,Status=@Status where Id in @ids");

            int result = 0;
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                result = conn.Execute(string.Format(strSql.ToString(), status), new { ids = classIdList.ToArray(), ModifyTime = DateTime.Now });
            }
            return result > 0;
        }

    }
}

