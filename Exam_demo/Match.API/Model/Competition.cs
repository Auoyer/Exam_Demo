using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    /// 竞赛信息
    /// </summary>
    [DataContract]
    public class Competition
    {
        public Competition()
        {
        }

        /// <summary>
        /// Id
        /// </summary>	
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// CollegeId
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 大赛名称
        /// </summary>		
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>		
        [DataMember]
        public int AddUserId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>		
        [DataMember]
        public string AddUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime? AddTime { get; set; }


        /// <summary>
        /// 竞赛类型；1=单项理论赛；2=单项实训赛；3=复合赛
        /// </summary>		
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// 竞赛进行状态；A.当比赛类型为【单项理论赛】时候，存在如下几种状态：100=未开始、101=报名中、102=待比赛、103=比赛中、104=成绩待发布、105=比赛结束。B.当比赛类型为【单项实训赛】时候，存在如下几种状态：200=未开始、201=报名中、202=待比赛、203=比赛中、204=待评分、205=评分中、206=成绩待发布、207=比赛结束C.当比赛类型为【复合比赛】时候，存在如下几种状态：300=未开始、301=报名中、302=待初赛、303=初赛中、304=成绩待发布（初赛）、305=待复赛、306=复赛中、307=待评分、308=评分中、309=成绩待发布、310=比赛结束
        /// </summary>		
        [DataMember]
        public int State { get; set; }

        /// <summary>
        /// 报名开始时间
        /// </summary>		
        [DataMember]
        public DateTime? RegistrationStartTime { get; set; }


        /// <summary>
        /// 报名结束时间
        /// </summary>		
        [DataMember]
        public DateTime? RegistrationEndTime { get; set; }

        /// <summary>
        /// 初赛开始时间；若为单项赛，初赛时间即为比赛时间；   若为复合赛，初赛时间即为初赛时间；
        /// </summary>		
        [DataMember]
        public DateTime? PreliminaryStartTime { get; set; }

        /// <summary>
        /// 初赛结束时间；若为单项赛，初赛时间即为比赛时间；   若为复合赛，初赛时间即为初赛时间；
        /// </summary>		
        [DataMember]
        public DateTime? PreliminaryEndTime { get; set; }


        /// <summary>
        /// 复赛开始时间；复合赛才有复赛时间，单项赛不需要复赛时间
        /// </summary>		
        [DataMember]
        public DateTime? RematchStartTime { get; set; }

        /// <summary>
        /// 复赛结束时间；复合赛才有复赛时间，单项赛不需要复赛时间
        /// </summary>		
        [DataMember]
        public DateTime? RematchEndTime { get; set; }

        /// <summary>
        /// 初赛成绩发布类型；1=手动发布（比赛结束时手动点击发布按钮）；2=自动发布（系统到时间自动发布）
        /// </summary>		
        [DataMember]
        public int PreliminaryResultType { get; set; }

        /// <summary>
        /// 初赛成绩发布时间
        /// </summary>		
        [DataMember]
        public DateTime? PreliminaryResultTime { get; set; }


        /// <summary>
        /// 复赛成绩发布类型（只有复合赛才使用此字段）；1=手动发布（比赛结束时手动点击发布按钮）；2=自动发布（系统到时间自动发布）
        /// </summary>		
        [DataMember]
        public int RematchResultType { get; set; }

        /// <summary>
        /// 复赛成绩发布时间（只有复合赛才使用此字段）
        /// </summary>		
        [DataMember]
        public DateTime? RematchResultTime { get; set; }


        /// <summary>
        /// ScoreStartTime
        /// </summary>		
        [DataMember]
        public DateTime? ScoreStartTime { get; set; }


        /// <summary>
        /// ScoreEndTime
        /// </summary>		
        [DataMember]
        public DateTime? ScoreEndTime { get; set; }


        /// <summary>
        /// 竞赛须知
        /// </summary>		
        [DataMember]
        public string Information { get; set; }

        /// <summary>
        /// 复赛入围人（组）数，只有复合赛才使用此字段
        /// </summary>		
        [DataMember]
        public int FinalistNumber { get; set; }

        /// <summary>
        /// 是否发布；1=已发布，0=未发布，2=已结束
        /// </summary>		
        [DataMember]
        public int IsRelease { get; set; }

        /// <summary>
        /// 是否删除；1=竞赛管理员删除，0=正常，2=超管删除
        /// </summary>		
        [DataMember]
        public int IsDelete { get; set; }

        /// <summary>
        /// 关联竞赛评委ID
        /// </summary>
        [DataMember]
        public List<int> ListJudgeId { get; set; }

        /// <summary>
        /// 当前考生状态
        /// </summary>
        [DataMember]
        public V_MatchUser CurUserInfo { get; set; }

        /// <summary>
        /// 当前考生考试状态
        /// </summary>
        [DataMember] 
        public V_MatchResult CurUserMRInfo { get; set; }

        /// <summary>
        /// 当前用户理论考试结果
        /// </summary>
        [DataMember]
        public PaperUserSummary CurUserPUResult { get; set; }

        /// <summary>
        /// 当前用户实训考试结果
        /// </summary>
        [DataMember] 
        public AssessmentResults CurUserARResult { get; set; }

        [DataMember] 
        public int? IsAudit { get; set; }

        #region 竞赛列表获取当前用户考试情况，并防止循环引用
        /// <summary>
        ///用户试卷得分情况
        /// </summary>
        [DataContract]
        public class PaperUserSummary
        {
            /// <summary>
            /// Id
            /// </summary>		
            [DataMember]
            public int Id { get; set; }

            /// <summary>
            /// 试卷Id
            /// </summary>		
            [DataMember]
            public int ExamPaperId { get; set; }

            /// <summary>
            /// 用户Id
            /// </summary>		
            [DataMember]
            public int UserId { get; set; }

            /// <summary>
            /// 用户所属竞赛Id
            /// </summary>		
            [DataMember]
            public int CompetitionId { get; set; }

            /// <summary>
            /// 用户所属班级Id（弃用）
            /// </summary>		
            [DataMember]
            public int ClassId { get; set; }

            /// <summary>
            /// 待评分数目（弃用）
            /// </summary>		
            [DataMember]
            public int UnScoredCount { get; set; }

            /// <summary>
            /// 试卷总分
            /// </summary>		
            [DataMember]
            public decimal TotalScore { get; set; }

            /// <summary>
            /// 用户得分
            /// </summary>		
            [DataMember]
            public decimal Score { get; set; }

            /// <summary>
            /// 评分状态
            /// </summary>		
            [DataMember]
            public int Status { get; set; }

            /// <summary>
            /// 完成时间
            /// </summary>		
            [DataMember]
            public DateTime? FinishDate { get; set; }

            //////////////////////扩展字段

            /// <summary>
            /// 考核名称
            /// </summary>		
            [DataMember]
            public string ExamPaperName { get; set; }

            /// <summary>
            /// 发布人
            /// </summary>		
            [DataMember]
            public int UId { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>	
            [DataMember]
            public DateTime EndDate { get; set; }

            /// <summary>
            /// 试卷状态
            /// </summary>		
            [DataMember]
            public int Status2 { get; set; }
        }

        /// <summary>
        /// 用户实训考核情况
        /// </summary>
        [DataContract]
        public class AssessmentResults
        {
            /// <summary>
            /// 主键ID
            /// </summary>
            [DataMember]
            public int Id { get; set; }

            /// <summary>
            /// 用户ID
            /// </summary>
            [DataMember]
            public int UserId { get; set; }
            /// <summary>
            /// 班级ID
            /// </summary>
            [DataMember]
            public int CompetitionId { get; set; }
            /// <summary>
            /// 实训考核ID
            /// </summary>
            [DataMember]
            public int TrainExamId { get; set; }

            /// <summary>
            /// 总分
            /// </summary>
            [DataMember]
            public decimal TotalScore { get; set; }

            /// <summary>
            /// 主观分
            /// </summary>
            [DataMember]
            public decimal SubjectiveResults { get; set; }
            /// <summary>
            /// 客观分
            /// </summary>
            [DataMember]
            public decimal ObjectiveResults { get; set; }
            /// <summary>
            /// 考核状态
            /// </summary>
            [DataMember]
            public int TrainExamStatus { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            [DataMember]
            public DateTime CreateTime { get; set; }

        } 
        #endregion
    }
}
