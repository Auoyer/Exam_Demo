using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 试卷分配班级（弃用）
    /// </summary>
    [DataContract]
    public class PaperClass
    {
        public PaperClass()
        {

        }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>		
        [DataMember]
        public int ClassId { get; set; }

    }
}