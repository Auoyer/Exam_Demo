using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///P2PProduct
    /// </summary>
    [DataContract]
    public class P2PProduct
    {
        public P2PProduct()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>		
        [DataMember]
        public string P2PName { get; set; }

        /// <summary>
        /// 投资领域
        /// </summary>		
        [DataMember]
        public string InvestmentField { get; set; }

        /// <summary>
        /// 投资周期
        /// </summary>		
        [DataMember]
        public string InvestmentCycle { get; set; }

        /// <summary>
        /// 起投金额
        /// </summary>		
        [DataMember]
        public string StartAmount { get; set; }

        /// <summary>
        /// 预期收益率
        /// </summary>		
        [DataMember]
        public string EarningsRate { get; set; }

        /// <summary>
        /// 来源Id，用于判重
        /// </summary>
        [DataMember]
        public string SourceId { get; set; }

    }
}