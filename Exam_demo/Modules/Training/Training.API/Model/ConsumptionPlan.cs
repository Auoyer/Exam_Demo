using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ConsumptionPlan
    /// </summary>
    [DataContract]
    public class ConsumptionPlan
    {
        public ConsumptionPlan()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        [DataMember]
        public int ProposalId { get; set; }

        /// <summary>
        /// 面积
        /// </summary>		
        [DataMember]
        public decimal? HouseArea { get; set; }

        /// <summary>
        /// 单价
        /// </summary>		
        [DataMember]
        public decimal? HousePrice { get; set; }

        /// <summary>
        /// 首付比例
        /// </summary>		
        [DataMember]
        public decimal HouseDownPaymentPercent { get; set; }

        /// <summary>
        /// 贷款年限
        /// </summary>		
        [DataMember]
        public int HouseLoanYear { get; set; }

        /// <summary>
        /// 贷款利率
        /// </summary>		
        [DataMember]
        public decimal HouseLoanRate { get; set; }

        /// <summary>
        /// 首付款
        /// </summary>		
        [DataMember]
        public decimal HouseDownPayment { get; set; }

        /// <summary>
        /// 购房总花费
        /// </summary>		
        [DataMember]
        public decimal HouseTotalAmount { get; set; }

        /// <summary>
        /// 购房月供
        /// </summary>		
        [DataMember]
        public decimal HouseMonthlyAmount { get; set; }

        /// <summary>
        /// 车款型号
        /// </summary>		
        [DataMember]
        public string CarType { get; set; }

        /// <summary>
        /// 裸车价格
        /// </summary>		
        [DataMember]
        public decimal CarPrice { get; set; }

        /// <summary>
        /// 首付比例
        /// </summary>		
        [DataMember]
        public decimal CarDownPaymentPercent { get; set; }

        /// <summary>
        /// 贷款期限
        /// </summary>		
        [DataMember]
        public int CarLoanYear { get; set; }

        /// <summary>
        /// 贷款利率
        /// </summary>		
        [DataMember]
        public decimal CarLoanRate { get; set; }

        /// <summary>
        /// 购置税
        /// </summary>		
        [DataMember]
        public decimal PurchaseTax { get; set; }

        /// <summary>
        /// 上牌费用
        /// </summary>		
        [DataMember]
        public decimal CarRegFee { get; set; }

        /// <summary>
        /// 汽车排量
        /// </summary>		
        [DataMember]
        public int Displacement { get; set; }

        /// <summary>
        /// 车船使用税
        /// </summary>		
        [DataMember]
        public decimal VehicleAndVesselTax { get; set; }

        /// <summary>
        /// 交强险
        /// </summary>		
        [DataMember]
        public decimal MotorVehicleCompulsory { get; set; }

        /// <summary>
        /// 商业保险
        /// </summary>		
        [DataMember]
        public decimal MotorVehicleCommercial { get; set; }

        /// <summary>
        /// 首付款
        /// </summary>		
        [DataMember]
        public decimal CarDownPayment { get; set; }

        /// <summary>
        /// 购车总花费
        /// </summary>		
        [DataMember]
        public decimal CarTotalAmount { get; set; }

        /// <summary>
        /// 购车月供
        /// </summary>		
        [DataMember]
        public decimal CarMonthlyAmount { get; set; }

        /// <summary>
        /// 消费规划分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

        /// <summary>
        /// 首付款准备的现金总金额
        /// </summary>		
        [DataMember]
        public decimal FirstAmount { get; set; }

        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        [DataMember]
        public decimal ReturnOnInvestment { get; set; }

        /// <summary>
        /// 一次性投入金额
        /// </summary>		
        [DataMember]
        public decimal DisposableInput { get; set; }

        /// <summary>
        /// 每月定期投资金额
        /// </summary>		
        [DataMember]
        public decimal MonthlyInvestment { get; set; }

        /// <summary>
        /// 定期定额投资年限
        /// </summary>		
        [DataMember]
        public decimal RegularYear { get; set; }

        /// <summary>
        /// 此方案能实现的目标金额
        /// </summary>		
        [DataMember]
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// 购房年限
        /// </summary>		
        [DataMember]
        public decimal ShopHouseYear { get; set; }

        /// <summary>
        /// 购房总金额
        /// </summary>		
        [DataMember]
        public decimal HouseAllMoney { get; set; }

        /// <summary>
        /// 购车年限
        /// </summary>		
        [DataMember]
        public decimal ShopCarYear { get; set; }
    }
}