using System;
using System.Runtime.Serialization;

namespace Resource.API
{
    /// <summary>
    /// 通用查询条件过滤器
    /// </summary>
    [DataContract]
    public class CustomFilter
    {
        [DataMember]
        public Nullable<int> Id { get; set; }

        /// <summary>
        /// 章节Id
        /// </summary>
        [DataMember]
        public Nullable<int> ChapterId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public Nullable<int> UserId { get; set; }
    }
}
