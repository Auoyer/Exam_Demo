using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    /// 进入实训考核表
    /// </summary>
    [DataContract]
    public class EntryAssessment
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// 实训考核ID
        /// </summary>
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// 进入考核时间
        /// </summary>
        [DataMember]
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 结束考试时间
        /// </summary>
        [DataMember]
        public DateTime EndTime { get; set; }
    }
}
