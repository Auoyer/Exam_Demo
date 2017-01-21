using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ExamCase
    /// </summary>
    [DataContract]
    public class ExamCase
    {
        public ExamCase()
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
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>		
        [DataMember]
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型Id
        /// </summary>		
        [DataMember]
        public int FinancialTypeId { get; set; }

        /// <summary>
        /// 客户背景
        /// </summary>		
        [DataMember]
        public string CustomerStory { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public DateTime CreateTime { get; set; }


    }
}