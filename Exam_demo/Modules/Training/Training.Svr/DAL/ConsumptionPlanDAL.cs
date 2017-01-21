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
    /// 数据访问类:ConsumptionPlan
    /// </summary>
    public partial class ConsumptionPlanDAL
    {
        public ConsumptionPlanDAL()
        {
        }

        #region  是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ConsumptionPlan where Id=@Id ");

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
        public int Add(ConsumptionPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ConsumptionPlan(");
            strSql.Append("ProposalId,HouseArea,HousePrice,HouseDownPaymentPercent,HouseLoanYear,HouseLoanRate,HouseDownPayment,HouseTotalAmount,HouseMonthlyAmount,CarType,CarPrice,CarDownPaymentPercent,CarLoanYear,CarLoanRate,PurchaseTax,CarRegFee,Displacement,VehicleAndVesselTax,MotorVehicleCompulsory,MotorVehicleCommercial,CarDownPayment,CarTotalAmount,CarMonthlyAmount,Analysis,FirstAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,ShopHouseYear,HouseAllMoney,ShopCarYear)");

            strSql.Append(" values (");
            strSql.Append("@ProposalId,@HouseArea,@HousePrice,@HouseDownPaymentPercent,@HouseLoanYear,@HouseLoanRate,@HouseDownPayment,@HouseTotalAmount,@HouseMonthlyAmount,@CarType,@CarPrice,@CarDownPaymentPercent,@CarLoanYear,@CarLoanRate,@PurchaseTax,@CarRegFee,@Displacement,@VehicleAndVesselTax,@MotorVehicleCompulsory,@MotorVehicleCommercial,@CarDownPayment,@CarTotalAmount,@CarMonthlyAmount,@Analysis,@FirstAmount,@ReturnOnInvestment,@DisposableInput,@MonthlyInvestment,@RegularYear,@TargetAmount,@ShopHouseYear,@HouseAllMoney,@ShopCarYear)");
            strSql.Append(";SELECT @returnid=SCOPE_IDENTITY()");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@HouseArea", model.HouseArea, dbType: DbType.Decimal);
                param.Add("@HousePrice", model.HousePrice, dbType: DbType.Decimal);
                param.Add("@HouseDownPaymentPercent", model.HouseDownPaymentPercent, dbType: DbType.Int32);
                param.Add("@HouseLoanYear", model.HouseLoanYear, dbType: DbType.Int32);
                param.Add("@HouseLoanRate", model.HouseLoanRate, dbType: DbType.Decimal);
                param.Add("@HouseDownPayment", model.HouseDownPayment, dbType: DbType.Decimal);
                param.Add("@HouseTotalAmount", model.HouseTotalAmount, dbType: DbType.Decimal);
                param.Add("@HouseMonthlyAmount", model.HouseMonthlyAmount, dbType: DbType.Decimal);
                param.Add("@CarType", model.CarType, dbType: DbType.String);
                param.Add("@CarPrice", model.CarPrice, dbType: DbType.Decimal);
                param.Add("@CarDownPaymentPercent", model.CarDownPaymentPercent, dbType: DbType.Int32);
                param.Add("@CarLoanYear", model.CarLoanYear, dbType: DbType.Int32);
                param.Add("@CarLoanRate", model.CarLoanRate, dbType: DbType.Decimal);
                param.Add("@PurchaseTax", model.PurchaseTax, dbType: DbType.Decimal);
                param.Add("@CarRegFee", model.CarRegFee, dbType: DbType.Decimal);
                param.Add("@Displacement", model.Displacement, dbType: DbType.Int32);
                param.Add("@VehicleAndVesselTax", model.VehicleAndVesselTax, dbType: DbType.Decimal);
                param.Add("@MotorVehicleCompulsory", model.MotorVehicleCompulsory, dbType: DbType.Decimal);
                param.Add("@MotorVehicleCommercial", model.MotorVehicleCommercial, dbType: DbType.Decimal);
                param.Add("@CarDownPayment", model.CarDownPayment, dbType: DbType.Decimal);
                param.Add("@CarTotalAmount", model.CarTotalAmount, dbType: DbType.Decimal);
                param.Add("@CarMonthlyAmount", model.CarMonthlyAmount, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@FirstAmount", model.FirstAmount, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestment", model.ReturnOnInvestment, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Decimal);
                param.Add("@TargetAmount", model.TargetAmount, dbType: DbType.Decimal);

                param.Add("@ShopHouseYear", model.ShopHouseYear, dbType: DbType.Int32);
                param.Add("@HouseAllMoney", model.HouseAllMoney, dbType: DbType.Decimal);
                param.Add("@ShopCarYear", model.ShopCarYear, dbType: DbType.Int32);

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
        public bool Update(ConsumptionPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ConsumptionPlan set ");
            strSql.Append("ProposalId=@ProposalId,");
            strSql.Append("HouseArea=@HouseArea,");
            strSql.Append("HousePrice=@HousePrice,");
            strSql.Append("HouseDownPaymentPercent=@HouseDownPaymentPercent,");
            strSql.Append("HouseLoanYear=@HouseLoanYear,");
            strSql.Append("HouseLoanRate=@HouseLoanRate,");
            strSql.Append("HouseDownPayment=@HouseDownPayment,");
            strSql.Append("HouseTotalAmount=@HouseTotalAmount,");
            strSql.Append("HouseMonthlyAmount=@HouseMonthlyAmount,");
            strSql.Append("CarType=@CarType,");
            strSql.Append("CarPrice=@CarPrice,");
            strSql.Append("CarDownPaymentPercent=@CarDownPaymentPercent,");
            strSql.Append("CarLoanYear=@CarLoanYear,");
            strSql.Append("CarLoanRate=@CarLoanRate,");
            strSql.Append("PurchaseTax=@PurchaseTax,");
            strSql.Append("CarRegFee=@CarRegFee,");
            strSql.Append("Displacement=@Displacement,");
            strSql.Append("VehicleAndVesselTax=@VehicleAndVesselTax,");
            strSql.Append("MotorVehicleCompulsory=@MotorVehicleCompulsory,");
            strSql.Append("MotorVehicleCommercial=@MotorVehicleCommercial,");
            strSql.Append("CarDownPayment=@CarDownPayment,");
            strSql.Append("CarTotalAmount=@CarTotalAmount,");
            strSql.Append("CarMonthlyAmount=@CarMonthlyAmount,");           
            strSql.Append("Analysis=@Analysis,");
            strSql.Append("FirstAmount=@FirstAmount,");
            strSql.Append("ReturnOnInvestment=@ReturnOnInvestment,");
            strSql.Append("DisposableInput=@DisposableInput,");
            strSql.Append("MonthlyInvestment=@MonthlyInvestment,");
            strSql.Append("RegularYear=@RegularYear,");
            strSql.Append("TargetAmount=@TargetAmount,");

            strSql.Append("ShopHouseYear=@ShopHouseYear,");
            strSql.Append("HouseAllMoney=@HouseAllMoney,");
            strSql.Append("ShopCarYear=@ShopCarYear");
            strSql.Append(" where Id=@Id ");

            int result = 0;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", model.Id, dbType: DbType.Int32);
                param.Add("@ProposalId", model.ProposalId, dbType: DbType.Int32);
                param.Add("@HouseArea", model.HouseArea, dbType: DbType.Decimal);
                param.Add("@HousePrice", model.HousePrice, dbType: DbType.Decimal);
                param.Add("@HouseDownPaymentPercent", model.HouseDownPaymentPercent, dbType: DbType.Decimal);
                param.Add("@HouseLoanYear", model.HouseLoanYear, dbType: DbType.Int32);
                param.Add("@HouseLoanRate", model.HouseLoanRate, dbType: DbType.Decimal);
                param.Add("@HouseDownPayment", model.HouseDownPayment, dbType: DbType.Decimal);
                param.Add("@HouseTotalAmount", model.HouseTotalAmount, dbType: DbType.Decimal);
                param.Add("@HouseMonthlyAmount", model.HouseMonthlyAmount, dbType: DbType.Decimal);
                param.Add("@CarType", model.CarType, dbType: DbType.String);
                param.Add("@CarPrice", model.CarPrice, dbType: DbType.Decimal);
                param.Add("@CarDownPaymentPercent", model.CarDownPaymentPercent, dbType: DbType.Decimal);
                param.Add("@CarLoanYear", model.CarLoanYear, dbType: DbType.Int32);
                param.Add("@CarLoanRate", model.CarLoanRate, dbType: DbType.Decimal);
                param.Add("@PurchaseTax", model.PurchaseTax, dbType: DbType.Decimal);
                param.Add("@CarRegFee", model.CarRegFee, dbType: DbType.Decimal);
                param.Add("@Displacement", model.Displacement, dbType: DbType.Int32);
                param.Add("@VehicleAndVesselTax", model.VehicleAndVesselTax, dbType: DbType.Decimal);
                param.Add("@MotorVehicleCompulsory", model.MotorVehicleCompulsory, dbType: DbType.Decimal);
                param.Add("@MotorVehicleCommercial", model.MotorVehicleCommercial, dbType: DbType.Decimal);
                param.Add("@CarDownPayment", model.CarDownPayment, dbType: DbType.Decimal);
                param.Add("@CarTotalAmount", model.CarTotalAmount, dbType: DbType.Decimal);
                param.Add("@CarMonthlyAmount", model.CarMonthlyAmount, dbType: DbType.Decimal);
                param.Add("@Analysis", model.Analysis, dbType: DbType.String);
                param.Add("@FirstAmount", model.FirstAmount, dbType: DbType.Decimal);
                param.Add("@ReturnOnInvestment", model.ReturnOnInvestment, dbType: DbType.Decimal);
                param.Add("@DisposableInput", model.DisposableInput, dbType: DbType.Decimal);
                param.Add("@MonthlyInvestment", model.MonthlyInvestment, dbType: DbType.Decimal);
                param.Add("@RegularYear", model.RegularYear, dbType: DbType.Decimal);
                param.Add("@TargetAmount", model.TargetAmount, dbType: DbType.Decimal);

                param.Add("@ShopHouseYear", model.ShopHouseYear, dbType: DbType.Int32);
                param.Add("@HouseAllMoney", model.HouseAllMoney, dbType: DbType.Decimal);
                param.Add("@ShopCarYear", model.ShopCarYear, dbType: DbType.Int32);
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
            strSql.Append("delete from ConsumptionPlan ");
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
        public ConsumptionPlan GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,HouseArea,HousePrice,HouseDownPaymentPercent,HouseLoanYear,HouseLoanRate,HouseDownPayment,HouseTotalAmount,HouseMonthlyAmount,CarType,CarPrice,CarDownPaymentPercent,CarLoanYear,CarLoanRate,PurchaseTax,CarRegFee,Displacement,VehicleAndVesselTax,MotorVehicleCompulsory,MotorVehicleCommercial,CarDownPayment,CarTotalAmount,CarMonthlyAmount,Analysis,FirstAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,ShopHouseYear,HouseAllMoney,ShopCarYear from ConsumptionPlan ");
            strSql.Append(" where Id=@Id ");

            ConsumptionPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@Id", Id, dbType: DbType.Int32);
                model = conn.Query<ConsumptionPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ConsumptionPlan GetModel2(int ProposalId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,HouseArea,HousePrice,HouseDownPaymentPercent,HouseLoanYear,HouseLoanRate,HouseDownPayment,HouseTotalAmount,HouseMonthlyAmount,CarType,CarPrice,CarDownPaymentPercent,CarLoanYear,CarLoanRate,PurchaseTax,CarRegFee,Displacement,VehicleAndVesselTax,MotorVehicleCompulsory,MotorVehicleCommercial,CarDownPayment,CarTotalAmount,CarMonthlyAmount,Analysis,FirstAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,ShopHouseYear,HouseAllMoney,ShopCarYear from ConsumptionPlan ");
            strSql.Append(" where ProposalId=@ProposalId ");

            ConsumptionPlan model = null;
            var param = new DynamicParameters();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                param.Add("@ProposalId", ProposalId, dbType: DbType.Int32);
                model = conn.Query<ConsumptionPlan>(strSql.ToString(), param).FirstOrDefault();
            }
            return model;
        }
        #endregion

        #region  根据查询条件获取列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ConsumptionPlan> GetList(CustomFilter filter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProposalId,HouseArea,HousePrice,HouseDownPaymentPercent,HouseLoanYear,HouseLoanRate,HouseDownPayment,HouseTotalAmount,HouseMonthlyAmount,CarType,CarPrice,CarDownPaymentPercent,CarLoanYear,CarLoanRate,PurchaseTax,CarRegFee,Displacement,VehicleAndVesselTax,MotorVehicleCompulsory,MotorVehicleCommercial,CarDownPayment,CarTotalAmount,CarMonthlyAmount,Analysis,FirstAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,ShopHouseYear,HouseAllMoney,ShopCarYear ");
            strSql.Append(" FROM ConsumptionPlan ");
            strSql.Append(" where 1=1 ");
            strSql.Append(GetStrWhere(filter));

            List<ConsumptionPlan> list = new List<ConsumptionPlan>();
            using (var conn = DBHelper.CreateConnection())
            {
                conn.Open();
                list = conn.Query<ConsumptionPlan>(strSql.ToString()).ToList();
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
        public PageModel GetConsumptionPlanPageParams(CustomFilter filter)
        {
            PageModel model = new PageModel();
            model.Tables = "ConsumptionPlan";
            model.PKey = "Id";
            model.Sort = "Id";
            model.Fields = "Id,ProposalId,HouseArea,HousePrice,HouseDownPaymentPercent,HouseLoanYear,HouseLoanRate,HouseDownPayment,HouseTotalAmount,HouseMonthlyAmount,CarType,CarPrice,CarDownPaymentPercent,CarLoanYear,CarLoanRate,PurchaseTax,CarRegFee,Displacement,VehicleAndVesselTax,MotorVehicleCompulsory,MotorVehicleCommercial,CarDownPayment,CarTotalAmount,CarMonthlyAmount,Analysis,FirstAmount,ReturnOnInvestment,DisposableInput,MonthlyInvestment,RegularYear,TargetAmount,ShopHouseYear,HouseAllMoney,ShopCarYear";
            model.Filter = GetStrWhere(filter);
            return model;
        }

        #endregion




    }
}

