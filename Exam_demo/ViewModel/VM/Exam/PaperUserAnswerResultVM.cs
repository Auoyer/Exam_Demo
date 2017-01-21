using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///用户答题结果分析
    /// </summary>
   [Serializable]
    public class PaperUserAnswerResultVM
    {
        public PaperUserAnswerResultVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 问题Id
        /// </summary>		
        public int QuesionId { get; set; }

        /// <summary>
        /// 题型Id
        /// </summary>		
        public int QuestionTypeId { get; set; }

        /// <summary>
        /// 结构类型（枚举）
        /// </summary>		
        public int StructType { get; set; }

        /// <summary>
        /// 每题分值
        /// </summary>		
        public decimal QuestionScore { get; set; }

        /// <summary>
        /// 结果
        /// </summary>		
        public int Result { get; set; }

        /// <summary>
        /// 用户得分
        /// </summary>		
        public decimal? UserScore { get; set; }

        /// <summary>
        /// 教师评析(简答)
        /// </summary>		
        public string Analyse { get; set; }

        /// <summary>
        /// 是否标记
        /// </summary>		
        public bool IsMark { get; set; }

        
        /*-----------------------------------------------扩展字段-------------------------------------------------------*/
        /// <summary>
        /// 问题Id
        /// </summary>		
        public string QuesionName { get; set; }

        /// <summary>
        /// 题型
        /// </summary>		
        public string QuestionTypeName{ get; set; }


        /// <summary>
        /// 结果
        /// </summary>		
        public string StrResult 
        {
            get {
                if (Result == 2)
                {
                    return "正确";
                }
                else
                {
                    return "错误";
                } 
            }
            set { }
        }
    }
}