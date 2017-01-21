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
    /// 数据访问类:Heritage
    /// </summary>
    public partial class HeritageDAL
    {
        public HeritageDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Heritage where Id=@Id ");

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
        public int Add(Heritage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Heritage(");
            strSql.Append("ProposalId,Cash,Deposit,LifeInsurance,OtherCashAccount,Stock,Bond,Fund,OtherInvestment,Pension,AnnuityRevenue,OtherPension,House,Car,Other,OtherProperty,TotalProperty,ShortTermLoan,MediumTermLoans,LongTermLoan,OtherLoan,MedicalCosts,TaxCosts,FuneralExpenses,HeritageCosts,OtherCosts,OtherLiabilities,TotalLiabilities,FinanceAnalysis,PlanTool,PlanAnalysis)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@Cash,@Deposit,@LifeInsurance,@OtherCashAccount,@Stock,@Bond,@Fund,@OtherInvestment,@Pension,@AnnuityRevenue,@OtherPension,@House,@Car,@Other,@OtherProperty,@TotalProperty,@ShortTermLoan,@MediumTermLoans,@LongTermLoan,@OtherLoan,@MedicalCosts,@TaxCosts,@FuneralExpenses,@HeritageCosts,@OtherCosts,@OtherLiabilities,@TotalLiabilities,@FinanceAnalysis,@PlanTool,@PlanAnalysis)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Cash", model.Cash, dbType: DbType.Decimal);
                param.Add("@Deposit", model.Deposit, dbType: DbType.Decimal);
                param.Add("@LifeInsurance", model.LifeInsurance, dbType: DbType.Decimal);
                param.Add("@OtherCashAccount", model.OtherCashAccount, dbType: DbType.Decimal);
                param.Add("@Stock", model.Stock, dbType: DbType.Decimal);
                param.Add("@Bond", model.Bond, dbType: DbType.Decimal);
                param.Add("@Fund", model.Fund, dbType: DbType.Decimal);
                param.Add("@OtherInvestment", model.OtherInvestment, dbType: DbType.Decimal);
                param.Add("@Pension", model.Pension, dbType: DbType.Decimal);
                param.Add("@AnnuityRevenue", model.AnnuityRevenue, dbType: DbType.Decimal);
                param.Add("@OtherPension", model.OtherPension, dbType: DbType.Decimal);
                param.Add("@House", model.House, dbType: DbType.Decimal);
                param.Add("@Car", model.Car, dbType: DbType.Decimal);
                param.Add("@Other", model.Other, dbType: DbType.Decimal);
                param.Add("@OtherProperty", model.OtherProperty, dbType: DbType.Decimal);
                param.Add("@TotalProperty", model.TotalProperty, dbType: DbType.Decimal);
                param.Add("@ShortTermLoan", model.ShortTermLoan, dbType: DbType.Decimal);
                param.Add("@MediumTermLoans", model.MediumTermLoans, dbType: DbType.Decimal);
                param.Add("@LongTermLoan", model.LongTermLoan, dbType: DbType.Decimal);
                param.Add("@OtherLoan", model.OtherLoan, dbType: DbType.Decimal);
                param.Add("@MedicalCosts", model.MedicalCosts, dbType: DbType.Decimal);
                param.Add("@TaxCosts", model.TaxCosts, dbType: DbType.Decimal);
                param.Add("@FuneralExpenses", model.FuneralExpenses, dbType: DbType.Decimal);
                param.Add("@HeritageCosts", model.HeritageCosts, dbType: DbType.Decimal);
                param.Add("@OtherCosts", model.OtherCosts, dbType: DbType.Decimal);
                param.Add("@OtherLiabilities", model.OtherLiabilities, dbType: DbType.Decimal);
                param.Add("@TotalLiabilities", model.TotalLiabilities, dbType: DbType.Decimal);
                param.Add("@FinanceAnalysis", model.FinanceAnalysis, dbType: DbType.String);
                param.Add("@PlanTool", model.PlanTool, dbType: DbType.Int32);
                param.Add("@PlanAnalysis", model.PlanAnalysis, dbType: DbType.String);
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
        public bool Update(Heritage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Heritage set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("Cash=@Cash,");
            strSql.Append("Deposit=@Deposit,");
            strSql.Append("LifeInsurance=@LifeInsurance,");
            strSql.Append("OtherCashAccount=@OtherCashAccount,");
            strSql.Append("Stock=@Stock,");
            strSql.Append("Bond=@Bond,");
            strSql.Append("Fund=@Fund,");
            strSql.Append("OtherInvestment=@OtherInvestment,");
            strSql.Append("Pension=@Pension,");
            strSql.Append("AnnuityRevenue=@AnnuityRevenue,");
            strSql.Append("OtherPension=@OtherPension,");
            strSql.Append("House=@House,");
            strSql.Append("Car=@Car,");
            strSql.Append("Other=@Other,");
            strSql.Append("OtherProperty=@OtherProperty,");
            strSql.Append("TotalProperty=@TotalProperty,");
            strSql.Append("ShortTermLoan=@ShortTermLoan,");
            strSql.Append("MediumTermLoans=@MediumTermLoans,");
            strSql.Append("LongTermLoan=@LongTermLoan,");
            strSql.Append("OtherLoan=@OtherLoan,");
            strSql.Append("MedicalCosts=@MedicalCosts,");
            strSql.Append("TaxCosts=@TaxCosts,");
            strSql.Append("FuneralExpenses=@FuneralExpenses,");
            strSql.Append("HeritageCosts=@HeritageCosts,");
            strSql.Append("OtherCosts=@OtherCosts,");
            strSql.Append("OtherLiabilities=@OtherLiabilities,");
            strSql.Append("TotalLiabilities=@TotalLiabilities,");
            strSql.Append("FinanceAnalysis=@FinanceAnalysis,");
            strSql.Append("PlanTool=@PlanTool,");
            strSql.Append("PlanAnalysis=@PlanAnalysis");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@Cash", model.Cash, dbType: DbType.Decimal);
                param.Add("@Deposit", model.Deposit, dbType: DbType.Decimal);
                param.Add("@LifeInsurance", model.LifeInsurance, dbType: DbType.Decimal);
                param.Add("@OtherCashAccount", model.OtherCashAccount, dbType: DbType.Decimal);
                param.Add("@Stock", model.Stock, dbType: DbType.Decimal);
                param.Add("@Bond", model.Bond, dbType: DbType.Decimal);
                param.Add("@Fund", model.Fund, dbType: DbType.Decimal);
                param.Add("@OtherInvestment", model.OtherInvestment, dbType: DbType.Decimal);
                param.Add("@Pension", model.Pension, dbType: DbType.Decimal);
                param.Add("@AnnuityRevenue", model.AnnuityRevenue, dbType: DbType.Decimal);
                param.Add("@OtherPension", model.OtherPension, dbType: DbType.Decimal);
                param.Add("@House", model.House, dbType: DbType.Decimal);
                param.Add("@Car", model.Car, dbType: DbType.Decimal);
                param.Add("@Other", model.Other, dbType: DbType.Decimal);
                param.Add("@OtherProperty", model.OtherProperty, dbType: DbType.Decimal);
                param.Add("@TotalProperty", model.TotalProperty, dbType: DbType.Decimal);
                param.Add("@ShortTermLoan", model.ShortTermLoan, dbType: DbType.Decimal);
                param.Add("@MediumTermLoans", model.MediumTermLoans, dbType: DbType.Decimal);
                param.Add("@LongTermLoan", model.LongTermLoan, dbType: DbType.Decimal);
                param.Add("@OtherLoan", model.OtherLoan, dbType: DbType.Decimal);
                param.Add("@MedicalCosts", model.MedicalCosts, dbType: DbType.Decimal);
                param.Add("@TaxCosts", model.TaxCosts, dbType: DbType.Decimal);
                param.Add("@FuneralExpenses", model.FuneralExpenses, dbType: DbType.Decimal);
                param.Add("@HeritageCosts", model.HeritageCosts, dbType: DbType.Decimal);
                param.Add("@OtherCosts", model.OtherCosts, dbType: DbType.Decimal);
                param.Add("@OtherLiabilities", model.OtherLiabilities, dbType: DbType.Decimal);
                param.Add("@TotalLiabilities", model.TotalLiabilities, dbType: DbType.Decimal);
                param.Add("@FinanceAnalysis", model.FinanceAnalysis, dbType: DbType.String);
                param.Add("@PlanTool", model.PlanTool, dbType: DbType.Int32);
                param.Add("@PlanAnalysis", model.PlanAnalysis, dbType: DbType.String);
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
            strSql.Append("delete from Heritage ");
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
        public Heritage GetModel(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Cash,Deposit,LifeInsurance,OtherCashAccount,Stock,Bond,Fund,OtherInvestment,Pension,AnnuityRevenue,OtherPension,House,Car,Other,OtherProperty,TotalProperty,ShortTermLoan,MediumTermLoans,LongTermLoan,OtherLoan,MedicalCosts,TaxCosts,FuneralExpenses,HeritageCosts,OtherCosts,OtherLiabilities,TotalLiabilities,FinanceAnalysis,PlanTool,PlanAnalysis from Heritage ");
            strSql.Append(" where ProposalId=@ProposalId ");

            Heritage model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<Heritage>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Heritage> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,Cash,Deposit,LifeInsurance,OtherCashAccount,Stock,Bond,Fund,OtherInvestment,Pension,AnnuityRevenue,OtherPension,House,Car,Other,OtherProperty,TotalProperty,ShortTermLoan,MediumTermLoans,LongTermLoan,OtherLoan,MedicalCosts,TaxCosts,FuneralExpenses,HeritageCosts,OtherCosts,OtherLiabilities,TotalLiabilities,FinanceAnalysis,PlanTool,PlanAnalysis ");
            strSql.Append(" FROM Heritage ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<Heritage> list = new List<Heritage>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<Heritage>(strSql.ToString()).ToList();
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
        public PageModel GetHeritagePageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "Heritage";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,Cash,Deposit,LifeInsurance,OtherCashAccount,Stock,Bond,Fund,OtherInvestment,Pension,AnnuityRevenue,OtherPension,House,Car,Other,OtherProperty,TotalProperty,ShortTermLoan,MediumTermLoans,LongTermLoan,OtherLoan,MedicalCosts,TaxCosts,FuneralExpenses,HeritageCosts,OtherCosts,OtherLiabilities,TotalLiabilities,FinanceAnalysis,PlanTool,PlanAnalysis";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

