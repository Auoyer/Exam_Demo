using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TheoryChapterHidden
    /// </summary>
    public class TheoryChapterHiddenVM
    {
        public TheoryChapterHiddenVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 理论实训章节Id(仅限内置)
        /// </summary>		
        public int TheoryChapterId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

    }
}