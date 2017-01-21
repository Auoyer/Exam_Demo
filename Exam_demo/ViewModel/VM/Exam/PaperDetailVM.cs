using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///试卷题目关联
    /// </summary>
    [Serializable]
    public class PaperDetailVM
    {
        public PaperDetailVM()
        {

        }

        ///// <summary>
        ///// Id
        ///// </summary>		
        //public int Id { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 问题Id
        /// </summary>		
        public int QuesionId { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>		
        public int StructType { get; set; }

        /// <summary>
        /// 题型Id
        /// </summary>		
        public int StructTypeId { get; set; } 
    }
}