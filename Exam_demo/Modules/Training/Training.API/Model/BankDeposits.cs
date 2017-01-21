using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///BankDeposits
    /// </summary>
    [DataContract]
    public class BankDeposits
    {
        public BankDeposits()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>		
        [DataMember]
        public string BankName { get; set; }

        /// <summary>
        /// 活期利率
        /// </summary>		
        [DataMember]
        public decimal DemandDeposit { get; set; }

        /// <summary>
        /// 3月定期利率
        /// </summary>		
        [DataMember]
        public decimal ThreeMonth { get; set; }

        /// <summary>
        /// 6月定期利率
        /// </summary>		
        [DataMember]
        public decimal SixMonth { get; set; }

        /// <summary>
        /// 1年定期利率
        /// </summary>		
        [DataMember]
        public decimal Year { get; set; }

        /// <summary>
        /// 2年定期利率
        /// </summary>		
        [DataMember]
        public decimal TwoYear { get; set; }

        /// <summary>
        /// 3年定期利率
        /// </summary>		
        [DataMember]
        public decimal ThreeYear { get; set; }

        /// <summary>
        /// 5年定期利率
        /// </summary>		
        [DataMember]
        public decimal FiveYear { get; set; }

    }
}