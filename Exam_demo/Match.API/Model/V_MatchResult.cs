using System;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    ///V_MatchUser
    /// </summary>
    [DataContract]
    public class V_MatchResult
    {
        public V_MatchResult()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// CompetitionId
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// GroupId
        /// </summary>		
        [DataMember]
        public int GroupId { get; set; }

        /// <summary>
        /// GroupSouce
        /// </summary>		
        [DataMember]
        public int GroupSouce { get; set; }

        /// <summary>
        /// UserName
        /// </summary>		
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// IDCard
        /// </summary>		
        [DataMember]
        public string IDCard { get; set; }

        /// <summary>
        /// Phone
        /// </summary>	
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// AccountNo
        /// </summary>		
        [DataMember]
        public string AccountNo { get; set; }

        /// <summary>
        /// CollegeName
        /// </summary>		
        [DataMember]
        public string CollegeName { get; set; }

        /// <summary>
        /// CollegeId
        /// </summary>	
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 主观分（实训）
        /// </summary>
        [DataMember]
        public decimal SubjectiveResults { get; set; }
        /// <summary>
        /// 客观分（实训）
        /// </summary>
        [DataMember]
        public decimal ObjectiveResults { get; set; }

        /// <summary>
        /// TrainExamId
        /// </summary>		
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// TrainExamStatus 1：未评分 2：已评分
        /// </summary>		
        [DataMember]
        public int TrainExamStatus { get; set; }

        /// <summary>
        /// 用户得分(理论)
        /// </summary>		
        [DataMember]
        public decimal Score { get; set; }

        /// <summary>
        /// ExamPaperId
        /// </summary>
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 实训结果ID
        /// </summary>	
        [DataMember]
        public int AssessmentId { get; set; }
    }
}