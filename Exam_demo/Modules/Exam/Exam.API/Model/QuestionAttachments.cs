using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 题目附件
    /// </summary>
    [DataContract]
    public class QuestionAttachments
    {
        public QuestionAttachments()
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
        /// 文件地址（相对路径）
        /// </summary>		
        [DataMember]
        public string FileUrl { get; set; }

        /// <summary>
        /// 文件名称（"C:\fakepath\原名"）
        /// </summary>		
        [DataMember]
        public string Name { get; set; }

    }
}