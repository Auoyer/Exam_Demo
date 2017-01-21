using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Exam.API
{
    /// <summary>
    ///试卷题目关联
    /// </summary>
    [DataContract]
    public class PaperDetail
    {
        public PaperDetail()
        {

        }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 题目Id
        /// </summary>		
        [DataMember]
        public int QuesionId { get; set; }

    }
}