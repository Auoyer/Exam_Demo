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
    /// 数据访问类:Liability
    /// </summary>
    public partial class LiabilityDAL
    {
        public LiabilityDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Liability where Id=@Id ");

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
        public int Add(Liability model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Liability(");
            strSql.Append("ProposalId,Cash,RMBDeposit,OtherAsset,RMBFixedDeposit,ForeignCurrencyFixedDeposit,StockInvestment,BondInvestment,FundInvestment,IndustryInvestment,EstateInvestment,PolicyInvestment,OtherInvestment,Estate,Car,Others,TotalAssets,CreditCard,Microfinance,OtherLoan,FinancialLoan,IndustryInvestmentLoan,EstateInvestmentLoan,OtherInvestmentLoan,EstateLoan,CarLoan,OthersLoan,TotalLoan)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Cash,@RMBDeposit,@OtherAsset,@RMBFixedDeposit,@ForeignCurrencyFixedDeposit,@StockInvestment,@BondInvestment,@FundInvestment,@IndustryInvestment,@EstateInvestment,@PolicyInvestment,@OtherInvestment,@Estate,@Car,@Others,@TotalAssets,@CreditCard,@Microfinance,@OtherLoan,@FinancialLoan,@IndustryInvestmentLoan,@EstateInvestmentLoan,@OtherInvestmentLoan,@EstateLoan,@CarLoan,@OthersLoan,@TotalLoan)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Cash", model.Cash, dbType: DbType.Decimal);
                param.Add("@RMBDeposit", model.RMBDeposit, dbType: DbType.Decimal);
                param.Add("@OtherAsset", model.OtherAsset, dbType: DbType.Decimal);
                param.Add("@RMBFixedDeposit", model.RMBFixedDeposit, dbType: DbType.Decimal);
                param.Add("@ForeignCurrencyFixedDeposit", model.ForeignCurrencyFixedDeposit, dbType: DbType.Decimal);
                param.Add("@StockInvestment", model.StockInvestment, dbType: DbType.Decimal);
                param.Add("@BondInvestment", model.BondInvestment, dbType: DbType.Decimal);
                param.Add("@FundInvestment", model.FundInvestment, dbType: DbType.Decimal);
                param.Add("@IndustryInvestment", model.IndustryInvestment, dbType: DbType.Decimal);
                param.Add("@EstateInvestment", model.EstateInvestment, dbType: DbType.Decimal);
                param.Add("@PolicyInvestment", model.PolicyInvestment, dbType: DbType.Decimal);
                param.Add("@OtherInvestment", model.OtherInvestment, dbType: DbType.Decimal);
                param.Add("@Estate", model.Estate, dbType: DbType.Decimal);
                param.Add("@Car", model.Car, dbType: DbType.Decimal);
                param.Add("@Others", model.Others, dbType: DbType.Decimal);
                param.Add("@TotalAssets", model.TotalAssets, dbType: DbType.Decimal);
                param.Add("@CreditCard", model.CreditCard, dbType: DbType.Decimal);
                param.Add("@Microfinance", model.Microfinance, dbType: DbType.Decimal);
                param.Add("@OtherLoan", model.OtherLoan, dbType: DbType.Decimal);
                param.Add("@FinancialLoan", model.FinancialLoan, dbType: DbType.Decimal);
                param.Add("@IndustryInvestmentLoan", model.IndustryInvestmentLoan, dbType: DbType.Decimal);
                param.Add("@EstateInvestmentLoan", model.EstateInvestmentLoan, dbType: DbType.Decimal);
                param.Add("@OtherInvestmentLoan", model.OtherInvestmentLoan, dbType: DbType.Decimal);
                param.Add("@EstateLoan", model.EstateLoan, dbType: DbType.Decimal);
                param.Add("@CarLoan", model.CarLoan, dbType: DbType.Decimal);
                param.Add("@OthersLoan", model.OthersLoan, dbType: DbType.Decimal);
                param.Add("@TotalLoan", model.TotalLoan, dbType: DbType.Decimal);
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
        public bool Update(Liability model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Liability set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Cash=@Cash,");
            strSql.Append("RMBDeposit=@RMBDeposit,");
            strSql.Append("OtherAsset=@OtherAsset,");
            strSql.Append("RMBFixedDeposit=@RMBFixedDeposit,");
            strSql.Append("ForeignCurrencyFixedDeposit=@ForeignCurrencyFixedDeposit,");
            strSql.Append("StockInvestment=@StockInvestment,");
            strSql.Append("BondInvestment=@BondInvestment,");
            strSql.Append("FundInvestment=@FundInvestment,");
            strSql.Append("IndustryInvestment=@IndustryInvestment,");
            strSql.Append("EstateInvestment=@EstateInvestment,");
            strSql.Append("PolicyInvestment=@PolicyInvestment,");
            strSql.Append("OtherInvestment=@OtherInvestment,");
            strSql.Append("Estate=@Estate,");
            strSql.Append("Car=@Car,");
            strSql.Append("Others=@Others,");
            strSql.Append("TotalAssets=@TotalAssets,");
            strSql.Append("CreditCard=@CreditCard,");
            strSql.Append("Microfinance=@Microfinance,");
            strSql.Append("OtherLoan=@OtherLoan,");
            strSql.Append("FinancialLoan=@FinancialLoan,");
            strSql.Append("IndustryInvestmentLoan=@IndustryInvestmentLoan,");
            strSql.Append("EstateInvestmentLoan=@EstateInvestmentLoan,");
            strSql.Append("OtherInvestmentLoan=@OtherInvestmentLoan,");
            strSql.Append("EstateLoan=@EstateLoan,");
            strSql.Append("CarLoan=@CarLoan,");
            strSql.Append("OthersLoan=@OthersLoan,");
            strSql.Append("TotalLoan=@TotalLoan");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Cash", model.Cash, dbType: DbType.Decimal);
                param.Add("@RMBDeposit", model.RMBDeposit, dbType: DbType.Decimal);
                param.Add("@OtherAsset", model.OtherAsset, dbType: DbType.Decimal);
                param.Add("@RMBFixedDeposit", model.RMBFixedDeposit, dbType: DbType.Decimal);
                param.Add("@ForeignCurrencyFixedDeposit", model.ForeignCurrencyFixedDeposit, dbType: DbType.Decimal);
                param.Add("@StockInvestment", model.StockInvestment, dbType: DbType.Decimal);
                param.Add("@BondInvestment", model.BondInvestment, dbType: DbType.Decimal);
                param.Add("@FundInvestment", model.FundInvestment, dbType: DbType.Decimal);
                param.Add("@IndustryInvestment", model.IndustryInvestment, dbType: DbType.Decimal);
                param.Add("@EstateInvestment", model.EstateInvestment, dbType: DbType.Decimal);
                param.Add("@PolicyInvestment", model.PolicyInvestment, dbType: DbType.Decimal);
                param.Add("@OtherInvestment", model.OtherInvestment, dbType: DbType.Decimal);
                param.Add("@Estate", model.Estate, dbType: DbType.Decimal);
                param.Add("@Car", model.Car, dbType: DbType.Decimal);
                param.Add("@Others", model.Others, dbType: DbType.Decimal);
                param.Add("@TotalAssets", model.TotalAssets, dbType: DbType.Decimal);
                param.Add("@CreditCard", model.CreditCard, dbType: DbType.Decimal);
                param.Add("@Microfinance", model.Microfinance, dbType: DbType.Decimal);
                param.Add("@OtherLoan", model.OtherLoan, dbType: DbType.Decimal);
                param.Add("@FinancialLoan", model.FinancialLoan, dbType: DbType.Decimal);
                param.Add("@IndustryInvestmentLoan", model.IndustryInvestmentLoan, dbType: DbType.Decimal);
                param.Add("@EstateInvestmentLoan", model.EstateInvestmentLoan, dbType: DbType.Decimal);
                param.Add("@OtherInvestmentLoan", model.OtherInvestmentLoan, dbType: DbType.Decimal);
                param.Add("@EstateLoan", model.EstateLoan, dbType: DbType.Decimal);
                param.Add("@CarLoan", model.CarLoan, dbType: DbType.Decimal);
                param.Add("@OthersLoan", model.OthersLoan, dbType: DbType.Decimal);
                param.Add("@TotalLoan", model.TotalLoan, dbType: DbType.Decimal);
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
            strSql.Append("delete from Liability ");
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
        public Liability GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Cash,RMBDeposit,OtherAsset,RMBFixedDeposit,ForeignCurrencyFixedDeposit,StockInvestment,BondInvestment,FundInvestment,IndustryInvestment,EstateInvestment,PolicyInvestment,OtherInvestment,Estate,Car,Others,TotalAssets,CreditCard,Microfinance,OtherLoan,FinancialLoan,IndustryInvestmentLoan,EstateInvestmentLoan,OtherInvestmentLoan,EstateLoan,CarLoan,OthersLoan,TotalLoan from Liability ");
            strSql.Append(" where Id=@Id ");

            Liability model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<Liability>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 根据建议号ID去获取一个实体
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public Liability GetModelByProposalId(int ProposalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Cash,RMBDeposit,OtherAsset,RMBFixedDeposit,ForeignCurrencyFixedDeposit,StockInvestment,BondInvestment,FundInvestment,IndustryInvestment,EstateInvestment,PolicyInvestment,OtherInvestment,Estate,Car,Others,TotalAssets,CreditCard,Microfinance,OtherLoan,FinancialLoan,IndustryInvestmentLoan,EstateInvestmentLoan,OtherInvestmentLoan,EstateLoan,CarLoan,OthersLoan,TotalLoan from Liability ");
            strSql.Append(" where ProposalId=@ProposalId ");
            Liability model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<Liability>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Liability> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Cash,RMBDeposit,OtherAsset,RMBFixedDeposit,ForeignCurrencyFixedDeposit,StockInvestment,BondInvestment,FundInvestment,IndustryInvestment,EstateInvestment,PolicyInvestment,OtherInvestment,Estate,Car,Others,TotalAssets,CreditCard,Microfinance,OtherLoan,FinancialLoan,IndustryInvestmentLoan,EstateInvestmentLoan,OtherInvestmentLoan,EstateLoan,CarLoan,OthersLoan,TotalLoan ");
            strSql.Append(" FROM Liability ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Liability> list = new List<Liability>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Liability>(strSql.ToString()).ToList();
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
        public PageModel GetLiabilityPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Liability";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Cash,RMBDeposit,OtherAsset,RMBFixedDeposit,ForeignCurrencyFixedDeposit,StockInvestment,BondInvestment,FundInvestment,IndustryInvestment,EstateInvestment,PolicyInvestment,OtherInvestment,Estate,Car,Others,TotalAssets,CreditCard,Microfinance,OtherLoan,FinancialLoan,IndustryInvestmentLoan,EstateInvestmentLoan,OtherInvestmentLoan,EstateLoan,CarLoan,OthersLoan,TotalLoan";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

