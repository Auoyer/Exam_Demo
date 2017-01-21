using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///StuCustomerDetail
    /// </summary>
    [DataContract]
    public class StuCustomerDetail
    {
        public StuCustomerDetail()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>		
        [DataMember]
        public int CustomerId { get; set; }

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
        public decimal? InCome { get; set; }

    }
}