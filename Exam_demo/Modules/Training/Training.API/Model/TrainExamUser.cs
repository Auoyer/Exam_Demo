using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    /// 实训考试考生关联表
    /// </summary>
    [DataContract]
    public class TrainExamUser
    {
        public TrainExamUser()
        {

        }

        /// <summary>
        /// 实训考核Id
        /// </summary>		
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// 大赛Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }
    }
}