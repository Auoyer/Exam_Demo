using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 理论考核试卷
    /// </summary>
    [DataContract]
    public class Paper
    {
        public Paper()
        {
            UserAnswerResult = new List<PaperUserAnswerResult>();
            UserAnswer = new List<PaperUserAnswer>();
            UserSummary = new List<PaperUserSummary>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>		
        [DataMember]
        public string ExamPaperName { get; set; }

        /// <summary>
        /// 题库（弃用）1.理论考试、2.银行从业人员资格认证、3.理财规划师资格认证
        /// </summary>		
        [DataMember]
        public int LibraryID { get; set; }

        /// <summary>
        /// 组卷方式：1.自动组卷、2.手动组卷
        /// </summary>		
        [DataMember]
        public int FormType { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 试卷状态: 0.未发布、1.发布、2.已结束、3.已删除
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 考试开始时间
        /// </summary>		
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 考试结束时间
        /// </summary>		
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>		
        [DataMember]
        public decimal TotalScore { get; set; }       

        /*=======================自定义分界线=====================*/
        /// <summary>
        /// 章节信息
        /// </summary>
        [DataMember]
        public List<PaperCharpter> CharpterList { get; set; }
        /// <summary>
        /// 题目信息
        /// </summary>
        [DataMember]
        public List<PaperDetail> Details { get; set; }
        /// <summary>
        /// 分数信息
        /// </summary>
        [DataMember]
        public List<PaperScore> ScoreInfo { get; set; }
        /// <summary>
        /// 班级信息（弃用）
        /// </summary>
        [DataMember]
        public List<PaperClass> ClassList { get; set; }
        /// <summary>
        /// 竞赛信息
        /// </summary>
        [DataMember]
        public List<PaperCompetition> CompetitionList { get; set; }
        /// <summary>
        /// 用户答题内容
        /// </summary>
        [DataMember]
        public List<PaperUserAnswer> UserAnswer { get; set; }
        /// <summary>
        /// 用户答题得分
        /// </summary>
        [DataMember]
        public List<PaperUserAnswerResult> UserAnswerResult { get; set; }
        /// <summary>
        /// 用户这张卷子的总分
        /// </summary>
        [DataMember]
        public List<PaperUserSummary> UserSummary { get; set; }

        /// <summary>
        /// 单个答题人这张卷子的总分（扩展字段）
        /// </summary>
        [DataMember]
        public decimal UserSumScore { get; set; }


        #region 私人字段 理论成绩管理用
        /// <summary>
        /// 已评分人数
        /// </summary>
        [DataMember]
        public int AlrEvaluatePerson { get; set; }

        /// <summary>
        /// 未评分人数
        /// </summary>
        [DataMember]
        public int NoEvaluatePerson { get; set; }

        #endregion
    }
}