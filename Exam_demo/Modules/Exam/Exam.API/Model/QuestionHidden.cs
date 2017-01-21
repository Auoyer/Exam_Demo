using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 用户屏蔽题目
    /// </summary>
    [DataContract]
    public class QuestionHidden
    {
        public QuestionHidden()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 题目Id
        /// </summary>		
        [DataMember]
        public int QuestionId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 是否内置题目删除
        /// </summary>
        [DataMember]
        public bool IsDelete { get; set; }

    }
}