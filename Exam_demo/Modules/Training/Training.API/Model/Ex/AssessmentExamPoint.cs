using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    /// <summary>
    /// 所有的考核点类
    /// </summary>
  [DataContract]
    public class AssessmentExamPoint
    {
      public AssessmentExamPoint()
      {

      }

      /// <summary>
      /// 考核点的AssessmentResultsDetail的ID
      /// </summary>	
         [DataMember]
      public int Id { get; set; }
      /// <summary>
      /// 实训考核ID
      /// </summary>
      [DataMember]
      public int TrainExamId { get; set; }
      /// <summary>
      /// 班级ID
      /// </summary>
        [DataMember]
      public int CompetitionId { get; set; }
      /// <summary>
      /// 用户ID
      /// </summary>
        [DataMember]
      public int UserId { get; set; }

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
        /// 考核点类型
        /// </summary>
        [DataMember]
        public int ExamPointType { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>	
        [DataMember]
        public int ModularId { get; set; }

        /// <summary>
        /// 考核点
        /// </summary>
        [DataMember]
        public int AssessmentPoint { get; set; }

        /// <summary>
        /// 状态
        /// </summary>	
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 得分
        /// </summary>	
        [DataMember]
        public decimal Score { get; set; }

    }
}
