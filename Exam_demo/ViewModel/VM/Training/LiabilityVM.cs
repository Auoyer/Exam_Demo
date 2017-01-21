using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Liability
    /// </summary>
    public class LiabilityVM
    {
        public LiabilityVM()
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
        /// 现金
        /// </summary>		
        public decimal Cash { get; set; }

        /// <summary>
        /// 人民币银行活存
        /// </summary>		
        public decimal RMBDeposit { get; set; }

        /// <summary>
        /// 其他流动资产
        /// </summary>		
        public decimal OtherAsset { get; set; }

        /// <summary>
        /// 人民币银行定存
        /// </summary>		
        public decimal RMBFixedDeposit { get; set; }

        /// <summary>
        /// 外币银行定存
        /// </summary>		
        public decimal ForeignCurrencyFixedDeposit { get; set; }

        /// <summary>
        /// 股票投资
        /// </summary>		
        public decimal StockInvestment { get; set; }

        /// <summary>
        /// 债券投资
        /// </summary>		
        public decimal BondInvestment { get; set; }

        /// <summary>
        /// 基金投资
        /// </summary>		
        public decimal FundInvestment { get; set; }

        /// <summary>
        /// 实业投资
        /// </summary>		
        public decimal IndustryInvestment { get; set; }

        /// <summary>
        /// 投资性房地产
        /// </summary>		
        public decimal EstateInvestment { get; set; }

        /// <summary>
        /// 保单现金价值
        /// </summary>		
        public decimal PolicyInvestment { get; set; }

        /// <summary>
        /// 其他投资性资产
        /// </summary>		
        public decimal OtherInvestment { get; set; }

        /// <summary>
        /// 自用房地产
        /// </summary>		
        public decimal Estate { get; set; }

        /// <summary>
        /// 自用汽车
        /// </summary>		
        public decimal Car { get; set; }

        /// <summary>
        /// 自用其他资产
        /// </summary>		
        public decimal Others { get; set; }

        /// <summary>
        /// 资产合计
        /// </summary>		
        public decimal TotalAssets { get; set; }

        /// <summary>
        /// 信用卡欠款
        /// </summary>		
        public decimal CreditCard { get; set; }

        /// <summary>
        /// 小额消费信贷
        /// </summary>		
        public decimal Microfinance { get; set; }

        /// <summary>
        /// 其他消费性负债
        /// </summary>		
        public decimal OtherLoan { get; set; }

        /// <summary>
        /// 金融投资借款
        /// </summary>		
        public decimal FinancialLoan { get; set; }

        /// <summary>
        /// 实业投资借款
        /// </summary>		
        public decimal IndustryInvestmentLoan { get; set; }

        /// <summary>
        /// 投资性房地产按揭贷款
        /// </summary>		
        public decimal EstateInvestmentLoan { get; set; }

        /// <summary>
        /// 其他投资性负债
        /// </summary>		
        public decimal OtherInvestmentLoan { get; set; }

        /// <summary>
        /// 自用房地产贷款
        /// </summary>		
        public decimal EstateLoan { get; set; }

        /// <summary>
        /// 自用汽车贷款
        /// </summary>		
        public decimal CarLoan { get; set; }

        /// <summary>
        /// 其他自用贷款
        /// </summary>		
        public decimal OthersLoan { get; set; }

        /// <summary>
        /// 负债合计
        /// </summary>		
        public decimal TotalLoan { get; set; }

        private decimal _totalVal;
        /// <summary>
        /// 净值合计
        /// </summary>
        public decimal TotalVal
        {
            get {
                if (_totalVal == 0)
                {
                    _totalVal = (Cash + RMBDeposit + OtherAsset) - (CreditCard + Microfinance + OtherLoan ) + (RMBFixedDeposit + ForeignCurrencyFixedDeposit  + StockInvestment  + BondInvestment  + FundInvestment  + IndustryInvestment  + PolicyInvestment  + EstateInvestment  + OtherInvestment ) - (FinancialLoan  + IndustryInvestmentLoan  + EstateInvestmentLoan  + OtherInvestmentLoan ) + (Estate  + Car  + Others ) - (EstateLoan  + CarLoan  + OthersLoan );
                }
                return _totalVal; }
        
        }

    }
}