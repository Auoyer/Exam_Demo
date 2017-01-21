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
    /// 数据访问类:InsurancePlan
    /// </summary>
    public partial class InsurancePlanDAL
    {
        public InsurancePlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from InsurancePlan where Id=@Id ");

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
        public int Add(InsurancePlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into InsurancePlan(");
            strSql.Append("ProposalId,MethodTypeId,Age1,Age2,RetirementAge1,RetirementAge2,ReturnOnInvestment1,ReturnOnInvestment2,InflationRate1,InflationRate2,RevenueGrowth1,RevenueGrowth2,MatrimonialFee1,MatrimonialFee2,AfterAccidentRate1,AfterAccidentRate2,Income1,Income2,ReserveFund1,ReserveFund2,EduAmount1,EduAmount2,PensionFunds1,PensionFunds2,DeathExpense1,DeathExpense2,LoanBalance1,LoanBalance2,EarningAssets1,EarningAssets2,InsuranceAmount1,InsuranceAmount2,BudgetAmount1,BudgetAmount2,SupplementaryQuota1,SupplementaryQuota2,Analysis,SpouseName,Expenditure)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@MethodTypeId,@Age1,@Age2,@RetirementAge1,@RetirementAge2,@ReturnOnInvestment1,@ReturnOnInvestment2,@InflationRate1,@InflationRate2,@RevenueGrowth1,@RevenueGrowth2,@MatrimonialFee1,@MatrimonialFee2,@AfterAccidentRate1,@AfterAccidentRate2,@Income1,@Income2,@ReserveFund1,@ReserveFund2,@EduAmount1,@EduAmount2,@PensionFunds1,@PensionFunds2,@DeathExpense1,@DeathExpense2,@LoanBalance1,@LoanBalance2,@EarningAssets1,@EarningAssets2,@InsuranceAmount1,@InsuranceAmount2,@BudgetAmount1,@BudgetAmount2,@SupplementaryQuota1,@SupplementaryQuota2,@Analysis,@SpouseName,@Expenditure)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@MethodTypeId", model.MethodTypeId, dbType: DbType.Int32);
                param.Add("@Age1", model.Age1, dbType: DbType.Int32);
                param.Add("@Age2", model.Age2, dbType: DbType.Int32);
                param.Add("@RetirementAge1", model.RetirementAge1, dbType: DbType.Int32);
                param.Add("@RetirementAge2", model.RetirementAge2, dbType: DbType.Int32);
                param.Add("@ReturnOnInvestment1", model.ReturnOnInvestment1, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestment2", model.ReturnOnInvestment2, dbType: DbType.Decimal);
                param.Add("@InflationRate1", model.InflationRate1, dbType: DbType.Decimal);
                param.Add("@InflationRate2", model.InflationRate2, dbType: DbType.Decimal);
                param.Add("@RevenueGrowth1", model.RevenueGrowth1, dbType: DbType.Decimal);
                param.Add("@RevenueGrowth2", model.RevenueGrowth2, dbType: DbType.Decimal);
                param.Add("@MatrimonialFee1", model.MatrimonialFee1, dbType: DbType.Decimal);
                param.Add("@MatrimonialFee2", model.MatrimonialFee2, dbType: DbType.Decimal);
                param.Add("@AfterAccidentRate1", model.AfterAccidentRate1, dbType: DbType.Decimal);
                param.Add("@AfterAccidentRate2", model.AfterAccidentRate2, dbType: DbType.Decimal);
                param.Add("@Income1", model.Income1, dbType: DbType.Decimal);
                param.Add("@Income2", model.Income2, dbType: DbType.Decimal);
                param.Add("@ReserveFund1", model.ReserveFund1, dbType: DbType.Decimal);
                param.Add("@ReserveFund2", model.ReserveFund2, dbType: DbType.Decimal);
                param.Add("@EduAmount1", model.EduAmount1, dbType: DbType.Decimal);
                param.Add("@EduAmount2", model.EduAmount2, dbType: DbType.Decimal);
                param.Add("@PensionFunds1", model.PensionFunds1, dbType: DbType.Decimal);
                param.Add("@PensionFunds2", model.PensionFunds2, dbType: DbType.Decimal);
                param.Add("@DeathExpense1", model.DeathExpense1, dbType: DbType.Decimal);
                param.Add("@DeathExpense2", model.DeathExpense2, dbType: DbType.Decimal);
                param.Add("@LoanBalance1", model.LoanBalance1, dbType: DbType.Decimal);
                param.Add("@LoanBalance2", model.LoanBalance2, dbType: DbType.Decimal);
                param.Add("@EarningAssets1", model.EarningAssets1, dbType: DbType.Decimal);
                param.Add("@EarningAssets2", model.EarningAssets2, dbType: DbType.Decimal);
                param.Add("@InsuranceAmount1", model.InsuranceAmount1, dbType: DbType.Decimal);
                param.Add("@InsuranceAmount2", model.InsuranceAmount2, dbType: DbType.Decimal);
                param.Add("@BudgetAmount1", model.BudgetAmount1, dbType: DbType.Decimal);
                param.Add("@BudgetAmount2", model.BudgetAmount2, dbType: DbType.Decimal);
                param.Add("@SupplementaryQuota1", model.SupplementaryQuota1, dbType: DbType.Decimal);
                param.Add("@SupplementaryQuota2", model.SupplementaryQuota2, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@SpouseName", model.SpouseName, dbType: DbType.String);
                param.Add("@Expenditure", model.Expenditure, dbType: DbType.Decimal);
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
        public bool Update(InsurancePlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update InsurancePlan set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("MethodTypeId=@MethodTypeId,");
            strSql.Append("Age1=@Age1,");
            strSql.Append("Age2=@Age2,");
            strSql.Append("RetirementAge1=@RetirementAge1,");
            strSql.Append("RetirementAge2=@RetirementAge2,");
            strSql.Append("ReturnOnInvestment1=@ReturnOnInvestment1,");
            strSql.Append("ReturnOnInvestment2=@ReturnOnInvestment2,");
            strSql.Append("InflationRate1=@InflationRate1,");
            strSql.Append("InflationRate2=@InflationRate2,");
            strSql.Append("RevenueGrowth1=@RevenueGrowth1,");
            strSql.Append("RevenueGrowth2=@RevenueGrowth2,");
            strSql.Append("MatrimonialFee1=@MatrimonialFee1,");
            strSql.Append("MatrimonialFee2=@MatrimonialFee2,");
            strSql.Append("AfterAccidentRate1=@AfterAccidentRate1,");
            strSql.Append("AfterAccidentRate2=@AfterAccidentRate2,");
            strSql.Append("Income1=@Income1,");
            strSql.Append("Income2=@Income2,");
            strSql.Append("ReserveFund1=@ReserveFund1,");
            strSql.Append("ReserveFund2=@ReserveFund2,");
            strSql.Append("EduAmount1=@EduAmount1,");
            strSql.Append("EduAmount2=@EduAmount2,");
            strSql.Append("PensionFunds1=@PensionFunds1,");
            strSql.Append("PensionFunds2=@PensionFunds2,");
            strSql.Append("DeathExpense1=@DeathExpense1,");
            strSql.Append("DeathExpense2=@DeathExpense2,");
            strSql.Append("LoanBalance1=@LoanBalance1,");
            strSql.Append("LoanBalance2=@LoanBalance2,");
            strSql.Append("EarningAssets1=@EarningAssets1,");
            strSql.Append("EarningAssets2=@EarningAssets2,");
            strSql.Append("InsuranceAmount1=@InsuranceAmount1,");
            strSql.Append("InsuranceAmount2=@InsuranceAmount2,");
            strSql.Append("BudgetAmount1=@BudgetAmount1,");
            strSql.Append("BudgetAmount2=@BudgetAmount2,");
            strSql.Append("SupplementaryQuota1=@SupplementaryQuota1,");
            strSql.Append("SupplementaryQuota2=@SupplementaryQuota2,");
            strSql.Append("Analysis=@Analysis,");
            strSql.Append("SpouseName=@SpouseName,");
            strSql.Append("Expenditure=@Expenditure");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@MethodTypeId", model.MethodTypeId, dbType: DbType.Int32);
                param.Add("@Age1", model.Age1, dbType: DbType.Int32);
                param.Add("@Age2", model.Age2, dbType: DbType.Int32);
                param.Add("@RetirementAge1", model.RetirementAge1, dbType: DbType.Int32);
                param.Add("@RetirementAge2", model.RetirementAge2, dbType: DbType.Int32);
                param.Add("@ReturnOnInvestment1", model.ReturnOnInvestment1, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestment2", model.ReturnOnInvestment2, dbType: DbType.Decimal);
                param.Add("@InflationRate1", model.InflationRate1, dbType: DbType.Decimal);
                param.Add("@InflationRate2", model.InflationRate2, dbType: DbType.Decimal);
                param.Add("@RevenueGrowth1", model.RevenueGrowth1, dbType: DbType.Decimal);
                param.Add("@RevenueGrowth2", model.RevenueGrowth2, dbType: DbType.Decimal);
                param.Add("@MatrimonialFee1", model.MatrimonialFee1, dbType: DbType.Decimal);
                param.Add("@MatrimonialFee2", model.MatrimonialFee2, dbType: DbType.Decimal);
                param.Add("@AfterAccidentRate1", model.AfterAccidentRate1, dbType: DbType.Decimal);
                param.Add("@AfterAccidentRate2", model.AfterAccidentRate2, dbType: DbType.Decimal);
                param.Add("@Income1", model.Income1, dbType: DbType.Decimal);
                param.Add("@Income2", model.Income2, dbType: DbType.Decimal);
                param.Add("@ReserveFund1", model.ReserveFund1, dbType: DbType.Decimal);
                param.Add("@ReserveFund2", model.ReserveFund2, dbType: DbType.Decimal);
                param.Add("@EduAmount1", model.EduAmount1, dbType: DbType.Decimal);
                param.Add("@EduAmount2", model.EduAmount2, dbType: DbType.Decimal);
                param.Add("@PensionFunds1", model.PensionFunds1, dbType: DbType.Decimal);
                param.Add("@PensionFunds2", model.PensionFunds2, dbType: DbType.Decimal);
                param.Add("@DeathExpense1", model.DeathExpense1, dbType: DbType.Decimal);
                param.Add("@DeathExpense2", model.DeathExpense2, dbType: DbType.Decimal);
                param.Add("@LoanBalance1", model.LoanBalance1, dbType: DbType.Decimal);
                param.Add("@LoanBalance2", model.LoanBalance2, dbType: DbType.Decimal);
                param.Add("@EarningAssets1", model.EarningAssets1, dbType: DbType.Decimal);
                param.Add("@EarningAssets2", model.EarningAssets2, dbType: DbType.Decimal);
                param.Add("@InsuranceAmount1", model.InsuranceAmount1, dbType: DbType.Decimal);
                param.Add("@InsuranceAmount2", model.InsuranceAmount2, dbType: DbType.Decimal);
                param.Add("@BudgetAmount1", model.BudgetAmount1, dbType: DbType.Decimal);
                param.Add("@BudgetAmount2", model.BudgetAmount2, dbType: DbType.Decimal);
                param.Add("@SupplementaryQuota1", model.SupplementaryQuota1, dbType: DbType.Decimal);
                param.Add("@SupplementaryQuota2", model.SupplementaryQuota2, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@SpouseName", model.SpouseName, dbType: DbType.String);
                param.Add("@Expenditure", model.Expenditure, dbType: DbType.Decimal);
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
            strSql.Append("delete from InsurancePlan ");
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
        public InsurancePlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,MethodTypeId,Age1,Age2,RetirementAge1,RetirementAge2,ReturnOnInvestment1,ReturnOnInvestment2,InflationRate1,InflationRate2,RevenueGrowth1,RevenueGrowth2,MatrimonialFee1,MatrimonialFee2,AfterAccidentRate1,AfterAccidentRate2,Income1,Income2,ReserveFund1,ReserveFund2,EduAmount1,EduAmount2,PensionFunds1,PensionFunds2,DeathExpense1,DeathExpense2,LoanBalance1,LoanBalance2,EarningAssets1,EarningAssets2,InsuranceAmount1,InsuranceAmount2,BudgetAmount1,BudgetAmount2,SupplementaryQuota1,SupplementaryQuota2,Analysis,SpouseName,Expenditure from InsurancePlan ");
            strSql.Append(" where Id=@Id ");

            InsurancePlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<InsurancePlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体---
        /// </summary>
        public InsurancePlan GetModelByProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,MethodTypeId,Age1,Age2,RetirementAge1,RetirementAge2,ReturnOnInvestment1,ReturnOnInvestment2,InflationRate1,InflationRate2,RevenueGrowth1,RevenueGrowth2,MatrimonialFee1,MatrimonialFee2,AfterAccidentRate1,AfterAccidentRate2,Income1,Income2,ReserveFund1,ReserveFund2,EduAmount1,EduAmount2,PensionFunds1,PensionFunds2,DeathExpense1,DeathExpense2,LoanBalance1,LoanBalance2,EarningAssets1,EarningAssets2,InsuranceAmount1,InsuranceAmount2,BudgetAmount1,BudgetAmount2,SupplementaryQuota1,SupplementaryQuota2,Analysis,SpouseName,Expenditure from InsurancePlan ");
            strSql.Append(" where ProposalId=@ProposalId ");

            InsurancePlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<InsurancePlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<InsurancePlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,MethodTypeId,Age1,Age2,RetirementAge1,RetirementAge2,ReturnOnInvestment1,ReturnOnInvestment2,InflationRate1,InflationRate2,RevenueGrowth1,RevenueGrowth2,MatrimonialFee1,MatrimonialFee2,AfterAccidentRate1,AfterAccidentRate2,Income1,Income2,ReserveFund1,ReserveFund2,EduAmount1,EduAmount2,PensionFunds1,PensionFunds2,DeathExpense1,DeathExpense2,LoanBalance1,LoanBalance2,EarningAssets1,EarningAssets2,InsuranceAmount1,InsuranceAmount2,BudgetAmount1,BudgetAmount2,SupplementaryQuota1,SupplementaryQuota2,Analysis,SpouseName,Expenditure ");
            strSql.Append(" FROM InsurancePlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<InsurancePlan> list = new List<InsurancePlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<InsurancePlan>(strSql.ToString()).ToList();
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
        public PageModel GetInsurancePlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "InsurancePlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,MethodTypeId,Age1,Age2,RetirementAge1,RetirementAge2,ReturnOnInvestment1,ReturnOnInvestment2,InflationRate1,InflationRate2,RevenueGrowth1,RevenueGrowth2,MatrimonialFee1,MatrimonialFee2,AfterAccidentRate1,AfterAccidentRate2,Income1,Income2,ReserveFund1,ReserveFund2,EduAmount1,EduAmount2,PensionFunds1,PensionFunds2,DeathExpense1,DeathExpense2,LoanBalance1,LoanBalance2,EarningAssets1,EarningAssets2,InsuranceAmount1,InsuranceAmount2,BudgetAmount1,BudgetAmount2,SupplementaryQuota1,SupplementaryQuota2,Analysis,SpouseName,Expenditure";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

