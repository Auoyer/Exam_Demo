using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Structure.API;

namespace Structure.Svr
{
    /// <summary>
    /// 数据访问类:Account
    /// </summary>
    public partial class AccountDAL
    {

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Account where Id=@Id ");

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
        public int Add(Account model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Account(");
            strSql.Append("AccountNo,password,UserId,CreateTime,ModifyTime)");

            strSql.Append(" values (");
            strSql.Append("@AccountNo,@password,@UserId,@CreateTime,@ModifyTime)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@AccountNo", model.AccountNo, dbType: DbType.String);
                param.Add("@password", model.password, dbType: DbType.String);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
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
        public bool Update(Account model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Account set ");
            strSql.Append("AccountNo=@AccountNo,");
            strSql.Append("password=@password,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("ModifyTime=@ModifyTime");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@AccountNo", model.AccountNo, dbType: DbType.String);
                param.Add("@password", model.password, dbType: DbType.String);
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@CreateTime", model.CreateTime, dbType: DbType.DateTime);
                param.Add("@ModifyTime", model.ModifyTime, dbType: DbType.DateTime);
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
            strSql.Append("delete from Account ");
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
        public Account GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,AccountNo,password,UserId,CreateTime,ModifyTime from Account ");
            strSql.Append(" where Id=@Id ");

            Account model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Account>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Account> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,AccountNo,password,UserId,CreateTime,ModifyTime ");
            strSql.Append(" FROM Account ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Account> list = new List<Account>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Account>(strSql.ToString()).ToList();
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
            if (filter.IdList != null && filter.IdList.Count > 0)
            {
                strSql.AppendFormat(" and UserId in ('{0}')", string.Join("','", filter.IdList));
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetAccountPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Account";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,AccountNo,password,UserId,CreateTime,ModifyTime";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

        /*===============================自定义分界线============================*/


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Account GetModel(string loginName, string password, int collegeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,AccountNo,Password,UserId,a.CreateTime,a.ModifyTime from Account a,UserInfo b ");
            strSql.Append(" where a.userId=b.id and AccountNo=@AccountNo and Password=@Password and CollegeId=@CollegeId and b.Status<>3");

            Account model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@AccountNo", loginName, dbType: DbType.String);
                param.Add("@Password", password, dbType: DbType.String);
                param.Add("@CollegeId", collegeId, dbType: DbType.String);
                model = conn.Query<Account>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 用户获取得到一个对象实体
        /// </summary>
        public Account GetModelByUserId(int UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,AccountNo,password,UserId,CreateTime,ModifyTime from Account ");
            strSql.Append(" where UserId=@UserId ");

            Account model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", UserId, dbType: DbType.Int32);
                model = conn.Query<Account>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="AccountNo">账号</param>
        public bool Exists(string AccountNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Account where AccountNo=@AccountNo ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@AccountNo", AccountNo, dbType: DbType.String);
                result = conn.Query<int>(strSql.ToString(), param).FirstOrDefault();
            }
            return result > 0;
        }


        /// <summary>
        /// 根据账号ID更新密码
        /// </summary>
        public bool UpdateAccountPwdById(Account model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Account set ");
            strSql.Append("password=@password,");
            strSql.Append("ModifyTime=@ModifyTime");
            strSql.Append(" where UserId=@UserId ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@UserId", model.UserId, dbType: DbType.Int32);
                param.Add("@password", model.password, dbType: DbType.String);
                param.Add("@ModifyTime", DateTime.Now, dbType: DbType.DateTime);
                result = conn.Execute(strSql.ToString(), param);
            }
            return result > 0;
        }

    }
}

