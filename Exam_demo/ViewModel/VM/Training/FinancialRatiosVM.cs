using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///FinancialRatios
    /// </summary>
    public class FinancialRatiosVM
    {
        public FinancialRatiosVM()
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
        /// 资产负债结构分析
        /// </summary>		
        public string LiabilityAnalysis { get; set; }

        /// <summary>
        /// 收支储蓄结构分析
        /// </summary>		
        public string IncomeAndExpensesAnalysis { get; set; }

        /// <summary>
        /// 客户财务情况分析
        /// </summary>		
        public string Analysis { get; set; }

    }
}