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
    /// 数据访问类:StuCustomerDetail
    /// </summary>
    public partial class StuCustomerDetailDAL
    {
        public StuCustomerDetailDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StuCustomerDetail where Id=@Id ");

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
        public int Add(StuCustomerDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StuCustomerDetail(");
            strSql.Append("CustomerId,DependentName,Age,Relation,InCome)");

            strSql.Append(" values (");
            strSql.Append("@CustomerId,@DependentName,@Age,@Relation,@InCome)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@CustomerId", model.CustomerId, dbType: DbType.Int32);
                param.Add("@DependentName", model.DependentName, dbType: DbType.String);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@Relation", model.Relation, dbType: DbType.String);
                param.Add("@InCome", model.InCome, dbType: DbType.Decimal);
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
        public bool Update(StuCustomerDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StuCustomerDetail set ");
            strSql.Append("CustomerId=@CustomerId,");
            strSql.Append("DependentName=@DependentName,");
            strSql.Append("Age=@Age,");
            strSql.Append("Relation=@Relation,");
            strSql.Append("InCome=@InCome");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@CustomerId", model.CustomerId, dbType: DbType.Int32);
                param.Add("@DependentName", model.DependentName, dbType: DbType.String);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@Relation", model.Relation, dbType: DbType.String);
                param.Add("@InCome", model.InCome, dbType: DbType.Decimal);
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
            strSql.Append("delete from StuCustomerDetail ");
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
        public StuCustomerDetail GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CustomerId,DependentName,Age,Relation,InCome from StuCustomerDetail ");
            strSql.Append(" where Id=@Id ");

            StuCustomerDetail model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<StuCustomerDetail>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StuCustomerDetail> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CustomerId,DependentName,Age,Relation,InCome ");
            strSql.Append(" FROM StuCustomerDetail ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<StuCustomerDetail> list = new List<StuCustomerDetail>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<StuCustomerDetail>(strSql.ToString()).ToList();
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
            if (filter.CustomerId.HasValue)
            {
                strSql.Append(" and CustomerId=" + filter.CustomerId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetStuCustomerDetailPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "StuCustomerDetail";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,CustomerId,DependentName,Age,Relation,InCome";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

