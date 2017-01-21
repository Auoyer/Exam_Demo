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
    /// 数据访问类:IncomeAndExpenses
    /// </summary>
    public partial class IncomeAndExpensesDAL
    {
        public IncomeAndExpensesDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from IncomeAndExpenses where Id=@Id ");

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
        public int Add(IncomeAndExpenses model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into IncomeAndExpenses(");
            strSql.Append("ProposalId,JobIncome,EndowmentInsurance,MedicalInsurance,HousingFund,OtherJobIncome,FamilyExpense,ChildExpense,OtherExpense,Interest,CapitalGains,OtherIncome,InterestExpense,InsuranceExpense,OtherFinanceExpense)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@JobIncome,@EndowmentInsurance,@MedicalInsurance,@HousingFund,@OtherJobIncome,@FamilyExpense,@ChildExpense,@OtherExpense,@Interest,@CapitalGains,@OtherIncome,@InterestExpense,@InsuranceExpense,@OtherFinanceExpense)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@JobIncome", model.JobIncome, dbType: DbType.Decimal);
                param.Add("@EndowmentInsurance", model.EndowmentInsurance, dbType: DbType.Decimal);
                param.Add("@MedicalInsurance", model.MedicalInsurance, dbType: DbType.Decimal);
                param.Add("@HousingFund", model.HousingFund, dbType: DbType.Decimal);
                param.Add("@OtherJobIncome", model.OtherJobIncome, dbType: DbType.Decimal);
                param.Add("@FamilyExpense", model.FamilyExpense, dbType: DbType.Decimal);
                param.Add("@ChildExpense", model.ChildExpense, dbType: DbType.Decimal);
                param.Add("@OtherExpense", model.OtherExpense, dbType: DbType.Decimal);
                param.Add("@Interest", model.Interest, dbType: DbType.Decimal);
                param.Add("@CapitalGains", model.CapitalGains, dbType: DbType.Decimal);
                param.Add("@OtherIncome", model.OtherIncome, dbType: DbType.Decimal);
                param.Add("@InterestExpense", model.InterestExpense, dbType: DbType.Decimal);
                param.Add("@InsuranceExpense", model.InsuranceExpense, dbType: DbType.Decimal);
                param.Add("@OtherFinanceExpense", model.OtherFinanceExpense, dbType: DbType.Decimal);
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
        public bool Update(IncomeAndExpenses model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update IncomeAndExpenses set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("JobIncome=@JobIncome,");
            strSql.Append("EndowmentInsurance=@EndowmentInsurance,");
            strSql.Append("MedicalInsurance=@MedicalInsurance,");
            strSql.Append("HousingFund=@HousingFund,");
            strSql.Append("OtherJobIncome=@OtherJobIncome,");
            strSql.Append("FamilyExpense=@FamilyExpense,");
            strSql.Append("ChildExpense=@ChildExpense,");
            strSql.Append("OtherExpense=@OtherExpense,");
            strSql.Append("Interest=@Interest,");
            strSql.Append("CapitalGains=@CapitalGains,");
            strSql.Append("OtherIncome=@OtherIncome,");
            strSql.Append("InterestExpense=@InterestExpense,");
            strSql.Append("InsuranceExpense=@InsuranceExpense,");
            strSql.Append("OtherFinanceExpense=@OtherFinanceExpense");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@JobIncome", model.JobIncome, dbType: DbType.Decimal);
                param.Add("@EndowmentInsurance", model.EndowmentInsurance, dbType: DbType.Decimal);
                param.Add("@MedicalInsurance", model.MedicalInsurance, dbType: DbType.Decimal);
                param.Add("@HousingFund", model.HousingFund, dbType: DbType.Decimal);
                param.Add("@OtherJobIncome", model.OtherJobIncome, dbType: DbType.Decimal);
                param.Add("@FamilyExpense", model.FamilyExpense, dbType: DbType.Decimal);
                param.Add("@ChildExpense", model.ChildExpense, dbType: DbType.Decimal);
                param.Add("@OtherExpense", model.OtherExpense, dbType: DbType.Decimal);
                param.Add("@Interest", model.Interest, dbType: DbType.Decimal);
                param.Add("@CapitalGains", model.CapitalGains, dbType: DbType.Decimal);
                param.Add("@OtherIncome", model.OtherIncome, dbType: DbType.Decimal);
                param.Add("@InterestExpense", model.InterestExpense, dbType: DbType.Decimal);
                param.Add("@InsuranceExpense", model.InsuranceExpense, dbType: DbType.Decimal);
                param.Add("@OtherFinanceExpense", model.OtherFinanceExpense, dbType: DbType.Decimal);
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
            strSql.Append("delete from IncomeAndExpenses ");
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
        public IncomeAndExpenses GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,JobIncome,EndowmentInsurance,MedicalInsurance,HousingFund,OtherJobIncome,FamilyExpense,ChildExpense,OtherExpense,Interest,CapitalGains,OtherIncome,InterestExpense,InsuranceExpense,OtherFinanceExpense from IncomeAndExpenses ");
            strSql.Append(" where Id=@Id ");

            IncomeAndExpenses model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<IncomeAndExpenses>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public IncomeAndExpenses GetModelByProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,JobIncome,EndowmentInsurance,MedicalInsurance,HousingFund,OtherJobIncome,FamilyExpense,ChildExpense,OtherExpense,Interest,CapitalGains,OtherIncome,InterestExpense,InsuranceExpense,OtherFinanceExpense from IncomeAndExpenses ");
            strSql.Append(" where ProposalId=@ProposalId ");

            IncomeAndExpenses model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<IncomeAndExpenses>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<IncomeAndExpenses> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,JobIncome,EndowmentInsurance,MedicalInsurance,HousingFund,OtherJobIncome,FamilyExpense,ChildExpense,OtherExpense,Interest,CapitalGains,OtherIncome,InterestExpense,InsuranceExpense,OtherFinanceExpense ");
            strSql.Append(" FROM IncomeAndExpenses ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<IncomeAndExpenses> list = new List<IncomeAndExpenses>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<IncomeAndExpenses>(strSql.ToString()).ToList();
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
        public PageModel GetIncomeAndExpensesPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "IncomeAndExpenses";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,JobIncome,EndowmentInsurance,MedicalInsurance,HousingFund,OtherJobIncome,FamilyExpense,ChildExpense,OtherExpense,Interest,CapitalGains,OtherIncome,InterestExpense,InsuranceExpense,OtherFinanceExpense";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

