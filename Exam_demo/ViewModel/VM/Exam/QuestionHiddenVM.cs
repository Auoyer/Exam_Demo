using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///用户屏蔽题目
    /// </summary>
    public class QuestionHiddenVM
    {
        public QuestionHiddenVM()
        {
            IsDelete = false;
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 理论考核试题Id
        /// </summary>		
        public int QuestionId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 是否内置题目删除
        /// </summary>
        public bool IsDelete { get; set; }

    }
}