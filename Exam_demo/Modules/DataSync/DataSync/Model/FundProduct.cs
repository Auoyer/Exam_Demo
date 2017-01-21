using System;
using System.Globalization;

namespace DataSync
{
    /// <summary>
    ///FundProduct
    /// </summary>
    public class FundProduct
    {
        public FundProduct()
        {
            UpdateDate = DateTime.Now;
            NavUpdateDate = DateTime.Now;
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int FundId { get; set; }

        /// <summary>
        /// 基金代码
        /// </summary>		
        public string FundCode { get; set; }

        /// <summary>
        /// 产品
        /// </summary>		
        public string FundName { get; set; }

        /// <summary>
        /// 基金类型
        /// </summary>		
        public string FundType { get; set; }

        /// <summary>
        /// 最新净值
        /// </summary>		
        public decimal? NewNetValue { get; set; }

        /// <summary>
        /// 累计净值
        /// </summary>		
        public decimal? TotalNewValue { get; set; }

        /// <summary>
        /// 最新净值更新时间
        /// </summary>
        public DateTime NavUpdateDate { get; set; }

        /// <summary>
        /// 托管费率
        /// </summary>		
        public string HostingFees { get; set; }

        /// <summary>
        /// 最低申购份额
        /// </summary>		
        public string PurchaseShares { get; set; }

        /// <summary>
        /// 基金公司
        /// </summary>		
        public string FundCompany { get; set; }

        /// <summary>
        /// 近一年收益率
        /// </summary>		
        public decimal? YearlyEarningsRate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateDate { get; set; }

    }
}