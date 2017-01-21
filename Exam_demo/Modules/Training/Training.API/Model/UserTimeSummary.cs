using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///用户操作时间统计
    /// </summary>
    [DataContract]
    public class UserTimeSummary
    {
        public UserTimeSummary()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 统计类型，枚举
        /// </summary>		
        [DataMember]
        public int SummaryType { get; set; }

        /// <summary>
        /// 花费时间(分钟)
        /// </summary>		
        [DataMember]
        public int UsedTime { get; set; }

    }
}