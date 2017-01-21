using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///TrainExamDetail
    /// </summary>
    [DataContract]
    public class TrainExamDetail
    {
        public TrainExamDetail()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 实训考核/销售机会Id
        /// </summary>		
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// 考核点Id
        /// </summary>		
        [DataMember]
        public int ExamPointId { get; set; }

        /// <summary>
        /// 分值
        /// </summary>		
        [DataMember]
        public decimal Score { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        [DataMember]
        public string Answer { get; set; }


        /// <summary>
        /// 模块IdPointType
        /// </summary>
        [DataMember]
        public string ModularId { get; set; }

        /// <summary>
        /// 客观题主观题类型
        /// </summary>
        [DataMember]
        public int ExamPointType { get; set; }
    }
}