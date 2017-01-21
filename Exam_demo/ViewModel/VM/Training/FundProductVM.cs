using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///FundProduct
    /// </summary>
    public class FundProductVM
    {
        public FundProductVM()
        {

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

        //----------------------------------扩展------------------------------------------
        /// <summary>
        /// 详细信息
        /// </summary>
        public List<FundProductDetailVM> FundProductDetail { get; set; }

        /// <summary>
        /// 最新净值更新时间字符串
        /// </summary>
        public string strNavUpdateDate
        {
            get
            {
                return NavUpdateDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

    }
}