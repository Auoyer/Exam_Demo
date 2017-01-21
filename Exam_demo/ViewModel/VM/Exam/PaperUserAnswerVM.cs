using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///用户试卷答案
    /// </summary>
    [Serializable]
    public class PaperUserAnswerVM
    {
        public PaperUserAnswerVM()
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
        /// 题型
        /// </summary>		
        public int QuesionTypeId { get; set; }

        /// <summary>
        /// 选择答案
        /// </summary>		
        public int? Answer { get; set; }

    }
}