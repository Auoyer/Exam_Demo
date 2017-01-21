using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TheoryChapter
    /// </summary>
    public class TheoryChapterVM
    {
        public TheoryChapterVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 理论考核章节名称
        /// </summary>		
        public string ChapterName { get; set; }

        /// <summary>
        /// 来源：内置，自定义(弃用)
        /// </summary>		
        public int ChapterSource { get; set; }

        /// <summary>
        /// 题库ID(弃用)
        /// </summary>		
        public int LibraryID { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public int CollegeId { get; set; }   

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /*====================================================================*/

        /// <summary>
        /// 章节子题型列表
        /// </summary>
        public List<TheoryQuestionTypeVM> SubTypes { get; set; }
    }
}