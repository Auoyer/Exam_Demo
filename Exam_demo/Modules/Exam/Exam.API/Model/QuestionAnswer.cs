using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 题目标准答案
    /// </summary>
    [DataContract]
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 问题Id
        /// </summary>		
        [DataMember]
        public int QuestionId { get; set; }

        /// <summary>
        /// 正确答案
        /// </summary>		
        [DataMember]
        public int Answer { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        [DataMember]
        public int Sort { get; set; }

    }
}