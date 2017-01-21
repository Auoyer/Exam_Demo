using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ResourceChapter
    /// </summary>
    public class ResourceChapterVM
    {
        public ResourceChapterVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 资源章节名称
        /// </summary>		
        public string ChapterName { get; set; }

        /// <summary>
        /// 来源：内置，自定义
        /// </summary>		
        public int ChapterSource { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

    }
}