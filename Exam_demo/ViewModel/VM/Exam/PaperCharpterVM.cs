using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///试卷关联章节
    /// </summary>
    public class PaperCharpterVM
    {
        public PaperCharpterVM()
        {

        }

        /// <summary>
        /// 试卷ID
        /// </summary>		
        public int PaperID { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>		
        public int CharpterID { get; set; }

        //////////////////////////////////扩展
        //章节名称
        public string Name { get; set; }
       
    }
}