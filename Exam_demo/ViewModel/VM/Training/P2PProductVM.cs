using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///P2PProduct
    /// </summary>
    public class P2PProductVM
    {
        public P2PProductVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>		
        public string P2PName { get; set; }

        /// <summary>
        /// 投资领域
        /// </summary>		
        public string InvestmentField { get; set; }

        /// <summary>
        /// 投资周期
        /// </summary>		
        public string InvestmentCycle { get; set; }

        /// <summary>
        /// 起投金额
        /// </summary>		
        public string StartAmount { get; set; }

        /// <summary>
        /// 预期收益率
        /// </summary>		
        public string EarningsRate { get; set; }

        /// <summary>
        /// 来源Id，用于判重
        /// </summary>
        public string SourceId { get; set; }

    }
}