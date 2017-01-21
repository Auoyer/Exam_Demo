using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ProposalCustomerDetail
    /// </summary>
    [DataContract]
    public class ProposalCustomerDetail
    {
        public ProposalCustomerDetail()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        [DataMember]
        public int ProposalId { get; set; }

        /// <summary>
        /// 客户信息家属/财产分配家属
        /// </summary>		
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// 家属姓名
        /// </summary>		
        [DataMember]
        public string DependentName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>		
        [DataMember]
        public int Age { get; set; }

        /// <summary>
        /// 与客户关系
        /// </summary>		
        [DataMember]
        public string Relation { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>		
        [DataMember]
        public decimal InCome { get; set; }

    }
}