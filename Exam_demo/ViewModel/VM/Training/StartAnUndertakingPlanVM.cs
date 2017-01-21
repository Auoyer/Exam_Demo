using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///StartAnUndertakingPlan
    /// </summary>
    public class StartAnUndertakingPlanVM
    {
        public StartAnUndertakingPlanVM()
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
        /// 当前年龄
        /// </summary>		
        public int Age { get; set; }

        /// <summary>
        /// 计划创业年龄
        /// </summary>		
        public int StartPlanAge { get; set; }

        /// <summary>
        /// 离创业年限
        /// </summary>		
        public int DistanceYear { get; set; }

        /// <summary>
        /// 创业时一次性投入
        /// </summary>		
        public decimal CostInput { get; set; }

        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        public decimal ReturnOnInvestmentRate { get; set; }

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
        /// 创业规划分析
        /// </summary>		
        public string Analysis { get; set; }

    }
}