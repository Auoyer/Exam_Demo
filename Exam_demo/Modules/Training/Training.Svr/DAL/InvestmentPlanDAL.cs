using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Utils;
using Training.API;

namespace Training.Svr
{
    /// <summary>
    /// 数据访问类:InvestmentPlan
    /// </summary>
    public partial class InvestmentPlanDAL
    {
        public InvestmentPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from InvestmentPlan where Id=@Id ");

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
        public int Add(InvestmentPlan model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                #region 投资规划
                strSql.Append("insert into InvestmentPlan(");
                strSql.Append("ProposalId,LifeCycleId,Analysis,HoldRate,IncreaseRate,SpeculationRate)");

                strSql.Append(" values (");
                strSql.Append("@ProposalId,@LifeCycleId,@Analysis,@HoldRate,@IncreaseRate,@SpeculationRate)");
                strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@LifeCycleId", model.LifeCycleId, dbType: DbType.Int32);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@HoldRate", model.HoldRate, dbType: DbType.Decimal);
                param.Add("@IncreaseRate", model.IncreaseRate, dbType: DbType.Decimal);
                param.Add("@SpeculationRate", model.SpeculationRate, dbType: DbType.Decimal);
                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param,tran);
                result = param.Get<int>("@returnid");
                #endregion

                #region 投资规划对应的产品类型

                if (model.InvestmentPlanProductList != null && model.InvestmentPlanProductList.Count > 0)
                {
                    foreach (var item in model.InvestmentPlanProductList)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into InvestmentPlanProduct(");
                        strSql.Append("ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3,P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate)");

                        strSql.Append(" values (");
                        strSql.Append("@ProposalId,@PlanId,@PlanRate,@DemandDepositsBank,@DemandDepositsBankRate,@TimeDepositBank,@TimeDepositBankTime,@TimeDepositBankRate,@Fund1,@Fund2,@P2PProduct,@P2PProductRate,@Fund3,@TotalRate,@CashCode,@CashFund,@CashMarket,@YearlyEarningsRate1,@BondCode,@BondFund,@BondMarket,@YearlyEarningsRate2,@StockCode,@StockFund,@StockMarket,@YearlyEarningsRate3,@P2PName,@InvestmentField,@InvestmentCycle,@StartAmount,@EarningsRate)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProposalId", item.ProposalId, dbType: DbType.Int32);
                        param.Add("@PlanId", item.PlanId, dbType: DbType.Int32);
                        param.Add("@PlanRate", item.PlanRate, dbType: DbType.Decimal);
                        param.Add("@DemandDepositsBank", item.DemandDepositsBank, dbType: DbType.Int32);
                        param.Add("@DemandDepositsBankRate", item.DemandDepositsBankRate, dbType: DbType.Decimal);
                        param.Add("@TimeDepositBank", item.TimeDepositBank, dbType: DbType.Int32);
                        param.Add("@TimeDepositBankTime", item.TimeDepositBankTime, dbType: DbType.Int32);
                        param.Add("@TimeDepositBankRate", item.TimeDepositBankRate, dbType: DbType.Decimal);
                        param.Add("@Fund1", item.Fund1, dbType: DbType.Int32);
                        param.Add("@Fund2", item.Fund2, dbType: DbType.Int32);
                        param.Add("@P2PProduct", item.P2PProduct, dbType: DbType.Int32);
                        param.Add("@P2PProductRate", item.P2PProductRate, dbType: DbType.Decimal);
                        param.Add("@Fund3", item.Fund3, dbType: DbType.Int32);
                        param.Add("@TotalRate", item.TotalRate, dbType: DbType.Decimal);

                        param.Add("@CashCode", item.CashCode, dbType: DbType.String);
                        param.Add("@CashFund", item.CashFund, dbType: DbType.String);
                        param.Add("@CashMarket", item.CashMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate1", item.YearlyEarningsRate1, dbType: DbType.Decimal);
                        param.Add("@BondCode", item.BondCode, dbType: DbType.String);
                        param.Add("@BondFund", item.BondFund, dbType: DbType.String);
                        param.Add("@BondMarket", item.BondMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate2", item.YearlyEarningsRate2, dbType: DbType.Decimal);
                        param.Add("@StockCode", item.StockCode, dbType: DbType.String);
                        param.Add("@StockFund", item.StockFund, dbType: DbType.String);
                        param.Add("@StockMarket", item.StockMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate3", item.YearlyEarningsRate3, dbType: DbType.Decimal);

                        param.Add("@P2PName", item.P2PName, dbType: DbType.String);
                        param.Add("@InvestmentField", item.InvestmentField, dbType: DbType.String);
                        param.Add("@InvestmentCycle", item.InvestmentCycle, dbType: DbType.String);
                        param.Add("@StartAmount", item.StartAmount, dbType: DbType.String);
                        param.Add("@EarningsRate", item.EarningsRate, dbType: DbType.String);

                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
  
                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }

                #endregion
                tran.Commit();

            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]新增案例出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
            }
            return result;

        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(InvestmentPlan model)
        {
            int result = 0;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                #region 更新投资规划
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();

                strSql.Append("update InvestmentPlan set ");
                strSql.Append("ProposalId=@ProposalId,");
                strSql.Append("LifeCycleId=@LifeCycleId,");
                strSql.Append("Analysis=@Analysis,");
                strSql.Append("HoldRate=@HoldRate,");
                strSql.Append("IncreaseRate=@IncreaseRate,");
                strSql.Append("SpeculationRate=@SpeculationRate");
                strSql.Append(" where Id=@Id ");

             
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@LifeCycleId", model.LifeCycleId, dbType: DbType.Int32);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@HoldRate", model.HoldRate, dbType: DbType.Decimal);
                param.Add("@IncreaseRate", model.IncreaseRate, dbType: DbType.Decimal);
                param.Add("@SpeculationRate", model.SpeculationRate, dbType: DbType.Decimal);
                result = conn.Execute(strSql.ToString(), param,tran);
                #endregion

                #region 更新投资规划产品

                if (model.InvestmentPlanProductList != null && model.InvestmentPlanProductList.Count > 0)
                {
                    strSql.Clear();
                    strSql.Append("delete from InvestmentPlanProduct ");
                    strSql.Append(" where ProposalId=@ProposalId ");
                    param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                    result = conn.Execute(strSql.ToString(), param, tran);

                    foreach (var item in model.InvestmentPlanProductList)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into InvestmentPlanProduct(");
                        strSql.Append("ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3,P2PName,InvestmentField,InvestmentCycle,StartAmount,EarningsRate)");

                        strSql.Append(" values (");
                        strSql.Append("@ProposalId,@PlanId,@PlanRate,@DemandDepositsBank,@DemandDepositsBankRate,@TimeDepositBank,@TimeDepositBankTime,@TimeDepositBankRate,@Fund1,@Fund2,@P2PProduct,@P2PProductRate,@Fund3,@TotalRate,@CashCode,@CashFund,@CashMarket,@YearlyEarningsRate1,@BondCode,@BondFund,@BondMarket,@YearlyEarningsRate2,@StockCode,@StockFund,@StockMarket,@YearlyEarningsRate3,@P2PName,@InvestmentField,@InvestmentCycle,@StartAmount,@EarningsRate)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProposalId", item.ProposalId, dbType: DbType.Int32);
                        param.Add("@PlanId", item.PlanId, dbType: DbType.Int32);
                        param.Add("@PlanRate", item.PlanRate, dbType: DbType.Decimal);
                        param.Add("@DemandDepositsBank", item.DemandDepositsBank, dbType: DbType.Int32);
                        param.Add("@DemandDepositsBankRate", item.DemandDepositsBankRate, dbType: DbType.Decimal);
                        param.Add("@TimeDepositBank", item.TimeDepositBank, dbType: DbType.Int32);
                        param.Add("@TimeDepositBankTime", item.TimeDepositBankTime, dbType: DbType.Int32);
                        param.Add("@TimeDepositBankRate", item.TimeDepositBankRate, dbType: DbType.Decimal);
                        param.Add("@Fund1", item.Fund1, dbType: DbType.Int32);
                        param.Add("@Fund2", item.Fund2, dbType: DbType.Int32);
                        param.Add("@P2PProduct", item.P2PProduct, dbType: DbType.Int32);
                        param.Add("@P2PProductRate", item.P2PProductRate, dbType: DbType.Decimal);
                        param.Add("@Fund3", item.Fund3, dbType: DbType.Int32);
                        param.Add("@TotalRate", item.TotalRate, dbType: DbType.Decimal);

                        param.Add("@CashCode", item.CashCode, dbType: DbType.String);
                        param.Add("@CashFund", item.CashFund, dbType: DbType.String);
                        param.Add("@CashMarket", item.CashMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate1", item.YearlyEarningsRate1, dbType: DbType.Decimal);
                        param.Add("@BondCode", item.BondCode, dbType: DbType.String);
                        param.Add("@BondFund", item.BondFund, dbType: DbType.String);
                        param.Add("@BondMarket", item.BondMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate2", item.YearlyEarningsRate2, dbType: DbType.Decimal);
                        param.Add("@StockCode", item.StockCode, dbType: DbType.String);
                        param.Add("@StockFund", item.StockFund, dbType: DbType.String);
                        param.Add("@StockMarket", item.StockMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate3", item.YearlyEarningsRate3, dbType: DbType.Decimal);

                        param.Add("@P2PName", item.P2PName, dbType: DbType.String);
                        param.Add("@InvestmentField", item.InvestmentField, dbType: DbType.String);
                        param.Add("@InvestmentCycle", item.InvestmentCycle, dbType: DbType.String);
                        param.Add("@StartAmount", item.StartAmount, dbType: DbType.String);
                        param.Add("@EarningsRate", item.EarningsRate, dbType: DbType.String);
               
                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }


                #endregion
                tran.Commit();
            }
            catch (Exception ex)
            {
                LogHelper.Log.WriteError("[回滚]修改案例出错", ex);
                tran.Rollback();
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                if (conn != null)
                    conn.Close();
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
            strSql.Append("delete from InvestmentPlan ");
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
        public InvestmentPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,LifeCycleId,Analysis,HoldRate,IncreaseRate,SpeculationRate from InvestmentPlan ");
            strSql.Append(" where Id=@Id ");

            InvestmentPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<InvestmentPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public InvestmentPlan GetModelByProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [InvestmentPlan] where ProposalId=@ProposalId; ");
            strSql.Append("select * from [InvestmentPlanProduct] where ProposalId=@ProposalId; ");

            InvestmentPlan model = new InvestmentPlan();
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                using (var multi = conn.QueryMultiple(strSql.ToString(), param))
                {
                    
                    model = multi.Read<InvestmentPlan>().FirstOrDefault();
                    if (model != null) {
                        model.InvestmentPlanProductList = multi.Read<InvestmentPlanProduct>().ToList();
                    }
                }
            }
            return model;
        }


        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<InvestmentPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,LifeCycleId,Analysis,HoldRate,IncreaseRate,SpeculationRate ");
            strSql.Append(" FROM InvestmentPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<InvestmentPlan> list = new List<InvestmentPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<InvestmentPlan>(strSql.ToString()).ToList();
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
            else if (filter.ProposalId.HasValue)
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
        public PageModel GetInvestmentPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "InvestmentPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,LifeCycleId,Analysis,HoldRate,IncreaseRate,SpeculationRate";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

