using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///题目选项
    /// </summary>
    public class QuestionOptionVM
    {
        public QuestionOptionVM()
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
        /// 选项内容
        /// </summary>		
        public string OptionName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int Sort { get; set; }

    }
}