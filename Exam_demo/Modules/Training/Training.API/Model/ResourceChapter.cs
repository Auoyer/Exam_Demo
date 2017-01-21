using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ResourceChapter
    /// </summary>
    [DataContract]
    public class ResourceChapter
    {
        public ResourceChapter()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 资源章节名称
        /// </summary>		
        [DataMember]
        public string ChapterName { get; set; }

        /// <summary>
        /// 来源：内置，自定义
        /// </summary>		
        [DataMember]
        public int ChapterSource { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

    }
}