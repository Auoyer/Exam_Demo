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
    /// 数据访问类:InvestmentPlanProduct
    /// </summary>
    public partial class InvestmentPlanProductDAL
    {
        public InvestmentPlanProductDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from InvestmentPlanProduct where Id=@Id ");

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
        public int Add(InvestmentPlanProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into InvestmentPlanProduct(");
            strSql.Append("ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@PlanId,@PlanRate,@DemandDepositsBank,@DemandDepositsBankRate,@TimeDepositBank,@TimeDepositBankTime,@TimeDepositBankRate,@Fund1,@Fund2,@P2PProduct,@P2PProductRate,@Fund3,@TotalRate,@CashCode,@CashFund,@CashMarket,@YearlyEarningsRate1,@BondCode,@BondFund,@BondMarket,@YearlyEarningsRate2,@StockCode,@StockFund,@StockMarket,@YearlyEarningsRate3)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@PlanId", model.PlanId, dbType: DbType.Int32);
                param.Add("@PlanRate", model.PlanRate, dbType: DbType.Decimal);
                param.Add("@DemandDepositsBank", model.DemandDepositsBank, dbType: DbType.Int32);
                param.Add("@DemandDepositsBankRate", model.DemandDepositsBankRate, dbType: DbType.Decimal);
                param.Add("@TimeDepositBank", model.TimeDepositBank, dbType: DbType.Int32);
                param.Add("@TimeDepositBankTime", model.TimeDepositBankTime, dbType: DbType.Int32);
                param.Add("@TimeDepositBankRate", model.TimeDepositBankRate, dbType: DbType.Decimal);
                param.Add("@Fund1", model.Fund1, dbType: DbType.Int32);
                param.Add("@Fund2", model.Fund2, dbType: DbType.Int32);
                param.Add("@P2PProduct", model.P2PProduct, dbType: DbType.Int32);
                param.Add("@P2PProductRate", model.P2PProductRate, dbType: DbType.Decimal);
                param.Add("@Fund3", model.Fund3, dbType: DbType.Int32);
                param.Add("@TotalRate", model.TotalRate, dbType: DbType.Decimal);

                param.Add("@CashCode", model.CashCode, dbType: DbType.String);
                param.Add("@CashFund", model.CashFund, dbType: DbType.String);
                param.Add("@CashMarket", model.CashMarket, dbType: DbType.String);
                param.Add("@YearlyEarningsRate1", model.YearlyEarningsRate1, dbType: DbType.Decimal);
                param.Add("@BondCode", model.BondCode, dbType: DbType.String);
                param.Add("@BondFund", model.BondFund, dbType: DbType.String);
                param.Add("@BondMarket", model.BondMarket, dbType: DbType.String);
                param.Add("@YearlyEarningsRate2", model.YearlyEarningsRate2, dbType: DbType.Decimal);
                param.Add("@StockCode", model.StockCode, dbType: DbType.String);
                param.Add("@StockFund", model.StockFund, dbType: DbType.String);
                param.Add("@StockMarket", model.StockMarket, dbType: DbType.String);
                param.Add("@YearlyEarningsRate3", model.YearlyEarningsRate3, dbType: DbType.Decimal);

                param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(strSql.ToString(), param);
                result = param.Get<int>("@returnid");
            }
            return result;
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool AddList(List<InvestmentPlanProduct> list)
        {
            bool tag = false;
            var conn = DBHelper.CreateConnection();
            conn.Open();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                var param = new DynamicParameters();
                if (list.Count > 0 && list != null)
                {
                    foreach (var model in list)
                    {
                        strSql.Clear();
                        param = new DynamicParameters();

                        strSql.Append("insert into InvestmentPlanProduct(");
                        strSql.Append("ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3)");

                        strSql.Append(" values (");
                        strSql.Append("@ProposalId,@PlanId,@PlanRate,@DemandDepositsBank,@DemandDepositsBankRate,@TimeDepositBank,@TimeDepositBankTime,@TimeDepositBankRate,@Fund1,@Fund2,@P2PProduct,@P2PProductRate,@Fund3,@TotalRate,@CashCode,@CashFund,@CashMarket,@YearlyEarningsRate1,@BondCode,@BondFund,@BondMarket,@YearlyEarningsRate2,@StockCode,@StockFund,@StockMarket,@YearlyEarningsRate3)");
                        strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

                        param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                        param.Add("@PlanId", model.PlanId, dbType: DbType.Int32);
                        param.Add("@PlanRate", model.PlanRate, dbType: DbType.Decimal);
                        param.Add("@DemandDepositsBank", model.DemandDepositsBank, dbType: DbType.Int32);
                        param.Add("@DemandDepositsBankRate", model.DemandDepositsBankRate, dbType: DbType.Decimal);
                        param.Add("@TimeDepositBank", model.TimeDepositBank, dbType: DbType.Int32);
                        param.Add("@TimeDepositBankTime", model.TimeDepositBankTime, dbType: DbType.Int32);
                        param.Add("@TimeDepositBankRate", model.TimeDepositBankRate, dbType: DbType.Decimal);
                        param.Add("@Fund1", model.Fund1, dbType: DbType.Int32);
                        param.Add("@Fund2", model.Fund2, dbType: DbType.Int32);
                        param.Add("@P2PProduct", model.P2PProduct, dbType: DbType.Int32);
                        param.Add("@P2PProductRate", model.P2PProductRate, dbType: DbType.Decimal);
                        param.Add("@Fund3", model.Fund3, dbType: DbType.Int32);
                        param.Add("@TotalRate", model.TotalRate, dbType: DbType.Decimal);

                        param.Add("@CashCode", model.CashCode, dbType: DbType.String);
                        param.Add("@CashFund", model.CashFund, dbType: DbType.String);
                        param.Add("@CashMarket", model.CashMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate1", model.YearlyEarningsRate1, dbType: DbType.Decimal);
                        param.Add("@BondCode", model.BondCode, dbType: DbType.String);
                        param.Add("@BondFund", model.BondFund, dbType: DbType.String);
                        param.Add("@BondMarket", model.BondMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate2", model.YearlyEarningsRate2, dbType: DbType.Decimal);
                        param.Add("@StockCode", model.StockCode, dbType: DbType.String);
                        param.Add("@StockFund", model.StockFund, dbType: DbType.String);
                        param.Add("@StockMarket", model.StockMarket, dbType: DbType.String);
                        param.Add("@YearlyEarningsRate3", model.YearlyEarningsRate3, dbType: DbType.Decimal);

                        param.Add("@returnid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        conn.Execute(strSql.ToString(), param);

                        conn.Execute(strSql.ToString(), param, tran);
                    }
                }
                tran.Commit();
            }
            catch(Exception ex)
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
            return tag;
          

        }
        #endregion

        #region  更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(InvestmentPlanProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update InvestmentPlanProduct set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("PlanId=@PlanId,");
            strSql.Append("PlanRate=@PlanRate,");
            strSql.Append("DemandDepositsBank=@DemandDepositsBank,");
            strSql.Append("DemandDepositsBankRate=@DemandDepositsBankRate,");
            strSql.Append("TimeDepositBank=@TimeDepositBank,");
            strSql.Append("TimeDepositBankTime=@TimeDepositBankTime,");
            strSql.Append("TimeDepositBankRate=@TimeDepositBankRate,");
            strSql.Append("Fund1=@Fund1,");
            strSql.Append("Fund2=@Fund2,");
            strSql.Append("P2PProduct=@P2PProduct,");
            strSql.Append("P2PProductRate=@P2PProductRate,");
            strSql.Append("Fund3=@Fund3,");
            strSql.Append("TotalRate=@TotalRate");

            strSql.Append("CashCode=@P2PProduct,");
            strSql.Append("CashFund=@P2PProductRate,");
            strSql.Append("CashMarket=@Fund3,");
            strSql.Append("YearlyEarningsRate1=@TotalRate");
            strSql.Append("BondCode=@P2PProduct,");
            strSql.Append("BondFund=@P2PProductRate,");
            strSql.Append("BondMarket=@Fund3,");
            strSql.Append("YearlyEarningsRate2=@TotalRate");
            strSql.Append("StockCode=@P2PProduct,");
            strSql.Append("StockFund=@P2PProductRate,");
            strSql.Append("StockMarket=@Fund3,");
            strSql.Append("YearlyEarningsRate3=@TotalRate");

            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@PlanId", model.PlanId, dbType: DbType.Int32);
                param.Add("@PlanRate", model.PlanRate, dbType: DbType.Decimal);
                param.Add("@DemandDepositsBank", model.DemandDepositsBank, dbType: DbType.Int32);
                param.Add("@DemandDepositsBankRate", model.DemandDepositsBankRate, dbType: DbType.Decimal);
                param.Add("@TimeDepositBank", model.TimeDepositBank, dbType: DbType.Int32);
                param.Add("@TimeDepositBankTime", model.TimeDepositBankTime, dbType: DbType.Int32);
                param.Add("@TimeDepositBankRate", model.TimeDepositBankRate, dbType: DbType.Decimal);
                param.Add("@Fund1", model.Fund1, dbType: DbType.Int32);
                param.Add("@Fund2", model.Fund2, dbType: DbType.Int32);
                param.Add("@P2PProduct", model.P2PProduct, dbType: DbType.Int32);
                param.Add("@P2PProductRate", model.P2PProductRate, dbType: DbType.Decimal);
                param.Add("@Fund3", model.Fund3, dbType: DbType.Int32);
                param.Add("@TotalRate", model.TotalRate, dbType: DbType.Decimal);

                param.Add("@CashCode", model.CashCode, dbType: DbType.String);
                param.Add("@CashFund", model.CashFund, dbType: DbType.String);
                param.Add("@CashMarket", model.CashMarket, dbType: DbType.String);
                param.Add("@YearlyEarningsRate1", model.YearlyEarningsRate1, dbType: DbType.Decimal);
                param.Add("@BondCode", model.BondCode, dbType: DbType.String);
                param.Add("@BondFund", model.BondFund, dbType: DbType.String);
                param.Add("@BondMarket", model.BondMarket, dbType: DbType.String);
                param.Add("@YearlyEarningsRate2", model.YearlyEarningsRate2, dbType: DbType.Decimal);
                param.Add("@StockCode", model.StockCode, dbType: DbType.String);
                param.Add("@StockFund", model.StockFund, dbType: DbType.String);
                param.Add("@StockMarket", model.StockMarket, dbType: DbType.String);
                param.Add("@YearlyEarningsRate3", model.YearlyEarningsRate3, dbType: DbType.Decimal);

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
            strSql.Append("delete from InvestmentPlanProduct ");
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
        public InvestmentPlanProduct GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3 from InvestmentPlanProduct ");
            strSql.Append(" where Id=@Id ");

            InvestmentPlanProduct model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<InvestmentPlanProduct>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到多个对象实体
        /// </summary>
        public List<InvestmentPlanProduct> GetModelByProposalId(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3 from InvestmentPlanProduct ");
            strSql.Append(" where ProposalId=@ProposalId ");

           List<InvestmentPlanProduct> model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<InvestmentPlanProduct>(strSql.ToString(), param).ToList();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<InvestmentPlanProduct> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3 ");
            strSql.Append(" FROM InvestmentPlanProduct ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<InvestmentPlanProduct> list = new List<InvestmentPlanProduct>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<InvestmentPlanProduct>(strSql.ToString()).ToList();
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
        public PageModel GetInvestmentPlanProductPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "InvestmentPlanProduct";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,PlanId,PlanRate,DemandDepositsBank,DemandDepositsBankRate,TimeDepositBank,TimeDepositBankTime,TimeDepositBankRate,Fund1,Fund2,P2PProduct,P2PProductRate,Fund3,TotalRate,CashCode,CashFund,CashMarket,YearlyEarningsRate1,BondCode,BondFund,BondMarket,YearlyEarningsRate2,StockCode,StockFund,StockMarket,YearlyEarningsRate3";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

