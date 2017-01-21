using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    /// 实训考试大赛关联表
    /// </summary>
    [DataContract]
    public class TrainExamCompetition
    {
        public TrainExamCompetition()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 实训考核Id
        /// </summary>		
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// 大赛Id
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// 大赛名称
        /// </summary>		
        public string CompetitionName { get; set; }
    }
}