using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ResourceChapterHidden
    /// </summary>
    [DataContract]
    public class ResourceChapterHidden
    {
        public ResourceChapterHidden()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 资源章节Id(仅限内置)
        /// </summary>		
        [DataMember]
        public int ResourceChapterId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

    }
}