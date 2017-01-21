using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    /// <summary>
    /// UnitTrainExam
    /// </summary>
    [DataContract]
    public class UnitTrainExam
    {
        public UnitTrainExam()
        {
    
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型Id
        /// </summary>		
        [DataMember]
        public int FinancialTypeId { get; set; }

        /// <summary>
        /// 案例Id
        /// </summary>		
        [DataMember]
        public int CaseId { get; set; }

        /// <summary>
        /// 考核名称TrainExamName
        /// </summary>		
        [DataMember]
        public string TrainExamName { get; set; }

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
        /// 发布用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 领取后的客户Id
        /// </summary>
        [DataMember]
        public Nullable<int> StuCustomerId { get; set; }
        /// <summary>
        /// 建议书Id
        /// </summary>
        [DataMember]
        public Nullable<int> ProposalId { get; set; }

        /// <summary>
        /// 考核类型
        /// </summary>	
        [DataMember]
        public Nullable<int> ExamTypeId { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 考试总分
        /// </summary>
        [DataMember]
        public decimal AllScore { get; set; }

        /// <summary>
        /// 主观题得分
        /// </summary>	
        [DataMember]
        public decimal SubjectiveResults { get; set; }

        /// <summary>
        /// 客观题得分
        /// </summary>	
        [DataMember]
        public decimal ObjectiveResults { get; set; }

        /// <summary>
        /// 评分状态
        /// </summary>
        [DataMember]
        public int ScoreStatus { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 实训班级Id
        /// </summary>
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// 实训状态
        /// </summary>
         [DataMember]
        public int TrainExamStatus { get; set; }
    }

}
