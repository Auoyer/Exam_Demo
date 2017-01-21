using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    /// 客户潜在存在表
    /// </summary>
    [DataContract]
  public class CustomerCount
    {
        public CustomerCount()
        {

        }
        /// <summary>
        /// 潜在客户总数
        /// </summary>
        [DataMember]
        public int CustomerPotentialSum { get; set; }
        /// <summary>
        /// 已有客户
        /// </summary>
        [DataMember]
        public int CustomerExistSum { get; set; }
        
        /// <summary>
        /// 高净资产已有客户数
        /// </summary>
        [DataMember]
        public decimal CustomerExistHighAssets { get; set; }
        /// <summary>
        ///高净资产潜在客户
        /// </summary>
        [DataMember]
        public decimal CustomerPotentialHighAssets { get; set; }
        /// <summary>
        /// 获取所有客户条数
        /// </summary>
        [DataMember]
        public int CustomerSumNum { get; set; }


    }
}
