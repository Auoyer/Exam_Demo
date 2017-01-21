using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///题目附件
    /// </summary>
    public class QuestionAttachmentsVM
    {
        public QuestionAttachmentsVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 题目Id
        /// </summary>		
        public int QuestionId { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>		
        public string FileUrl { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>		
        public string Name { get; set; }

    }
}