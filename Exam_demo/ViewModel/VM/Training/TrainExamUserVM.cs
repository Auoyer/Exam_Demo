using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    /// 实训考试考生关联表
    /// </summary>
    public class TrainExamUserVM
    {
        public TrainExamUserVM()
        {

        }

        /// <summary>
        /// 实训考核Id
        /// </summary>		
        public int TrainExamId { get; set; }

        /// <summary>
        /// 大赛Id
        /// </summary>		
        public int UserId { get; set; }

    }
}