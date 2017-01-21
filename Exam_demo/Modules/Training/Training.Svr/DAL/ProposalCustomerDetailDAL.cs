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
    /// 数据访问类:ProposalCustomerDetail
    /// </summary>
    public partial class ProposalCustomerDetailDAL
    {
        public ProposalCustomerDetailDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProposalCustomerDetail where Id=@Id ");

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
        public int Add(ProposalCustomerDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProposalCustomerDetail(");
            strSql.Append("ProposalId,Type,DependentName,Age,Relation,InCome)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Type,@DependentName,@Age,@Relation,@InCome)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Type", model.Type, dbType: DbType.Int32);
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
        public bool Update(ProposalCustomerDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProposalCustomerDetail set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Type=@Type,");
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
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Type", model.Type, dbType: DbType.Int32);
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
            strSql.Append("delete from ProposalCustomerDetail ");
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
        public ProposalCustomerDetail GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Type,DependentName,Age,Relation,InCome from ProposalCustomerDetail ");
            strSql.Append(" where Id=@Id ");

            ProposalCustomerDetail model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ProposalCustomerDetail>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ProposalCustomerDetail> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Type,DependentName,Age,Relation,InCome ");
            strSql.Append(" FROM ProposalCustomerDetail ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ProposalCustomerDetail> list = new List<ProposalCustomerDetail>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ProposalCustomerDetail>(strSql.ToString()).ToList();
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
            if (filter.ProposalId.HasValue)
            {
                strSql.Append(" and ProposalId=" + filter.ProposalId);
            }
            return strSql.ToString();
        }

        #endregion

        #region  获取分页参数
        /// <summary>
        /// 获取分页参数
        /// </summary>
        public PageModel GetProposalCustomerDetailPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ProposalCustomerDetail";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Type,DependentName,Age,Relation,InCome";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

