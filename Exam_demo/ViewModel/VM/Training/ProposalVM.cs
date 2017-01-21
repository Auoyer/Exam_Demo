using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    /// 建议书
    /// </summary>
    public class ProposalVM
    {
        public ProposalVM()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 实训考核/销售机会Id
        /// </summary>		
        public int TrainExamId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 建议书编号
        /// </summary>		
        public string ProposalNum { get; set; }

        /// <summary>
        /// 建议书名称
        /// </summary>		
        public string ProposalName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>		
        public DateTime UpdateDate { get; set; }


        #region 扩展字段
        /// <summary>
        /// (扩展字段)建议书客户信息是否保存
        /// </summary>
        public bool IsCustomer
        {
            get
            {
                if (ProposalCustomerVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)风险测评是否保存
        /// </summary>
        public bool IsRisk
        {
            get
            {
                if (RiskIndexVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)财务分析是否保存
        /// </summary>
        public bool IsFinancial
        {
            get
            {
                if (LiabilityVM != null || IncomeAndExpensesVM != null || CashFlowVM != null || FinancialRatiosVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)现金规划是否保存
        /// </summary>
        public bool IsCashPlan
        {
            get
            {
                if (CashPlanVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)生涯规划是否保存
        /// </summary>
        public bool IsLife
        {
            get
            {
                if (LifeEducationPlanVM != null || ConsumptionPlanVM != null || StartAnUndertakingPlanVM != null || RetirementPlanVM != null || InsurancePlanVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)投资规划是否保存
        /// </summary>
        public bool IsInvestmentPlan
        {
            get
            {
                if (InvestmentPlanVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)税收筹划是否保存
        /// </summary>
        public bool IsTaxPlan
        {
            get
            {
                if (TaxPlanVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)财产分配表是否保存
        /// </summary>
        public bool IsDistributionOfProperty
        {
            get
            {
                if (DistributionOfPropertyVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)财产传承表是否保存
        /// </summary>
        public bool IsHeritage
        {
            get
            {
                if (HeritageVM != null)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        /// <summary>
        /// (扩展字段)建议书客户信息
        /// </summary>
        public ProposalCustomerVM ProposalCustomerVM { get; set; } 
        /// <summary>
        /// (扩展字段)风险测评
        /// </summary>
        public RiskIndexVM RiskIndexVM { get; set; }
        /// <summary>
        /// (扩展字段)财务分析-资产负债表
        /// </summary>
        public LiabilityVM LiabilityVM { get; set; }
        /// <summary>
        /// (扩展字段)财务分析-收支储蓄表
        /// </summary>
        public IncomeAndExpensesVM IncomeAndExpensesVM { get; set; }
        /// <summary>
        /// (扩展字段)财务分析-现金流量表
        /// </summary>
        public CashFlowVM CashFlowVM { get; set; }
        /// <summary>
        /// (扩展字段)财务分析-财务比率分析
        /// </summary>
        public FinancialRatiosVM FinancialRatiosVM { get; set; }
        /// <summary>
        /// (扩展字段)现金规划
        /// </summary>
        public CashPlanVM CashPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)生涯规划-教育规划
        /// </summary>
        public LifeEducationPlanVM LifeEducationPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)生涯规划-消费规划表
        /// </summary>
        public ConsumptionPlanVM ConsumptionPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)生涯规划-创业规划
        /// </summary>
        public StartAnUndertakingPlanVM StartAnUndertakingPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)生涯规划-退休规划
        /// </summary>
        public RetirementPlanVM RetirementPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)生涯规划-保险规划
        /// </summary>
        public InsurancePlanVM InsurancePlanVM { get; set; }
        /// <summary>
        /// (扩展字段)投资规划
        /// </summary>
        public InvestmentPlanVM InvestmentPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)税收筹划
        /// </summary>
        public TaxPlanVM TaxPlanVM { get; set; }
        /// <summary>
        /// (扩展字段)财产分配表
        /// </summary>
        public DistributionOfPropertyVM DistributionOfPropertyVM { get; set; }
        /// <summary>
        /// (扩展字段)财产传承表
        /// </summary>
        public HeritageVM HeritageVM { get; set; }


        /// <summary>
        /// (扩展字段)考核案例
        /// </summary>
        public ExamCaseVM ExamCaseVM { get; set; }

        /// <summary>
        /// (扩展字段)潜在客户ID
        /// </summary>
        public int StuCustomerId { get; set; }

        #endregion



    }
}