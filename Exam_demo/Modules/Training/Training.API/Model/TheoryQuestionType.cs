using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///TheoryQuestionType
    /// </summary>
    [DataContract]
    public class TheoryQuestionType
    {
        public TheoryQuestionType()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int TheoryChapterId { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>		
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// 来源：内置，自定义（弃用）
        /// </summary>		
        [DataMember]
        public int TypeSource { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

    }
}