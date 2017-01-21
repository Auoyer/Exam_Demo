using System;
using System.Globalization;

namespace DataSync
{
    /// <summary>
    /// 基金日净值文件
    /// </summary>
    public class FUND_NAV
    {
        /// <summary>
        /// 最新净值
        /// </summary>
        public decimal? NAV { get; set; }

        /// <summary>
        /// 累计净值
        /// </summary>
        public decimal? AccumulativeNAV { get; set; }

        /// <summary>
        /// 7日年化收益率
        /// </summary>
        public decimal? ANNUALIZEDYIELD { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TRADINGDATE { get; set; }
    }
}
