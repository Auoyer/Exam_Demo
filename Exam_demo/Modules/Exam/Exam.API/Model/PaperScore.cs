using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 试卷分数结构
    /// </summary>
    [DataContract]
    public class PaperScore
    {
        public PaperScore()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>		
        [DataMember]
        public int PaperID { get; set; }

        /// <summary>
        /// 章节ID（细分）（一张试卷某种题型所属的所有章节，如单选题所属章节可为"1，7，13，19"）
        /// </summary>		
        [DataMember]
        public string CharpterID { get; set; }

        /// <summary>
        /// 当前章节题目数
        /// </summary>		
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// 每题分值
        /// </summary>		
        [DataMember]
        public decimal Score { get; set; }

    }
}