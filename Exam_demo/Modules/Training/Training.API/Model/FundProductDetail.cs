using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///FundProductDetail
    /// </summary>
    [DataContract]
    public class FundProductDetail
    {
        public FundProductDetail()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 基金产品ID
        /// </summary>		
        [DataMember]
        public int FundId { get; set; }

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
        /// 7日年化收益率
        /// </summary>
        [DataMember]
        public decimal? AnnualizedYield { get; set; }

        /// <summary>
        /// 近一年收益率
        /// </summary>		
        [DataMember]
        public decimal? YearlyEarningsRate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>		
        [DataMember]
        public DateTime UpdateDate { get; set; }

    }
}