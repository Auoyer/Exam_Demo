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
    /// 数据访问类:CashFlow
    /// </summary>
    public partial class CashFlowDAL
    {
        public CashFlowDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CashFlow where Id=@Id ");

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
        public int Add(CashFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CashFlow(");
            strSql.Append("ProposalId,Redemption,Investment,BorrowCapital,RepaymentCapital,WorkIncome,LiveExpense,InvestIncome,InterestExpense,InsuranceExpense)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Redemption,@Investment,@BorrowCapital,@RepaymentCapital,@WorkIncome,@LiveExpense,@InvestIncome,@InterestExpense,@InsuranceExpense)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Redemption", model.Redemption, dbType: DbType.Decimal);
                param.Add("@Investment", model.Investment, dbType: DbType.Decimal);
                param.Add("@BorrowCapital", model.BorrowCapital, dbType: DbType.Decimal);
                param.Add("@RepaymentCapital", model.RepaymentCapital, dbType: DbType.Decimal);
                param.Add("@WorkIncome", model.WorkIncome, dbType: DbType.Decimal);
                param.Add("@LiveExpense", model.LiveExpense, dbType: DbType.Decimal);
                param.Add("@InvestIncome", model.InvestIncome, dbType: DbType.Decimal);
                param.Add("@InterestExpense", model.InterestExpense, dbType: DbType.Decimal);
                param.Add("@InsuranceExpense", model.InsuranceExpense, dbType: DbType.Decimal);
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
        public bool Update(CashFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CashFlow set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Redemption=@Redemption,");
            strSql.Append("Investment=@Investment,");
            strSql.Append("BorrowCapital=@BorrowCapital,");
            strSql.Append("RepaymentCapital=@RepaymentCapital,");
            strSql.Append("WorkIncome=@WorkIncome,");
            strSql.Append("LiveExpense=@LiveExpense,");
            strSql.Append("InvestIncome=@InvestIncome,");
            strSql.Append("InterestExpense=@InterestExpense,");
            strSql.Append("InsuranceExpense=@InsuranceExpense");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Redemption", model.Redemption, dbType: DbType.Decimal);
                param.Add("@Investment", model.Investment, dbType: DbType.Decimal);
                param.Add("@BorrowCapital", model.BorrowCapital, dbType: DbType.Decimal);
                param.Add("@RepaymentCapital", model.RepaymentCapital, dbType: DbType.Decimal);
                param.Add("@WorkIncome", model.WorkIncome, dbType: DbType.Decimal);
                param.Add("@LiveExpense", model.LiveExpense, dbType: DbType.Decimal);
                param.Add("@InvestIncome", model.InvestIncome, dbType: DbType.Decimal);
                param.Add("@InterestExpense", model.InterestExpense, dbType: DbType.Decimal);
                param.Add("@InsuranceExpense", model.InsuranceExpense, dbType: DbType.Decimal);
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
            strSql.Append("delete from CashFlow ");
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
        public CashFlow GetModel(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Redemption,Investment,BorrowCapital,RepaymentCapital,WorkIncome,LiveExpense,InvestIncome,InterestExpense,InsuranceExpense from CashFlow ");
            strSql.Append(" where ProposalId=@ProposalId ");

            CashFlow model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<CashFlow>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<CashFlow> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Redemption,Investment,BorrowCapital,RepaymentCapital,WorkIncome,LiveExpense,InvestIncome,InterestExpense,InsuranceExpense ");
            strSql.Append(" FROM CashFlow ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<CashFlow> list = new List<CashFlow>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<CashFlow>(strSql.ToString()).ToList();
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
        public PageModel GetCashFlowPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "CashFlow";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Redemption,Investment,BorrowCapital,RepaymentCapital,WorkIncome,LiveExpense,InvestIncome,InterestExpense,InsuranceExpense";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

