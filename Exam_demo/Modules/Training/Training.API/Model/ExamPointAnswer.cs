using System;
using System.Runtime.Serialization;

namespace Training.API
{
    [DataContract]
    public class ExamPointAnswer
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 案例Id
        /// </summary>
        [DataMember]
        public int CaseId { get; set; }

        /// <summary>
        /// 考核点Id
        /// </summary>
        [DataMember]
        public int ExamPointId { get; set; }

        /// <summary>
        /// 标准答案
        /// </summary>
        [DataMember]
        public string Answer { get; set; }
    }
}
