using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///InvestmentPlan
    /// </summary>
    public class InvestmentPlanVM
    {
        public InvestmentPlanVM()
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
        /// 当前客户所处家庭生命周期Id
        /// </summary>		
        public int LifeCycleId { get; set; }

        /// <summary>
        /// 投资规划分析
        /// </summary>		
        public string Analysis { get; set; }

        /// <summary>
        /// 保值层比例
        /// </summary>		
        public decimal HoldRate { get; set; }

        /// <summary>
        /// 增值层比例
        /// </summary>		
        public decimal IncreaseRate { get; set; }

        /// <summary>
        /// 投机层比例
        /// </summary>		
        public decimal SpeculationRate { get; set; }

        /// <summary>
        ///投资规划产品列表
        /// </summary>
        public List<InvestmentPlanProductVM> InvestmentPlanProductList { get; set; }

     
      

    }
}