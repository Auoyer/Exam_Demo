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
    /// 数据访问类:CashPlan
    /// </summary>
    public partial class CashPlanDAL
    {
        public CashPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CashPlan where Id=@Id ");

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
        public int Add(CashPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CashPlan(");
            strSql.Append("ProposalId,FamilyMonthExpense,RetainCashType,Deposit,Fund,CreditCard,Analysis)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@FamilyMonthExpense,@RetainCashType,@Deposit,@Fund,@CreditCard,@Analysis)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@FamilyMonthExpense", model.FamilyMonthExpense, dbType: DbType.Decimal);
                param.Add("@RetainCashType", model.RetainCashType, dbType: DbType.Int32);
                param.Add("@Deposit", model.Deposit, dbType: DbType.Decimal);
                param.Add("@Fund", model.Fund, dbType: DbType.Decimal);
                param.Add("@CreditCard", model.CreditCard, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
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
        public bool Update(CashPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CashPlan set ");
            strSql.Append("FamilyMonthExpense=@FamilyMonthExpense,");
            strSql.Append("RetainCashType=@RetainCashType,");
            strSql.Append("Deposit=@Deposit,");
            strSql.Append("Fund=@Fund,");
            strSql.Append("CreditCard=@CreditCard,");
            strSql.Append("Analysis=@Analysis");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@FamilyMonthExpense", model.FamilyMonthExpense, dbType: DbType.Decimal);
                param.Add("@RetainCashType", model.RetainCashType, dbType: DbType.Int32);
                param.Add("@Deposit", model.Deposit, dbType: DbType.Decimal);
                param.Add("@Fund", model.Fund, dbType: DbType.Decimal);
                param.Add("@CreditCard", model.CreditCard, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
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
            strSql.Append("delete from CashPlan ");
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
        public CashPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,FamilyMonthExpense,RetainCashType,Deposit,Fund,CreditCard,Analysis from CashPlan ");
            strSql.Append(" where Id=@Id ");

            CashPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<CashPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  获取实体---根据建议书Id
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CashPlan GetModelByProposalId(int ProposalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,FamilyMonthExpense,RetainCashType,Deposit,Fund,CreditCard,Analysis from CashPlan ");
            strSql.Append(" where ProposalId=@ProposalId ");

            CashPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<CashPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<CashPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,FamilyMonthExpense,RetainCashType,Deposit,Fund,CreditCard,Analysis ");
            strSql.Append(" FROM CashPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<CashPlan> list = new List<CashPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<CashPlan>(strSql.ToString()).ToList();
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
        public PageModel GetCashPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "CashPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,FamilyMonthExpense,RetainCashType,Deposit,Fund,CreditCard,Analysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion

    }
}

