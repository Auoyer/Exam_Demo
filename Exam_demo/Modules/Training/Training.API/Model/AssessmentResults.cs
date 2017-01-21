using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
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
        /// <summary>
        /// 考核明细
        /// </summary>
        [DataMember]
        public List<AssessmentResultsDetail> DetailList { get; set; }

        #region 获取考核分数清空


        #endregion

    }
}
