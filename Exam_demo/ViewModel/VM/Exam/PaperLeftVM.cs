using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VM.Custom;

namespace VM
{
    [Serializable]
    public class PaperLeftVM
    {

        public PaperLeftVM()
        {
            PageIndex = 1;
            PageCount = 1;
            PageSize = 100;
            QuestionTypes = new List<TheoryQuestionTypeVM>();
            Details = new List<PaperDetailVM>();
            Question = new List<QuestionVM>();
            ExamFirstQuestion = new ExamShowVM();
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string PaperName { get; set; }
        /// <summary>
        /// 考试时间
        /// </summary>
        public string strTime { get; set; }

        /// <summary>
        /// 题数统计
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 试卷总分
        /// </summary>
        public decimal ExamPagerScore { get; set; }
        /// <summary>
        /// 考题类型
        /// </summary>
        public List<TheoryQuestionTypeVM> QuestionTypes { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public List<PaperDetailVM> Details { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public List<QuestionVM> Question { get; set; }

        /// <summary>
        /// 试卷结果
        /// </summary>
        public List<PaperUserAnswerResultVM> PUserAnswerResult { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNo { get; set; }
        
        /// <summary>
        /// 首题信息
        /// </summary>
        public ExamShowVM ExamFirstQuestion { get; set; }

        /// <summary>
        /// 考试剩余时间
        /// </summary>
        public double RestTime { get; set; }
    }
}
