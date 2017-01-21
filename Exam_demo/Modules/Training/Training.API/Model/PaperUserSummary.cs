using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    /// <summary>
    ///PaperUserSummary
    /// </summary>
    [DataContract]
    public class PaperUserSummary
    {
        public PaperUserSummary()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// ExamPaperId
        /// </summary>		
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// CompetitionId
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// TotalScore
        /// </summary>		
        [DataMember]
        public decimal TotalScore { get; set; }

        /// <summary>
        /// Score
        /// </summary>		
        [DataMember]
        public decimal Score { get; set; }

        /// <summary>
        /// Status
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// FinishDate
        /// </summary>		
        [DataMember]
        public DateTime FinishDate { get; set; }


        /// <summary>
        /// FinishDate
        /// </summary>		
        [DataMember]
        public int GroupId { get; set; }
    }
}
