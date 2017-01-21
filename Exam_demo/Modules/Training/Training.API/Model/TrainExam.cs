using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///TrainExam
    /// </summary>
    [DataContract]
    public class TrainExam
    {
        public TrainExam()
        {
            TrainExamDetail = new List<TrainExamDetail>();
            TrainExamClass = new List<TrainExamClass>();
            TrainExamCompetition = new List<TrainExamCompetition>();
            AddTrainExamClassIds = new List<int>();
            DelTrainExamClassIds = new List<int>();
            AddTrainExamPointIds = new List<int>();
            DelTrainExamPointIds = new List<int>();
            UpdateTrainExamDetail = new List<TrainExamDetail>();
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
        /// 考核名称TrainExamName
        /// </summary>
        [DataMember]
        public string TrainExamName { get; set; }

        /// <summary>
        /// 案例Id
        /// </summary>		
        [DataMember]
        public int ExamCaseId { get; set; }

        /// <summary>
        /// 发布用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 状态 //0：未发布 1：发布 2：已删除
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 考核类型（销售机会、实训考核）
        /// </summary>		
        [DataMember]
        public int ExamTypeId { get; set; }


        /// <summary>
        /// 案例ID
        /// </summary>		
        [DataMember]
        public int CaseId { get; set; }

        /// <summary>
        /// 总分
        /// </summary>		
        [DataMember]
        public decimal AllScore { get; set; }

        /// <summary>
        ///平分状态 //1：待评分 2：已评分 2：不显示
        /// </summary>		
        [DataMember]
        public int TrainExamStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>		
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 考核点和销售机会关联表集合
        /// </summary>
        [DataMember]
        public List<TrainExamDetail> TrainExamDetail { get; set; }

        /// <summary>
        /// 班级和销售机会关联表集合
        /// </summary>
        [DataMember]
        public List<TrainExamClass> TrainExamClass { get; set; }

        /// <summary>
        /// 大赛和实训考试关联表集合
        /// </summary>
        [DataMember]
        public List<TrainExamCompetition> TrainExamCompetition { get; set; }

        /// <summary>
        /// 案例
        /// </summary>
        [DataMember]
        public List<ExamCase> ExamCase { get; set; }

        /// <summary>
        /// 案例 hzq
        /// </summary>
        [DataMember]
        public Case Case { get; set; }

        /// <summary>
        /// 需要新增的销售机会/实训考核班级关联ID集合
        /// </summary>
        [DataMember]
        public List<int> AddTrainExamClassIds { get; set; }

        /// <summary>
        /// 需要删除的销售机会/实训考核班级关联ID集合
        /// </summary>
        [DataMember]
        public List<int> DelTrainExamClassIds { get; set; }

        /// <summary>
        /// 需要新增的销售机会/实训考核考核点关联ID集合
        /// </summary>
        [DataMember]
        public List<int> AddTrainExamPointIds { get; set; }

        /// <summary>
        ///  需要修改的销售机会/实训考核考核点关联ID集合
        /// </summary>
        [DataMember]
        public List<TrainExamDetail> AddTrainExamDetail { get; set; }

        /// <summary>
        /// 需要删除的销售机会/实训考核考核点关联ID集合
        /// </summary>
        [DataMember]
        public List<int> DelTrainExamPointIds { get; set; }

        /// <summary>
        ///  需要修改的销售机会/实训考核考核点关联ID集合
        /// </summary>
        [DataMember]
        public List<TrainExamDetail> UpdateTrainExamDetail { get; set; }
    }
}