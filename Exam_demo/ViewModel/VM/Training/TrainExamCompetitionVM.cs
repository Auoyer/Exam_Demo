using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    /// 实训考试大赛关联表
    /// </summary>
    public class TrainExamCompetitionVM
    {
        public TrainExamCompetitionVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 实训考核Id
        /// </summary>		
        public int TrainExamId { get; set; }

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