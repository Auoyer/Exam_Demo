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
    /// 数据访问类:RetirementPlan
    /// </summary>
    public partial class RetirementPlanDAL
    {
        public RetirementPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RetirementPlan where Id=@Id ");

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
        public int Add(RetirementPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RetirementPlan(");
            strSql.Append("ProposalId,Age,BeforeInflationRate,AfterInflationRate,RetirementRate,SociaSecurityRate,RentRate,OtherRate,RetirementAge,RetirementYears,LivingStandardNow,Satisfaction,SatisfactionLivingStandard,ConvertProportion,lineageFee,RetirementLivingStandard,AfterLivingStandard,SocialInsurance,CommercialInsurance,RentIncome,OtherIncome,TotalIncome,TotalAmount,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Age,@BeforeInflationRate,@AfterInflationRate,@RetirementRate,@SociaSecurityRate,@RentRate,@OtherRate,@RetirementAge,@RetirementYears,@LivingStandardNow,@Satisfaction,@SatisfactionLivingStandard,@ConvertProportion,@lineageFee,@RetirementLivingStandard,@AfterLivingStandard,@SocialInsurance,@CommercialInsurance,@RentIncome,@OtherIncome,@TotalIncome,@TotalAmount,@ReturnOnInvestmentRate,@DisposableInput,@MonthlyInvestment,@RegularYear,@TargetAmount,@Analysis)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@BeforeInflationRate", model.BeforeInflationRate, dbType: DbType.Decimal);
                param.Add("@AfterInflationRate", model.AfterInflationRate, dbType: DbType.Decimal);
                param.Add("@RetirementRate", model.RetirementRate, dbType: DbType.Decimal);
                param.Add("@SociaSecurityRate", model.SociaSecurityRate, dbType: DbType.Decimal);
                param.Add("@RentRate", model.RentRate, dbType: DbType.Decimal);
                param.Add("@OtherRate", model.OtherRate, dbType: DbType.Decimal);
                param.Add("@RetirementAge", model.RetirementAge, dbType: DbType.Int32);
                param.Add("@RetirementYears", model.RetirementYears, dbType: DbType.Int32);
                param.Add("@LivingStandardNow", model.LivingStandardNow, dbType: DbType.Decimal);
                param.Add("@Satisfaction", model.Satisfaction, dbType: DbType.Int32);
                param.Add("@SatisfactionLivingStandard", model.SatisfactionLivingStandard, dbType: DbType.Decimal);
                param.Add("@ConvertProportion", model.ConvertProportion, dbType: DbType.Int32);
                param.Add("@lineageFee", model.lineageFee, dbType: DbType.Decimal);
                param.Add("@RetirementLivingStandard", model.RetirementLivingStandard, dbType: DbType.Decimal);
                param.Add("@AfterLivingStandard", model.AfterLivingStandard, dbType: DbType.Decimal);
                param.Add("@SocialInsurance", model.SocialInsurance, dbType: DbType.Decimal);
                param.Add("@CommercialInsurance", model.CommercialInsurance, dbType: DbType.Decimal);
                param.Add("@RentIncome", model.RentIncome, dbType: DbType.Decimal);
                param.Add("@OtherIncome", model.OtherIncome, dbType: DbType.Decimal);
                param.Add("@TotalIncome", model.TotalIncome, dbType: DbType.Decimal);
                param.Add("@TotalAmount", model.TotalAmount, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestmentRate", model.ReturnOnInvestmentRate, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Decimal);
                param.Add("@TargetAmount", model.TargetAmount, dbType: DbType.Decimal);
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
        public bool Update(RetirementPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RetirementPlan set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Age=@Age,");
            strSql.Append("BeforeInflationRate=@BeforeInflationRate,");
            strSql.Append("AfterInflationRate=@AfterInflationRate,");
            strSql.Append("RetirementRate=@RetirementRate,");
            strSql.Append("SociaSecurityRate=@SociaSecurityRate,");
            strSql.Append("RentRate=@RentRate,");
            strSql.Append("OtherRate=@OtherRate,");
            strSql.Append("RetirementAge=@RetirementAge,");
            strSql.Append("RetirementYears=@RetirementYears,");
            strSql.Append("LivingStandardNow=@LivingStandardNow,");
            strSql.Append("Satisfaction=@Satisfaction,");
            strSql.Append("SatisfactionLivingStandard=@SatisfactionLivingStandard,");
            strSql.Append("ConvertProportion=@ConvertProportion,");
            strSql.Append("lineageFee=@lineageFee,");
            strSql.Append("RetirementLivingStandard=@RetirementLivingStandard,");
            strSql.Append("AfterLivingStandard=@AfterLivingStandard,");
            strSql.Append("SocialInsurance=@SocialInsurance,");
            strSql.Append("CommercialInsurance=@CommercialInsurance,");
            strSql.Append("RentIncome=@RentIncome,");
            strSql.Append("OtherIncome=@OtherIncome,");
            strSql.Append("TotalIncome=@TotalIncome,");
            strSql.Append("TotalAmount=@TotalAmount,");
            strSql.Append("ReturnOnInvestmentRate=@ReturnOnInvestmentRate,");
            strSql.Append("DisposableInput=@DisposableInput,");
            strSql.Append("MonthlyInvestment=@MonthlyInvestment,");
            strSql.Append("RegularYear=@RegularYear,");
            strSql.Append("TargetAmount=@TargetAmount,");
            strSql.Append("Analysis=@Analysis");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@BeforeInflationRate", model.BeforeInflationRate, dbType: DbType.Decimal);
                param.Add("@AfterInflationRate", model.AfterInflationRate, dbType: DbType.Decimal);
                param.Add("@RetirementRate", model.RetirementRate, dbType: DbType.Decimal);
                param.Add("@SociaSecurityRate", model.SociaSecurityRate, dbType: DbType.Decimal);
                param.Add("@RentRate", model.RentRate, dbType: DbType.Decimal);
                param.Add("@OtherRate", model.OtherRate, dbType: DbType.Decimal);
                param.Add("@RetirementAge", model.RetirementAge, dbType: DbType.Int32);
                param.Add("@RetirementYears", model.RetirementYears, dbType: DbType.Int32);
                param.Add("@LivingStandardNow", model.LivingStandardNow, dbType: DbType.Decimal);
                param.Add("@Satisfaction", model.Satisfaction, dbType: DbType.Int32);
                param.Add("@SatisfactionLivingStandard", model.SatisfactionLivingStandard, dbType: DbType.Decimal);
                param.Add("@ConvertProportion", model.ConvertProportion, dbType: DbType.Int32);
                param.Add("@lineageFee", model.lineageFee, dbType: DbType.Decimal);
                param.Add("@RetirementLivingStandard", model.RetirementLivingStandard, dbType: DbType.Decimal);
                param.Add("@AfterLivingStandard", model.AfterLivingStandard, dbType: DbType.Decimal);
                param.Add("@SocialInsurance", model.SocialInsurance, dbType: DbType.Decimal);
                param.Add("@CommercialInsurance", model.CommercialInsurance, dbType: DbType.Decimal);
                param.Add("@RentIncome", model.RentIncome, dbType: DbType.Decimal);
                param.Add("@OtherIncome", model.OtherIncome, dbType: DbType.Decimal);
                param.Add("@TotalIncome", model.TotalIncome, dbType: DbType.Decimal);
                param.Add("@TotalAmount", model.TotalAmount, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestmentRate", model.ReturnOnInvestmentRate, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Decimal);
                param.Add("@TargetAmount", model.TargetAmount, dbType: DbType.Decimal);
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
            strSql.Append("delete from RetirementPlan ");
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
        public RetirementPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Age,BeforeInflationRate,AfterInflationRate,RetirementRate,SociaSecurityRate,RentRate,OtherRate,RetirementAge,RetirementYears,LivingStandardNow,Satisfaction,SatisfactionLivingStandard,ConvertProportion,lineageFee,RetirementLivingStandard,AfterLivingStandard,SocialInsurance,CommercialInsurance,RentIncome,OtherIncome,TotalIncome,TotalAmount,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis from RetirementPlan ");
            strSql.Append(" where Id=@Id ");

            RetirementPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<RetirementPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RetirementPlan GetModelByProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Age,BeforeInflationRate,AfterInflationRate,RetirementRate,SociaSecurityRate,RentRate,OtherRate,RetirementAge,RetirementYears,LivingStandardNow,Satisfaction,SatisfactionLivingStandard,ConvertProportion,lineageFee,RetirementLivingStandard,AfterLivingStandard,SocialInsurance,CommercialInsurance,RentIncome,OtherIncome,TotalIncome,TotalAmount,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis from RetirementPlan ");
            strSql.Append(" where ProposalId=@ProposalId ");

            RetirementPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<RetirementPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RetirementPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Age,BeforeInflationRate,AfterInflationRate,RetirementRate,SociaSecurityRate,RentRate,OtherRate,RetirementAge,RetirementYears,LivingStandardNow,Satisfaction,SatisfactionLivingStandard,ConvertProportion,lineageFee,RetirementLivingStandard,AfterLivingStandard,SocialInsurance,CommercialInsurance,RentIncome,OtherIncome,TotalIncome,TotalAmount,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis ");
            strSql.Append(" FROM RetirementPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<RetirementPlan> list = new List<RetirementPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<RetirementPlan>(strSql.ToString()).ToList();
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
        public PageModel GetRetirementPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "RetirementPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Age,BeforeInflationRate,AfterInflationRate,RetirementRate,SociaSecurityRate,RentRate,OtherRate,RetirementAge,RetirementYears,LivingStandardNow,Satisfaction,SatisfactionLivingStandard,ConvertProportion,lineageFee,RetirementLivingStandard,AfterLivingStandard,SocialInsurance,CommercialInsurance,RentIncome,OtherIncome,TotalIncome,TotalAmount,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

