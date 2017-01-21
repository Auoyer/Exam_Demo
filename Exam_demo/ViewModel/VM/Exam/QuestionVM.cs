using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///题目
    /// </summary>
    [Serializable]
    public class QuestionVM
    {
        public QuestionVM()
        {
            ListID = new List<int>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public int CollegeId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>		
        public string strCollege { get; set; }

        /// <summary>
        /// 题干
        /// </summary>		
        public string Context { get; set; }

        /// <summary>
        /// 题型：1.单选题、2.多选题、3.判断题
        /// </summary>		
        public int StructType { get; set; }

        /// <summary>
        /// 题库（弃用）1.理论考试、2.银行从业人员资格认证、3.理财规划师资格认证
        /// </summary>		
        public int LibraryID { get; set; }

        /// <summary>
        /// 题目章节Id（细分）
        /// </summary>		
        public int CharpterID { get; set; }

        /// <summary>
        /// 题目章节
        /// </summary>		
        public int TheoryCharpter { get; set; }

        /// <summary>
        /// 题目章节字符串
        /// </summary>		
        public string strTheoryCharpter { get; set; }

        /// <summary>
        /// 题目解析
        /// </summary>		
        public string Analysis { get; set; }

        /// <summary>
        /// 题目状态：1.开启、2.屏蔽、3.逻辑删除
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 状态：0.为查看、1.已查看
        /// </summary>		
        public int ViewStatus { get; set; }

        /// <summary>
        /// 题目来源：1.系统内置、2.管理员、3.自定义
        /// </summary>		
        public int Source { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        public int PaperId { get; set; }

        /// <summary>
        /// 题目完成情况
        /// </summary>		
        public int? Result { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 是否标记（弃用）
        /// </summary>		
        public bool IsMark { get; set; }

        /*=====================================================================================*/
        /// <summary>
        /// 题目选项列表
        /// </summary>
        public List<QuestionOptionVM> OptionList { get; set; }
        /// <summary>
        /// 题目答案列表
        /// </summary>
        public List<QuestionAnswerVM> AnswerList { get; set; }
        /// <summary>
        /// 题目附件列表
        /// </summary>
        public List<QuestionAttachmentsVM> AttachmentList { get; set; }

        /// <summary>
        /// 题目附件列表
        /// </summary>
        public List<int> ListID { get; set; }
        /// <summary>
        /// 题型
        /// </summary>
        public string CharpterName { get; set; }
         
        /// <summary>
        /// 题目类型
        /// </summary>
        public string StrStructType { get; set; }
        /// <summary>
        /// 老师是否批改过了
        /// </summary>
        public bool ReviseResult { get; set; }
        /// <summary>
        /// 只读客观题
        /// </summary>
        public bool IsReadSub { get; set; }

        /// <summary>
        /// 是否屏蔽
        /// </summary>		
        public bool IsHiden { get; set; }

        /// <summary>
        /// 相同题型名称的ID组合
        /// </summary>
        public string strIdList { get; set; }

        /// <summary>
        /// 是否答题
        /// </summary>		
        public bool IsDaTi { get; set; }
    }
}