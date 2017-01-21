using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///Liability
    /// </summary>
    [DataContract]
    public class Liability
    {
        public Liability()
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
        /// 现金
        /// </summary>		
        [DataMember]
        public decimal Cash { get; set; }

        /// <summary>
        /// 人民币银行活存
        /// </summary>		
        [DataMember]
        public decimal RMBDeposit { get; set; }

        /// <summary>
        /// 其他流动资产
        /// </summary>		
        [DataMember]
        public decimal OtherAsset { get; set; }

        /// <summary>
        /// 人民币银行定存
        /// </summary>		
        [DataMember]
        public decimal RMBFixedDeposit { get; set; }

        /// <summary>
        /// 外币银行定存
        /// </summary>		
        [DataMember]
        public decimal ForeignCurrencyFixedDeposit { get; set; }

        /// <summary>
        /// 股票投资
        /// </summary>		
        [DataMember]
        public decimal StockInvestment { get; set; }

        /// <summary>
        /// 债券投资
        /// </summary>		
        [DataMember]
        public decimal BondInvestment { get; set; }

        /// <summary>
        /// 基金投资
        /// </summary>		
        [DataMember]
        public decimal FundInvestment { get; set; }

        /// <summary>
        /// 实业投资
        /// </summary>		
        [DataMember]
        public decimal IndustryInvestment { get; set; }

        /// <summary>
        /// 投资性房地产
        /// </summary>		
        [DataMember]
        public decimal EstateInvestment { get; set; }

        /// <summary>
        /// 保单现金价值
        /// </summary>		
        [DataMember]
        public decimal PolicyInvestment { get; set; }

        /// <summary>
        /// 其他投资性资产
        /// </summary>		
        [DataMember]
        public decimal OtherInvestment { get; set; }

        /// <summary>
        /// 自用房地产
        /// </summary>		
        [DataMember]
        public decimal Estate { get; set; }

        /// <summary>
        /// 自用汽车
        /// </summary>		
        [DataMember]
        public decimal Car { get; set; }

        /// <summary>
        /// 自用其他资产
        /// </summary>		
        [DataMember]
        public decimal Others { get; set; }

        /// <summary>
        /// 资产合计
        /// </summary>		
        [DataMember]
        public decimal TotalAssets { get; set; }

        /// <summary>
        /// 信用卡欠款
        /// </summary>		
        [DataMember]
        public decimal CreditCard { get; set; }

        /// <summary>
        /// 小额消费信贷
        /// </summary>		
        [DataMember]
        public decimal Microfinance { get; set; }

        /// <summary>
        /// 其他消费性负债
        /// </summary>		
        [DataMember]
        public decimal OtherLoan { get; set; }

        /// <summary>
        /// 金融投资借款
        /// </summary>		
        [DataMember]
        public decimal FinancialLoan { get; set; }

        /// <summary>
        /// 实业投资借款
        /// </summary>		
        [DataMember]
        public decimal IndustryInvestmentLoan { get; set; }

        /// <summary>
        /// 投资性房地产按揭贷款
        /// </summary>		
        [DataMember]
        public decimal EstateInvestmentLoan { get; set; }

        /// <summary>
        /// 其他投资性负债
        /// </summary>		
        [DataMember]
        public decimal OtherInvestmentLoan { get; set; }

        /// <summary>
        /// 自用房地产贷款
        /// </summary>		
        [DataMember]
        public decimal EstateLoan { get; set; }

        /// <summary>
        /// 自用汽车贷款
        /// </summary>		
        [DataMember]
        public decimal CarLoan { get; set; }

        /// <summary>
        /// 其他自用贷款
        /// </summary>		
        [DataMember]
        public decimal OthersLoan { get; set; }

        /// <summary>
        /// 负债合计
        /// </summary>		
        [DataMember]
        public decimal TotalLoan { get; set; }

    }
}