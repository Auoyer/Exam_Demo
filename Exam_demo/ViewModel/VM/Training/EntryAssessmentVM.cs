using System;

namespace VM
{
    /// <summary>
    /// 进入实训考核表
    /// </summary> 
    public class EntryAssessmentVM
    {
        /// <summary>
        /// 主键ID
        /// </summary> 
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary> 
        public int UserId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public int CompetitionId { get; set; }

        /// <summary>
        /// 实训考核ID
        /// </summary> 
        public int TrainExamId { get; set; }

        /// <summary>
        /// 进入考核时间
        /// </summary> 
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 结束考试时间
        /// </summary> 
        public DateTime EndTime { get; set; }
    }
}
