using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///试卷分数结构
    /// </summary>
    public class PaperScoreVM
    {
        public PaperScoreVM()
        {
            ChapterListId = new List<int>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>		
        public int PaperID { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>		
        public string CharpterID { get; set; }

        /// <summary>
        /// 当前章节题目数
        /// </summary>		
        public int Count { get; set; }

        /// <summary>
        /// 每题分值
        /// </summary>		
        public decimal Score { get; set; }
        /*--------------------------------扩展字段-------------------------------------*/
        /// <summary>
        /// 章节名称
        /// </summary>
        public string CharpterName { get; set; }
        /// <summary>
        /// 选中的题库数量
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 题型名称
        /// </summary>
        public string QuestionsName { get; set; }
        /// <summary>
        /// 对应的多个单选多选这些ID
        /// </summary>
        public List<int> ChapterListId { get; set; }

        /// <summary>
        /// 对应所有的题型Id
        /// </summary>
        public List<QuestionVM> QuestionsListId { get; set; }

        /// <summary>
        /// 题型stringid
        /// </summary>
        public List<string> StrListId { get; set; }

        /// <summary>
        /// 用户答题内容
        /// </summary>
        public List<PaperUserAnswerVM> UserAnswer { get; set; }
        /// <summary>
        /// 用户答题得分
        /// </summary>
        public PaperUserAnswerResultVM UserAnswerResult { get; set; }
        /// <summary>
        /// 是否有单选题
        /// </summary>
        public bool IsRadio { get; set; }

    }
}