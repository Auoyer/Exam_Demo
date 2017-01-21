using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 题目
    /// </summary>
    [DataContract]
    public class Question
    {
        public Question()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }    

        /// <summary>
        /// 题干
        /// </summary>		
        [DataMember]
        public string Context { get; set; }

        /// <summary>
        /// 题型：1.单选题、2.多选题、3.判断题
        /// </summary>		
        [DataMember]
        public int StructType { get; set; }

        /// <summary>
        /// 题库（弃用）1.理论考试、2.银行从业人员资格认证、3.理财规划师资格认证
        /// </summary>		
        [DataMember]
        public int LibraryID { get; set; }

        /// <summary>
        /// 题目章节Id（细分）
        /// </summary>		
        [DataMember]
        public int CharpterID { get; set; }

        /// <summary>
        /// 题目解析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

        /// <summary>
        /// 题目状态：1.开启、2.屏蔽、3.逻辑删除
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 状态：0.为查看、1.已查看
        /// </summary>
        [DataMember]
        public int ViewStatus { get; set; }

        /// <summary>
        /// 题目来源：1.系统内置、2.管理员、3.自定义
        /// </summary>		
        [DataMember]
        public int Source { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreatedTime { get; set; }


        /*=====================================================================================*/
        /// <summary>
        /// 题目选项列表
        /// </summary>
        [DataMember]
        public List<QuestionOption> OptionList { get; set; }
        /// <summary>
        /// 题目答案列表
        /// </summary>
        [DataMember]
        public List<QuestionAnswer> AnswerList { get; set; }
        /// <summary>
        /// 题目附件列表
        /// </summary>
        [DataMember]
        public List<QuestionAttachments> AttachmentList { get; set; }

    }
}