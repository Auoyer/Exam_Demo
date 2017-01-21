using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ConsumptionPlan
    /// </summary>
    public class ConsumptionPlanVM
    {
        public ConsumptionPlanVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        public int ProposalId { get; set; }

        /// <summary>
        /// 面积
        /// </summary>		
        public decimal? HouseArea { get; set; }

        /// <summary>
        /// 单价
        /// </summary>		
        public decimal? HousePrice { get; set; }

        /// <summary>
        /// 首付比例
        /// </summary>		
        public decimal HouseDownPaymentPercent { get; set; }

        /// <summary>
        /// 贷款年限
        /// </summary>		
        public int HouseLoanYear { get; set; }

        /// <summary>
        /// 贷款利率
        /// </summary>		
        public decimal HouseLoanRate { get; set; }

        /// <summary>
        /// 首付款
        /// </summary>		
        public decimal HouseDownPayment { get; set; }

        /// <summary>
        /// 购房总花费
        /// </summary>		
        public decimal HouseTotalAmount { get; set; }

        /// <summary>
        /// 购房月供
        /// </summary>		
        public decimal HouseMonthlyAmount { get; set; }

        /// <summary>
        /// 车款型号
        /// </summary>		
        public string CarType { get; set; }

        /// <summary>
        /// 裸车价格
        /// </summary>		
        public decimal CarPrice { get; set; }

        /// <summary>
        /// 首付比例
        /// </summary>		
        public decimal CarDownPaymentPercent { get; set; }

        /// <summary>
        /// 贷款期限
        /// </summary>		
        public int CarLoanYear { get; set; }

        /// <summary>
        /// 贷款利率
        /// </summary>		
        public decimal CarLoanRate { get; set; }

        /// <summary>
        /// 购置税
        /// </summary>		
        public decimal PurchaseTax { get; set; }

        /// <summary>
        /// 上牌费用
        /// </summary>		
        public decimal CarRegFee { get; set; }

        /// <summary>
        /// 汽车排量
        /// </summary>		
        public int Displacement { get; set; }

        /// <summary>
        /// 车船使用税
        /// </summary>		
        public decimal VehicleAndVesselTax { get; set; }

        /// <summary>
        /// 交强险
        /// </summary>		
        public decimal MotorVehicleCompulsory { get; set; }

        /// <summary>
        /// 商业保险
        /// </summary>		
        public decimal MotorVehicleCommercial { get; set; }

        /// <summary>
        /// 首付款
        /// </summary>		
        public decimal CarDownPayment { get; set; }

        /// <summary>
        /// 购车总花费
        /// </summary>		
        public decimal CarTotalAmount { get; set; }

        /// <summary>
        /// 购车月供
        /// </summary>		
        public decimal CarMonthlyAmount { get; set; }

        /// <summary>
        /// 消费规划分析
        /// </summary>		
        public string Analysis { get; set; }

        /// <summary>
        /// 首付款准备的现金总金额
        /// </summary>		
        public decimal FirstAmount { get; set; }

        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        public decimal ReturnOnInvestment { get; set; }

        /// <summary>
        /// 一次性投入金额
        /// </summary>		
        public decimal DisposableInput { get; set; }

        /// <summary>
        /// 每月定期投资金额
        /// </summary>		
        public decimal MonthlyInvestment { get; set; }

        /// <summary>
        /// 定期定额投资年限
        /// </summary>		
        public decimal RegularYear { get; set; }

        /// <summary>
        /// 此方案能实现的目标金额
        /// </summary>		
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// 购房年限
        /// </summary>		
        public decimal ShopHouseYear { get; set; }

        /// <summary>
        /// 购房总金额
        /// </summary>		
        public decimal HouseAllMoney { get; set; }

        /// <summary>
        /// 购车年限
        /// </summary>		
        public decimal ShopCarYear { get; set; }
    }
}