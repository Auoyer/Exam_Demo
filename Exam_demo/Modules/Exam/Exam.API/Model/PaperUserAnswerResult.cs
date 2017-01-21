using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    ///用户答题结果分析
    /// </summary>
    [DataContract]
    public class PaperUserAnswerResult
    {
        public PaperUserAnswerResult()
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
        /// 考生Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 题目Id
        /// </summary>		
        [DataMember]
        public int QuesionId { get; set; }

        /// <summary>
        /// 章节Id（细分）
        /// </summary>		
        [DataMember]
        public int QuestionTypeId { get; set; }

        /// <summary>
        /// 题型：1.单选题、2.多选题、3.判断题
        /// </summary>		
        [DataMember]
        public int StructType { get; set; }

        /// <summary>
        /// 每题分值
        /// </summary>		
        [DataMember]
        public decimal QuestionScore { get; set; }

        /// <summary>
        /// 答题结果：1.未答、2.正确、3.错误
        /// </summary>		
        [DataMember]
        public int Result { get; set; }

        /// <summary>
        /// 用户得分
        /// </summary>		
        [DataMember]
        public decimal? UserScore { get; set; }

        /// <summary>
        /// 教师评析(简答)（弃用）
        /// </summary>		
        [DataMember]
        public string Analyse { get; set; }

        /// <summary>
        /// 是否标记（弃用）
        /// </summary>		
        [DataMember]
        public bool IsMark { get; set; }

    }
}