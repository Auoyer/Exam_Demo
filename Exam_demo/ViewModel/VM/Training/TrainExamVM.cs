using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TrainExam
    /// </summary>
    public class TrainExamVM
    {
        public TrainExamVM()
        {
            TrainExamDetail = new List<TrainExamDetailVM>();
            TrainExamClass = new List<TrainExamClassVM>();
            TrainExamCompetition = new List<TrainExamCompetitionVM>();
            AddTrainExamClassIds = new List<int>();
            DelTrainExamClassIds = new List<int>();
            AddTrainExamPointIds = new List<int>();
            DelTrainExamPointIds = new List<int>();
            UpdateTrainExamDetail = new List<TrainExamDetailVM>();
            ExamCase = new List<ExamCaseVM>();
            AddTrainExamDetail = new List<TrainExamDetailVM>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>		
        public int CompetitionId { get; set; }

        /// <summary>
        /// 竞赛名称
        /// </summary>		
        public string StrCompetition { get; set; }

        /// <summary>
        /// 考核名称TrainExamName
        /// </summary>
        public string TrainExamName { get; set; }

        /// <summary>
        /// 案例Id
        /// </summary>		
        public int ExamCaseId { get; set; }

        /// <summary>
        /// 发布用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 发布用户Name
        /// </summary>		
        public string UserName { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>		
        public string ClassName { get; set; }

        /// <summary>
        /// 状态(0：未发布；1：已发布；2：已删除)
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 案例ID
        /// </summary>		
        public int CaseId { get; set; }

        /// <summary>
        /// 考核类型（销售机会、实训考核）
        /// </summary>		
        public int ExamTypeId { get; set; }

        /// <summary>
        /// 评分状态
        /// </summary>		
        public int TrainExamStatus { get; set; }

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
        public decimal AllScore { get; set; }

        /*=========================================================*/

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
        /// 考核点和销售机会关联表集合
        /// </summary>
        public List<TrainExamDetailVM> TrainExamDetail { get; set; }

        /// <summary>
        /// 班级和销售机会关联表集合
        /// </summary>
        public List<TrainExamClassVM> TrainExamClass { get; set; }

        /// <summary>
        /// 大赛和实训考试关联表集合
        /// </summary>
        public List<TrainExamCompetitionVM> TrainExamCompetition { get; set; }

        /// <summary>
        /// 案例
        /// </summary>
        public List<ExamCaseVM> ExamCase { get; set; }

        /// <summary>
        /// 案例 hzq
        /// </summary>
        public CaseVM Case { get; set; }

        /// <summary>
        /// 需要新增的销售机会/实训考核班级关联ID集合
        /// </summary>
        public List<int> AddTrainExamClassIds { get; set; }

        /// <summary>
        ///  需要修改的销售机会/实训考核考核点关联ID集合
        /// </summary>
        public List<TrainExamDetailVM> AddTrainExamDetail { get; set; }

        /// <summary>
        /// 需要删除的销售机会/实训考核班级关联ID集合
        /// </summary>
        public List<int> DelTrainExamClassIds { get; set; }

        /// <summary>
        /// 需要新增的销售机会/实训考核考核点关联ID集合
        /// </summary>
        public List<int> AddTrainExamPointIds { get; set; }

        /// <summary>
        /// 需要删除的销售机会/实训考核考核点关联ID集合
        /// </summary>
        public List<int> DelTrainExamPointIds { get; set; }

        /// <summary>
        ///  需要修改的销售机会/实训考核考核点关联ID集合
        /// </summary>
        public List<TrainExamDetailVM> UpdateTrainExamDetail { get; set; }

    }
}