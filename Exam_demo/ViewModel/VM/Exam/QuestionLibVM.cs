using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    /// 超管-题库
    /// </summary>
    public class QuestionLibVM
    {
        public QuestionLibVM()
        {
        }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public int CollegeId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>		
        public string strCollege { get; set; }

        /// <summary>
        /// 题型：1.单选题、2.多选题、3.判断题
        /// </summary>		
        public int StructType { get; set; }

        /// <summary>
        /// 章节（细分）
        /// </summary>		
        public int TypeId { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>
        public string StrStructType { get; set; }

        /// <summary>
        /// 题量
        /// </summary>		
        public int Count { get; set; }

        /// <summary>
        /// 来源
        /// </summary>		
        public int Source { get; set; }

        /// <summary>
        /// 上传时间（最近一次）
        /// </summary>		
        public DateTime LastTime { get; set; }

        /// <summary>
        /// 上传时间（最近一次）字符串
        /// </summary>
        public string strLastTime
        {
            get
            {
                if (LastTime == DateTime.MinValue)
                {
                    return "------";
                }
                return LastTime.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 状态：0.为查看、1.已查看
        /// </summary>		
        public int ViewStatus { get; set; }
    }
}