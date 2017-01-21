using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///LifeEducationPlanDetail
    /// </summary>
    public class LifeEducationPlanDetailVM
    {
        public LifeEducationPlanDetailVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        public int ProposalId { get; set; }

        /// <summary>
        /// 教育阶段
        /// </summary>		
        public int EduStage { get; set; }

        /// <summary>
        /// 求学年龄
        /// </summary>		
        public int EduAge { get; set; }

        /// <summary>
        /// 求学时间
        /// </summary>		
        public int EduTime { get; set; }

        /// <summary>
        /// 目前学费
        /// </summary>		
        public decimal Tuition { get; set; }

        /// <summary>
        /// 上学时学费
        /// </summary>		
        public decimal EduTuition { get; set; }

        /// <summary>
        /// 上学前需准备的总学费
        /// </summary>		
        public decimal TotalTuition { get; set; }

    }
}