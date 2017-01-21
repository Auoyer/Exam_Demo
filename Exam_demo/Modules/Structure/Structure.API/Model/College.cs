using System;
using System.Runtime.Serialization;

namespace Structure.API
{
    /// <summary>
    ///College
    /// </summary>
    [DataContract]
    public class College
    {

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 学院名称
        /// </summary>		
        [DataMember]
        public string CollegeName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>		
        [DataMember]
        public string DomainName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>		
        [DataMember]
        public string CollegeCode { get; set; }        

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>		
        [DataMember]
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>		
        [DataMember]
        public string Contacts { get; set; }

        /// <summary>
        /// 地址
        /// </summary>		
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>		
        [DataMember]
        public string Tel { get; set; }  
    }
}