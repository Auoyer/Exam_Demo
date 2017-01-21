using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///LifeEducationPlan
    /// </summary>
    public class LifeEducationPlanVM
    {
        public LifeEducationPlanVM()
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
        /// 子女年龄
        /// </summary>		
        public int ChildAge { get; set; }

        /// <summary>
        /// 国内学费增长率
        /// </summary>		
        public decimal InlandEduFee { get; set; }

        /// <summary>
        /// 国外学费增长率
        /// </summary>		
        public decimal ForeignEduFee { get; set; }

        /// <summary>
        /// 商业保险
        /// </summary>		
        public decimal Insurance { get; set; }

        /// <summary>
        /// 储蓄计划
        /// </summary>		
        public decimal Deposit { get; set; }

        /// <summary>
        /// 其他安排
        /// </summary>		
        public decimal Other { get; set; }

        /// <summary>
        /// 上学前需准备的现金总金额
        /// </summary>		
        public decimal EduTotalAmount { get; set; }

        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        public decimal ReturnOnInvestment { get; set; }

        /// <summary>
        /// 一次性投入金额
        /// </summary>		
        public decimal DisposableInput { get; set; }

        /// <summary>
        /// 每月定期投资金额
        /// </summary>		
        public decimal MonthlyInvestment { get; set; }

        /// <summary>
        /// 定期定额投资年限
        /// </summary>		
        public int RegularYear { get; set; }

        /// <summary>
        /// 此方案能实现的目标金额
        /// </summary>		
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// 教育规划分析
        /// </summary>		
        public string Analysis { get; set; }

        /// <summary>
        /// 教育计划详细信息
        /// </summary>
        public List<LifeEducationPlanDetailVM> LifeEducationPlanDetailList { get; set; }

    }
}