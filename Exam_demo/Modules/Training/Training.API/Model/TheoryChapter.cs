using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///TheoryChapter
    /// </summary>
    [DataContract]
    public class TheoryChapter
    {
        public TheoryChapter()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 理论考核章节名称
        /// </summary>		
        [DataMember]
        public string ChapterName { get; set; }

        /// <summary>
        /// 来源：内置，自定义
        /// </summary>		
        [DataMember]
        public int ChapterSource { get; set; }

        /// <summary>
        /// 题库（弃用）1.理论考试、2.银行从业人员资格认证、3.理财规划师资格认证
        /// </summary>		
        [DataMember]
        public int LibraryID { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }   

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

        /*======================================================*/

        /// <summary>
        /// 章节子题型列表
        /// </summary>
        [DataMember]
        public List<TheoryQuestionType> SubTypes { get; set; }

    }
}