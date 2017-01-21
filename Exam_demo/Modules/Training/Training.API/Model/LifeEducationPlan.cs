using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///LifeEducationPlan
    /// </summary>
    [DataContract]
    public class LifeEducationPlan
    {
        public LifeEducationPlan()
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
        /// 子女年龄
        /// </summary>		
        [DataMember]
        public int ChildAge { get; set; }

        /// <summary>
        /// 国内学费增长率
        /// </summary>		
        [DataMember]
        public decimal InlandEduFee { get; set; }

        /// <summary>
        /// 国外学费增长率
        /// </summary>		
        [DataMember]
        public decimal ForeignEduFee { get; set; }

        /// <summary>
        /// 商业保险
        /// </summary>		
        [DataMember]
        public decimal Insurance { get; set; }

        /// <summary>
        /// 储蓄计划
        /// </summary>		
        [DataMember]
        public decimal Deposit { get; set; }

        /// <summary>
        /// 其他安排
        /// </summary>		
        [DataMember]
        public decimal Other { get; set; }

        /// <summary>
        /// 上学前需准备的现金总金额
        /// </summary>		
        [DataMember]
        public decimal EduTotalAmount { get; set; }

        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        [DataMember]
        public decimal ReturnOnInvestment { get; set; }

        /// <summary>
        /// 一次性投入金额
        /// </summary>		
        [DataMember]
        public decimal DisposableInput { get; set; }

        /// <summary>
        /// 每月定期投资金额
        /// </summary>		
        [DataMember]
        public decimal MonthlyInvestment { get; set; }

        /// <summary>
        /// 定期定额投资年限
        /// </summary>		
        [DataMember]
        public int RegularYear { get; set; }

        /// <summary>
        /// 此方案能实现的目标金额
        /// </summary>		
        [DataMember]
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// 教育规划分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

        /// <summary>
        /// 教育计划详细信息
        /// </summary>
        [DataMember]
        public List<LifeEducationPlanDetail> LifeEducationPlanDetailList { get; set; }

        /// <summary>
        /// 添加教育计划详细信息
        /// </summary>
        [DataMember]
        public List<LifeEducationPlanDetail> AddLifeEducationPlanDetailList { get; set; }

    }
}