using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 试卷大赛关联
    /// </summary>
    [DataContract]
    public class PaperCompetition
    {
        public PaperCompetition()
        {
        }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

    }
}