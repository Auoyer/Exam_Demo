using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///InvestmentPlanProduct
    /// </summary>
    [DataContract]
    public class InvestmentPlanProduct
    {
        public InvestmentPlanProduct()
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
        /// 已完成规划Id
        /// </summary>		
        [DataMember]
        public int PlanId { get; set; }

        /// <summary>
        /// 方案所需投资收益率
        /// </summary>		
        [DataMember]
        public decimal PlanRate { get; set; }

        /// <summary>
        /// 活期银行
        /// </summary>		
        [DataMember]
        public int DemandDepositsBank { get; set; }

        /// <summary>
        /// 活期银行利率
        /// </summary>		
        [DataMember]
        public decimal DemandDepositsBankRate { get; set; }

        /// <summary>
        /// 定期银行
        /// </summary>		
        [DataMember]
        public int TimeDepositBank { get; set; }

        /// <summary>
        /// 定期期限
        /// </summary>		
        [DataMember]
        public int TimeDepositBankTime { get; set; }

        /// <summary>
        /// 定期银行利率
        /// </summary>		
        [DataMember]
        public decimal TimeDepositBankRate { get; set; }

        /// <summary>
        /// 保值层基金
        /// </summary>		
        [DataMember]
        public int Fund1 { get; set; }

        /// <summary>
        /// 增值层基金
        /// </summary>		
        [DataMember]
        public int Fund2 { get; set; }

        /// <summary>
        /// P2P产品
        /// </summary>		
        [DataMember]
        public int P2PProduct { get; set; }

        /// <summary>
        /// 年化收益率
        /// </summary>		
        [DataMember]
        public decimal P2PProductRate { get; set; }

        /// <summary>
        /// 投机层基金
        /// </summary>		
        [DataMember]
        public int Fund3 { get; set; }

        /// <summary>
        /// 产品组合预期收益率
        /// </summary>		
        [DataMember]
        public decimal TotalRate { get; set; }

        /// <summary>
        /// 代码-货币基金编号
        /// </summary>
         [DataMember]
        public string CashCode { get; set; }
        /// <summary>
        /// 产品-货币市场基金
        /// </summary>
         [DataMember]
        public string CashFund { get; set; }
        /// <summary>
        /// 类型-货币市场
        /// </summary>
         [DataMember]
        public string CashMarket { get; set; }
        /// <summary>
        /// 近一年收益-货币型
        /// </summary>
         [DataMember]
        public decimal YearlyEarningsRate1 { get; set; }
        /// <summary>
        /// 代码-债券型基金编号
        /// </summary>
         [DataMember]
        public string BondCode { get; set; }
        /// <summary>
        /// 产品-债券型市场基金
        /// </summary>
         [DataMember]
        public string BondFund { get; set; }
        /// <summary>
        /// 类型-债券型市场
        /// </summary>
         [DataMember]
        public string BondMarket { get; set; }
        /// <summary>
        /// 近一年收益-债券型
        /// </summary>
         [DataMember]
        public decimal YearlyEarningsRate2 { get; set; }
        /// <summary>
        /// 代码-股票型金编号
        /// </summary>
         [DataMember]
        public string StockCode { get; set; }
        /// <summary>
        /// 产品-股票型市场基金
        /// </summary>
         [DataMember]
        public string StockFund { get; set; }
        /// <summary>
        /// 类型-股票型市场
        /// </summary>
         [DataMember]
        public string StockMarket { get; set; }
        /// <summary>
        /// 近一年收益-股票型
        /// </summary>
         [DataMember]
        public decimal YearlyEarningsRate3 { get; set; }

         /// <summary>
         /// 产品名称
         /// </summary>		
         [DataMember]
         public string P2PName { get; set; }

         /// <summary>
         /// 投资领域
         /// </summary>		
         [DataMember]
         public string InvestmentField { get; set; }

         /// <summary>
         /// 投资周期
         /// </summary>		
         [DataMember]
         public string InvestmentCycle { get; set; }

         /// <summary>
         /// 起投金额
         /// </summary>		
         [DataMember]
         public string StartAmount { get; set; }

         /// <summary>
         /// 预期收益率
         /// </summary>		
         [DataMember]
         public string EarningsRate { get; set; }

    }
}