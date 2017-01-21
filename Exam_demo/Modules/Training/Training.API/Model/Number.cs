using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///Number
    /// </summary>
    [DataContract]
    public class Number
    {
        public Number()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 编号类型，枚举
        /// </summary>		
        [DataMember]
        public int NumberType { get; set; }

        /// <summary>
        /// 编号前缀
        /// </summary>		
        [DataMember]
        public string Prefix { get; set; }

        /// <summary>
        /// 编号日期
        /// </summary>		
        [DataMember]
        public DateTime CurrentDate { get; set; }

        /// <summary>
        /// 已使用最大编号
        /// </summary>		
        [DataMember]
        public long UsedMaxCode { get; set; }

        /// <summary>
        /// 允许最大位数
        /// </summary>
        [DataMember]
        public int Figure { get; set; }

    }
}