using System;
using System.Runtime.Serialization;

namespace Structure.API
{
    /// <summary>
    ///Class
    /// </summary>
    [DataContract]
    public class Class
    {

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>		
        [DataMember]
        public string ClassName { get; set; }

        /// <summary>
        /// 学院Id，默认为0
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 学校Id，默认为0
        /// </summary>		
        [DataMember]
        public int SchoolId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        [DataMember]
        public string Remark { get; set; }

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
        /// 枚举：1.正常，2.删除
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

    }
}