using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    /// 试卷大赛关联
    /// </summary>
    public class PaperCompetitionVM
    {
        public PaperCompetitionVM()
        {

        }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 大赛Id
        /// </summary>		
        public int CompetitionId { get; set; }
        
        /// <summary>
        /// 大赛名称
        /// </summary>
        public string CompetitionName { get; set; }

    }
}