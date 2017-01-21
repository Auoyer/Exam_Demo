using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///题目标准答案
    /// </summary>
    public class QuestionAnswerVM
    {
        public QuestionAnswerVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 问题Id
        /// </summary>		
        public int QuestionId { get; set; }

        /// <summary>
        /// 正确答案
        /// </summary>		
        public int Answer { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int Sort { get; set; }

    }
}