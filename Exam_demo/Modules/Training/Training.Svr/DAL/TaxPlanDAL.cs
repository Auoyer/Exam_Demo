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
    /// 数据访问类:TaxPlan
    /// </summary>
    public partial class TaxPlanDAL
    {
        public TaxPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TaxPlan where Id=@Id ");

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
        public int Add(TaxPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TaxPlan(");
            strSql.Append("ProposalId,Salary,SalaryTax,OperatingRevenue,OperatingRevenueTax,EnterprisesRevenue,EnterprisesRevenueTax,ServiceIncome,ServiceIncomeTax,Remuneration,RemunerationTax,Loyalities,LoyalitiesTax,Demise,DemiseTax,IncidentalIncome,IncidentalIncomeTax,Interest,InterestTax,TotalAmount,TotalTax,Analysis)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Salary,@SalaryTax,@OperatingRevenue,@OperatingRevenueTax,@EnterprisesRevenue,@EnterprisesRevenueTax,@ServiceIncome,@ServiceIncomeTax,@Remuneration,@RemunerationTax,@Loyalities,@LoyalitiesTax,@Demise,@DemiseTax,@IncidentalIncome,@IncidentalIncomeTax,@Interest,@InterestTax,@TotalAmount,@TotalTax,@Analysis)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Salary", model.Salary, dbType: DbType.Decimal);
                param.Add("@SalaryTax", model.SalaryTax, dbType: DbType.Decimal);
                param.Add("@OperatingRevenue", model.OperatingRevenue, dbType: DbType.Decimal);
                param.Add("@OperatingRevenueTax", model.OperatingRevenueTax, dbType: DbType.Decimal);
                param.Add("@EnterprisesRevenue", model.EnterprisesRevenue, dbType: DbType.Decimal);
                param.Add("@EnterprisesRevenueTax", model.EnterprisesRevenueTax, dbType: DbType.Decimal);
                param.Add("@ServiceIncome", model.ServiceIncome, dbType: DbType.Decimal);
                param.Add("@ServiceIncomeTax", model.ServiceIncomeTax, dbType: DbType.Decimal);
                param.Add("@Remuneration", model.Remuneration, dbType: DbType.Decimal);
                param.Add("@RemunerationTax", model.RemunerationTax, dbType: DbType.Decimal);
                param.Add("@Loyalities", model.Loyalities, dbType: DbType.Decimal);
                param.Add("@LoyalitiesTax", model.LoyalitiesTax, dbType: DbType.Decimal);
                param.Add("@Demise", model.Demise, dbType: DbType.Decimal);
                param.Add("@DemiseTax", model.DemiseTax, dbType: DbType.Decimal);
                param.Add("@IncidentalIncome", model.IncidentalIncome, dbType: DbType.Decimal);
                param.Add("@IncidentalIncomeTax", model.IncidentalIncomeTax, dbType: DbType.Decimal);
                param.Add("@Interest", model.Interest, dbType: DbType.Decimal);
                param.Add("@InterestTax", model.InterestTax, dbType: DbType.Decimal);
                param.Add("@TotalAmount", model.TotalAmount, dbType: DbType.Decimal);
                param.Add("@TotalTax", model.TotalTax, dbType: DbType.Decimal);
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
        public bool Update(TaxPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TaxPlan set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Salary=@Salary,");
            strSql.Append("SalaryTax=@SalaryTax,");
            strSql.Append("OperatingRevenue=@OperatingRevenue,");
            strSql.Append("OperatingRevenueTax=@OperatingRevenueTax,");
            strSql.Append("EnterprisesRevenue=@EnterprisesRevenue,");
            strSql.Append("EnterprisesRevenueTax=@EnterprisesRevenueTax,");
            strSql.Append("ServiceIncome=@ServiceIncome,");
            strSql.Append("ServiceIncomeTax=@ServiceIncomeTax,");
            strSql.Append("Remuneration=@Remuneration,");
            strSql.Append("RemunerationTax=@RemunerationTax,");
            strSql.Append("Loyalities=@Loyalities,");
            strSql.Append("LoyalitiesTax=@LoyalitiesTax,");
            strSql.Append("Demise=@Demise,");
            strSql.Append("DemiseTax=@DemiseTax,");
            strSql.Append("IncidentalIncome=@IncidentalIncome,");
            strSql.Append("IncidentalIncomeTax=@IncidentalIncomeTax,");
            strSql.Append("Interest=@Interest,");
            strSql.Append("InterestTax=@InterestTax,");
            strSql.Append("TotalAmount=@TotalAmount,");
            strSql.Append("TotalTax=@TotalTax,");
            strSql.Append("Analysis=@Analysis");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Salary", model.Salary, dbType: DbType.Decimal);
                param.Add("@SalaryTax", model.SalaryTax, dbType: DbType.Decimal);
                param.Add("@OperatingRevenue", model.OperatingRevenue, dbType: DbType.Decimal);
                param.Add("@OperatingRevenueTax", model.OperatingRevenueTax, dbType: DbType.Decimal);
                param.Add("@EnterprisesRevenue", model.EnterprisesRevenue, dbType: DbType.Decimal);
                param.Add("@EnterprisesRevenueTax", model.EnterprisesRevenueTax, dbType: DbType.Decimal);
                param.Add("@ServiceIncome", model.ServiceIncome, dbType: DbType.Decimal);
                param.Add("@ServiceIncomeTax", model.ServiceIncomeTax, dbType: DbType.Decimal);
                param.Add("@Remuneration", model.Remuneration, dbType: DbType.Decimal);
                param.Add("@RemunerationTax", model.RemunerationTax, dbType: DbType.Decimal);
                param.Add("@Loyalities", model.Loyalities, dbType: DbType.Decimal);
                param.Add("@LoyalitiesTax", model.LoyalitiesTax, dbType: DbType.Decimal);
                param.Add("@Demise", model.Demise, dbType: DbType.Decimal);
                param.Add("@DemiseTax", model.DemiseTax, dbType: DbType.Decimal);
                param.Add("@IncidentalIncome", model.IncidentalIncome, dbType: DbType.Decimal);
                param.Add("@IncidentalIncomeTax", model.IncidentalIncomeTax, dbType: DbType.Decimal);
                param.Add("@Interest", model.Interest, dbType: DbType.Decimal);
                param.Add("@InterestTax", model.InterestTax, dbType: DbType.Decimal);
                param.Add("@TotalAmount", model.TotalAmount, dbType: DbType.Decimal);
                param.Add("@TotalTax", model.TotalTax, dbType: DbType.Decimal);
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
            strSql.Append("delete from TaxPlan ");
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
        public TaxPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Salary,SalaryTax,OperatingRevenue,OperatingRevenueTax,EnterprisesRevenue,EnterprisesRevenueTax,ServiceIncome,ServiceIncomeTax,Remuneration,RemunerationTax,Loyalities,LoyalitiesTax,Demise,DemiseTax,IncidentalIncome,IncidentalIncomeTax,Interest,InterestTax,TotalAmount,TotalTax,Analysis from TaxPlan ");
            strSql.Append(" where Id=@Id ");

            TaxPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<TaxPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TaxPlan GetModel2(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Salary,SalaryTax,OperatingRevenue,OperatingRevenueTax,EnterprisesRevenue,EnterprisesRevenueTax,ServiceIncome,ServiceIncomeTax,Remuneration,RemunerationTax,Loyalities,LoyalitiesTax,Demise,DemiseTax,IncidentalIncome,IncidentalIncomeTax,Interest,InterestTax,TotalAmount,TotalTax,Analysis from TaxPlan ");
            strSql.Append(" where ProposalId=@ProposalId ");

            TaxPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<TaxPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TaxPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Salary,SalaryTax,OperatingRevenue,OperatingRevenueTax,EnterprisesRevenue,EnterprisesRevenueTax,ServiceIncome,ServiceIncomeTax,Remuneration,RemunerationTax,Loyalities,LoyalitiesTax,Demise,DemiseTax,IncidentalIncome,IncidentalIncomeTax,Interest,InterestTax,TotalAmount,TotalTax,Analysis ");
            strSql.Append(" FROM TaxPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<TaxPlan> list = new List<TaxPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<TaxPlan>(strSql.ToString()).ToList();
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
        public PageModel GetTaxPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "TaxPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Salary,SalaryTax,OperatingRevenue,OperatingRevenueTax,EnterprisesRevenue,EnterprisesRevenueTax,ServiceIncome,ServiceIncomeTax,Remuneration,RemunerationTax,Loyalities,LoyalitiesTax,Demise,DemiseTax,IncidentalIncome,IncidentalIncomeTax,Interest,InterestTax,TotalAmount,TotalTax,Analysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

