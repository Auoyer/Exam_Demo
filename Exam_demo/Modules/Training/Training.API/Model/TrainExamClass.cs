using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///TrainExamClass
    /// </summary>
    [DataContract]
    public class TrainExamClass
    {
        public TrainExamClass()
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
        /// 班级Id
        /// </summary>		
        [DataMember]
        public int ClassId { get; set; }

        /// <summary>
        /// 班级
        /// </summary>		
        public string ClassName { get; set; }
    }
}