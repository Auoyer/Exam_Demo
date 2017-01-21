using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 题目选项
    /// </summary>
    [DataContract]
    public class QuestionOption
    {
        public QuestionOption()
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
        /// 选项内容
        /// </summary>		
        [DataMember]
        public string OptionName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        [DataMember]
        public int Sort { get; set; }

    }
}