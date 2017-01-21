using System.Runtime.Serialization;

namespace Training.API
{
    [DataContract]
    /// <summary>
    /// 考核明细
    /// </summary>
    public class AssessmentResultsDetail
    {
        /// <summary>
        /// Id
        /// </summary>	
        [DataMember]	
        public int Id { get; set; }

        /// <summary>
        /// 考核结果主表
        /// </summary>	
        [DataMember]	
        public int AssessmentResultsId { get; set; }
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
