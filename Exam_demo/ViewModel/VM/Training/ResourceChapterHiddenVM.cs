using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ResourceChapterHidden
    /// </summary>
    public class ResourceChapterHiddenVM
    {
        public ResourceChapterHiddenVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 资源章节Id(仅限内置)
        /// </summary>		
        public int ResourceChapterId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

    }
}