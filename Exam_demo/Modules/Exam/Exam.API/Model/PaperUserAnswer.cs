using System;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    ///用户试卷答案
    /// </summary>
    [DataContract]
    public class PaperUserAnswer
    {
        public PaperUserAnswer()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        [DataMember]
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 考生Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 问题Id
        /// </summary>		
        [DataMember]
        public int QuesionId { get; set; }

        /// <summary>
        /// 题型：1.单选题、2.多选题、3.判断题
        /// </summary>		
        [DataMember]
        public int QuesionTypeId { get; set; }

        /// <summary>
        /// 考生答案
        /// </summary>		
        [DataMember]
        public int? Answer { get; set; }

    }
}