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
    /// 数据访问类:StartAnUndertakingPlan
    /// </summary>
    public partial class StartAnUndertakingPlanDAL
    {
        public StartAnUndertakingPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StartAnUndertakingPlan where Id=@Id ");

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
        public int Add(StartAnUndertakingPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StartAnUndertakingPlan(");
            strSql.Append("ProposalId,Age,StartPlanAge,CostInput,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Age,@StartPlanAge,@CostInput,@ReturnOnInvestmentRate,@DisposableInput,@MonthlyInvestment,@RegularYear,@TargetAmount,@Analysis)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Age", model.Age, dbType: DbType.Int32);
                param.Add("@StartPlanAge", model.StartPlanAge, dbType: DbType.Int32);
               // param.Add("@DistanceYear", model.DistanceYear, dbType: DbType.Int32);
                param.Add("@CostInput", model.CostInput, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestmentRate", model.ReturnOnInvestmentRate, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Int32);
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
        public bool Update(StartAnUndertakingPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StartAnUndertakingPlan set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Age=@Age,");
            strSql.Append("StartPlanAge=@StartPlanAge,");
           // strSql.Append("DistanceYear=@DistanceYear,");
            strSql.Append("CostInput=@CostInput,");
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
                param.Add("@StartPlanAge", model.StartPlanAge, dbType: DbType.Int32);
                //param.Add("@DistanceYear", model.DistanceYear, dbType: DbType.Int32);
                param.Add("@CostInput", model.CostInput, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestmentRate", model.ReturnOnInvestmentRate, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Int32);
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
            strSql.Append("delete from StartAnUndertakingPlan ");
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
        public StartAnUndertakingPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Age,StartPlanAge,DistanceYear,CostInput,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis from StartAnUndertakingPlan ");
            strSql.Append(" where Id=@Id ");

            StartAnUndertakingPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<StartAnUndertakingPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public StartAnUndertakingPlan GetModelProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Age,StartPlanAge,DistanceYear,CostInput,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis from StartAnUndertakingPlan ");
            strSql.Append(" where ProposalId=@ProposalId ");

            StartAnUndertakingPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<StartAnUndertakingPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StartAnUndertakingPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Age,StartPlanAge,DistanceYear,CostInput,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis ");
            strSql.Append(" FROM StartAnUndertakingPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<StartAnUndertakingPlan> list = new List<StartAnUndertakingPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<StartAnUndertakingPlan>(strSql.ToString()).ToList();
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
        public PageModel GetStartAnUndertakingPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "StartAnUndertakingPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Age,StartPlanAge,DistanceYear,CostInput,ReturnOnInvestmentRate,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,Analysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

