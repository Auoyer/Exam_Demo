﻿using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TaxPlan
    /// </summary>
    public class TaxPlanVM
    {
        public TaxPlanVM()
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
        /// 工资、薪金所得
        /// </summary>		
        public decimal Salary { get; set; }

        /// <summary>
        /// 工资、薪金所得税
        /// </summary>		
        public decimal SalaryTax { get; set; }

        /// <summary>
        /// 个体工商户的生产、经营所得
        /// </summary>		
        public decimal OperatingRevenue { get; set; }

        /// <summary>
        /// 个体工商户的生产、经营所得税
        /// </summary>		
        public decimal OperatingRevenueTax { get; set; }

        /// <summary>
        /// 对企事业单位承包、承租经营所得
        /// </summary>		
        public decimal EnterprisesRevenue { get; set; }

        /// <summary>
        /// 对企事业单位承包、承租经营所得税
        /// </summary>		
        public decimal EnterprisesRevenueTax { get; set; }

        /// <summary>
        /// 劳务报酬所得
        /// </summary>		
        public decimal ServiceIncome { get; set; }

        /// <summary>
        /// 劳务报酬所得税
        /// </summary>		
        public decimal ServiceIncomeTax { get; set; }

        /// <summary>
        /// 稿酬所得
        /// </summary>		
        public decimal Remuneration { get; set; }

        /// <summary>
        /// 稿酬所得税
        /// </summary>		
        public decimal RemunerationTax { get; set; }

        /// <summary>
        /// 特许权使用费所得
        /// </summary>		
        public decimal Loyalities { get; set; }

        /// <summary>
        /// 特许权使用费所得税
        /// </summary>		
        public decimal LoyalitiesTax { get; set; }

        /// <summary>
        /// 财产转让所得
        /// </summary>		
        public decimal Demise { get; set; }

        /// <summary>
        /// 财产转让所得税
        /// </summary>		
        public decimal DemiseTax { get; set; }

        /// <summary>
        /// 偶然所得
        /// </summary>		
        public decimal IncidentalIncome { get; set; }

        /// <summary>
        /// 偶然所得税
        /// </summary>		
        public decimal IncidentalIncomeTax { get; set; }

        /// <summary>
        /// 利息、红利、股利所得
        /// </summary>		
        public decimal Interest { get; set; }

        /// <summary>
        /// 利息、红利、股利所得税
        /// </summary>		
        public decimal InterestTax { get; set; }

        /// <summary>
        /// 合计
        /// </summary>		
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 合计税
        /// </summary>		
        public decimal TotalTax { get; set; }

        /// <summary>
        /// 税收筹划分析
        /// </summary>		
        public string Analysis { get; set; }

    }
}