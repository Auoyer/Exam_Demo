using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///试卷分配班级
    /// </summary>
    public class PaperClassVM
    {
        public PaperClassVM()
        {

        }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>		
        public int ClassId { get; set; }
        /////////////////////////////////////扩展
        //班级名称
        public string ClassName { get; set; }

    }
}