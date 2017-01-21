using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///LifeEducationPlanDetail
    /// </summary>
    [DataContract]
    public class LifeEducationPlanDetail
    {
        public LifeEducationPlanDetail()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        [DataMember]
        public int ProposalId { get; set; }

        /// <summary>
        /// 教育阶段
        /// </summary>		
        [DataMember]
        public int EduStage { get; set; }

        /// <summary>
        /// 求学年龄
        /// </summary>		
        [DataMember]
        public int EduAge { get; set; }

        /// <summary>
        /// 求学时间
        /// </summary>		
        [DataMember]
        public int EduTime { get; set; }

        /// <summary>
        /// 目前学费
        /// </summary>		
        [DataMember]
        public decimal Tuition { get; set; }

        /// <summary>
        /// 上学时学费
        /// </summary>		
        [DataMember]
        public decimal EduTuition { get; set; }

        /// <summary>
        /// 上学前需准备的总学费
        /// </summary>		
        [DataMember]
        public decimal TotalTuition { get; set; }

    }
}