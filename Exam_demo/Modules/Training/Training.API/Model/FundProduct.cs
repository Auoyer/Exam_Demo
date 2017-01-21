using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///FundProduct
    /// </summary>
    [DataContract]
    public class FundProduct
    {
        public FundProduct()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int FundId { get; set; }

        /// <summary>
        /// 基金代码
        /// </summary>		
        [DataMember]
        public string FundCode { get; set; }

        /// <summary>
        /// 产品
        /// </summary>		
        [DataMember]
        public string FundName { get; set; }

        /// <summary>
        /// 基金类型
        /// </summary>		
        [DataMember]
        public string FundType { get; set; }

        /// <summary>
        /// 最新净值
        /// </summary>		
        [DataMember]
        public decimal? NewNetValue { get; set; }

        /// <summary>
        /// 累计净值
        /// </summary>		
        [DataMember]
        public decimal? TotalNewValue { get; set; }

        /// <summary>
        /// 最新净值更新时间
        /// </summary>		
        [DataMember]
        public DateTime NavUpdateDate { get; set; }

        /// <summary>
        /// 托管费率
        /// </summary>		
        [DataMember]
        public string HostingFees { get; set; }

        /// <summary>
        /// 最低申购份额
        /// </summary>		
        [DataMember]
        public string PurchaseShares { get; set; }

        /// <summary>
        /// 基金公司
        /// </summary>		
        [DataMember]
        public string FundCompany { get; set; }

        /// <summary>
        /// 近一年收益率
        /// </summary>		
        [DataMember]
        public decimal? YearlyEarningsRate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        [DataMember]
        public DateTime UpdateDate { get; set; }

        //----------------------------------扩展------------------------------------------
        /// <summary>
        /// 详细信息
        /// </summary>
        [DataMember]
        public List<FundProductDetail> FundProductDetail { get; set; }

    }
}