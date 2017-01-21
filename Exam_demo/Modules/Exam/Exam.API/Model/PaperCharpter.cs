using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 试卷章节关联
    /// </summary>
    [DataContract]
    public class PaperCharpter
    {
        public PaperCharpter()
        {

        }

        /// <summary>
        /// 试卷ID
        /// </summary>		
        [DataMember]
        public int PaperID { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>		
        [DataMember]
        public int CharpterID { get; set; }

    }
}