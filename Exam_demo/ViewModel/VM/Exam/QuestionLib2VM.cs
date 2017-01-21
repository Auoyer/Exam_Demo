using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    /// 超管-单校题库
    /// </summary>
    public class QuestionLib2VM
    {
        public QuestionLib2VM()
        {
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public int CollegeId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>		
        public string strCollege { get; set; }

        /// <summary>
        /// 题干
        /// </summary>		
        public string Context { get; set; }

        /// <summary>
        /// 题型：1.单选题、2.多选题、3.判断题
        /// </summary>		
        public int StructType { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>
        public string StrStructType { get; set; }

        /// <summary>
        /// 题目章节Id（细分）
        /// </summary>		
        public int CharpterTypeID { get; set; }

        /// <summary>
        /// 章节
        /// </summary>		
        public int CharpterID { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>		
        public string strCharpterID { get; set; }
    }
}