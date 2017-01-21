using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///TheoryChapterHidden
    /// </summary>
    [DataContract]
    public class TheoryChapterHidden
    {
        public TheoryChapterHidden()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 理论实训章节Id(仅限内置)
        /// </summary>		
        [DataMember]
        public int TheoryChapterId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

    }
}