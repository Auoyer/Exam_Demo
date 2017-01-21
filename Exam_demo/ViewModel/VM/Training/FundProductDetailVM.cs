using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///FundProductDetail
    /// </summary>
    public class FundProductDetailVM
    {
        public FundProductDetailVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 基金产品ID
        /// </summary>		
        public int FundId { get; set; }

        /// <summary>
        /// 最新净值
        /// </summary>		
        public decimal? NewNetValue { get; set; }

        /// <summary>
        /// 累计净值
        /// </summary>
        public decimal? TotalNewValue { get; set; }

        /// <summary>
        /// 7日年化收益率
        /// </summary>
        public decimal? AnnualizedYield { get; set; }

        /// <summary>
        /// 近一年收益率
        /// </summary>		
        public decimal? YearlyEarningsRate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime UpdateDate { get; set; }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// 创建日期字符串
        /// </summary>
        public string strUpdateDate
        {
            get
            {
                return UpdateDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

    }
}