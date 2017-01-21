using System;
using System.Collections.Generic;
using System.Globalization;
using VM.Custom;

namespace VM
{
    /// <summary>
    ///试卷
    /// </summary>
    [Serializable]
    public class PaperVM
    {
        public PaperVM()
        {
            Details = new List<PaperDetailVM>();
            DetailsSort = new List<PaperDetailVM>();
            DetailsRandom = new List<PaperDetailVM>();
            UserAnswerResult = new List<PaperUserAnswerResultVM>();
            UserAnswer = new List<PaperUserAnswerVM>();
            UserSummary = new List<PaperUserSummaryVM>();
            PaperQuestionType = new List<TheoryQuestionTypeVM>();
            PaperQuestions = new List<QuestionVM>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>		
        public string ExamPaperName { get; set; }

        /// <summary>
        /// 枚举：自动组卷、手动组卷
        /// </summary>		 
        public int FormType { get; set; }

        /// <summary>
        /// 题库（弃用）1.理论考试、2.银行从业人员资格认证、3.理财规划师资格认证
        /// </summary>		
        public int LibraryID { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>		
        public int CompetitionId { get; set; }

        /// <summary>
        /// 竞赛名称
        /// </summary>		
        public string StrCompetition { get; set; }

        /// <summary>
        /// 试卷状态: 0.未发布、1.发布、2.已结束、3.已删除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>		
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 总分
        /// </summary>		
        public decimal TotalScore { get; set; }



        /*=======================自定义分界线=====================*/
        /// <summary>
        /// 章节信息
        /// </summary>
        public List<PaperCharpterVM> CharpterList { get; set; }

        /// <summary>
        /// 题目信息
        /// </summary>
        public List<PaperDetailVM> Details { get; set; }

        /// <summary>
        /// 随机题目信息
        /// </summary>
        public List<PaperDetailVM> DetailsRandom { get; set; }

        /// <summary>
        /// 顺序题目信息
        /// </summary>
        public List<PaperDetailVM> DetailsSort { get; set; }

        /// <summary>
        /// 分数信息
        /// </summary>
        public List<PaperScoreVM> ScoreInfo { get; set; }

        /// <summary>
        /// 竞赛信息
        /// </summary>
        public List<PaperCompetitionVM> CompetitionList { get; set; }

        /// <summary>
        /// 用户答题内容
        /// </summary>
        public List<PaperUserAnswerVM> UserAnswer { get; set; }
        /// <summary>
        /// 用户答题得分
        /// </summary>
        public List<PaperUserAnswerResultVM> UserAnswerResult { get; set; }
        /// <summary>
        /// 用户这张卷子的总分
        /// </summary>
        public List<PaperUserSummaryVM> UserSummary { get; set; }
       
        /// <summary>
        /// 试卷试题显示扩展
        /// </summary>
        public List<ExamShowVM> ExamShow { get; set; }

        /// <summary>
        /// 试卷题型
        /// </summary>
        public List<TheoryQuestionTypeVM> PaperQuestionType { get; set; }

        /// <summary>
        /// 试卷习题
        /// </summary>
        public List<QuestionVM> PaperQuestions { get; set; }
        
        /// <summary>
        /// 单个答题人这张卷子的总分（扩展字段）
        /// </summary>
        public decimal UserSumScore { get; set; }
      

        /// <summary>
        /// 开始时间字符串
        /// </summary>
        public string strStartDate
        {
            get
            {
                return StartDate.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 结束时间字符串
        /// </summary>
        public string strEndDate
        {
            get
            {
                return EndDate.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }
        
        
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; } 

        /// <summary>
        /// 学生端理论考核状态名称
        /// </summary>
        public string TrainExamStateName
        {
            get
            {
                string state = string.Empty;
                if (DateTime.Now > EndDate)
                {
                    //当前时间大于结束时间
                    state = "已结束";
                }
                if (DateTime.Now >= StartDate && DateTime.Now <= EndDate)
                {
                    //当前时间大于开始时间,当前时间小于结束时间
                    state = "已开始";
                }
                if (DateTime.Now < StartDate)
                {
                    state = "未开始";
                }
                return state;
            }
            private set { }
        } 
        /// <summary>
        /// 考试时长
        /// </summary>
        public double ExamTime { get; set; } 

     

        private string _examTimeLong;
        /// <summary>
        /// 直接获取时间长度
        /// </summary>
        public string ExamTimeLong {
            get
            {

                TimeSpan tim = (EndDate - StartDate);
               _examTimeLong= tim.TotalMinutes.ToString();
               if (_examTimeLong.IndexOf('.') != -1)
               {
                   return _examTimeLong.Substring(0, _examTimeLong.IndexOf('.'));
               }
               return _examTimeLong;
            }
        }

        /// <summary>
        /// 未评分人数
        /// </summary>
        public int NoEvaluatePerson { get; set; }
        /// <summary>
        /// 已经评分人数
        /// </summary>
        public int AlrEvaluatePerson { get; set; }

        
       
    }
}