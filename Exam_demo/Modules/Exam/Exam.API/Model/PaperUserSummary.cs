using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    ///用户试卷得分情况
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
        /// 试卷Id
        /// </summary>		
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 用户所属竞赛Id
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// 用户所属班级Id（弃用）
        /// </summary>		
        [DataMember]
        public int ClassId { get; set; }

        /// <summary>
        /// 待评分数目（弃用）
        /// </summary>		
        [DataMember]
        public int UnScoredCount { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>		
        [DataMember]
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 用户得分
        /// </summary>		
        [DataMember]
        public decimal Score { get; set; }

        /// <summary>
        /// 评分状态
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>		
        [DataMember]
        public DateTime? FinishDate { get; set; }

        //////////////////////扩展字段

        /// <summary>
        /// 考核名称
        /// </summary>		
        [DataMember]
        public string ExamPaperName { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>		
        [DataMember]
        public int UId { get; set; }

        /// <summary>
        /// 试卷
        /// </summary>		
        [DataMember]
        public Paper ExamPaper { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>	
         [DataMember]
        public DateTime EndDate { get; set; }

         /// <summary>
         /// 试卷状态
         /// </summary>		
         [DataMember]
         public int Status2 { get; set; }
    }
}