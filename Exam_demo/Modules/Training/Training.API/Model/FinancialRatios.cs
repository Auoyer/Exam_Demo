using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///FinancialRatios
    /// </summary>
    [DataContract]
    public class FinancialRatios
    {
        public FinancialRatios()
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
        /// 资产负债结构分析
        /// </summary>		
        [DataMember]
        public string LiabilityAnalysis { get; set; }

        /// <summary>
        /// 收支储蓄结构分析
        /// </summary>		
        [DataMember]
        public string IncomeAndExpensesAnalysis { get; set; }

        /// <summary>
        /// 客户财务情况分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

    }
}