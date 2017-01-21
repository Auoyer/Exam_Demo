using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Heritage
    /// </summary>
    public class HeritageVM
    {
        public HeritageVM()
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
        /// 现金
        /// </summary>		
        public decimal Cash { get; set; }

        /// <summary>
        /// 银行存款
        /// </summary>		
        public decimal Deposit { get; set; }

        /// <summary>
        /// 人寿保单赔偿金额
        /// </summary>		
        public decimal LifeInsurance { get; set; }

        /// <summary>
        /// 其他现金账户
        /// </summary>		
        public decimal OtherCashAccount { get; set; }

        /// <summary>
        /// 股票
        /// </summary>		
        public decimal Stock { get; set; }

        /// <summary>
        /// 债券
        /// </summary>		
        public decimal Bond { get; set; }

        /// <summary>
        /// 基金
        /// </summary>		
        public decimal Fund { get; set; }

        /// <summary>
        /// 其他投资收益
        /// </summary>		
        public decimal OtherInvestment { get; set; }

        /// <summary>
        /// 养老金（一次性收入现值）
        /// </summary>		
        public decimal Pension { get; set; }

        /// <summary>
        /// 配偶/遗孤年金收益（现值）
        /// </summary>		
        public decimal AnnuityRevenue { get; set; }

        /// <summary>
        /// 其他退休基金
        /// </summary>		
        public decimal OtherPension { get; set; }

        /// <summary>
        /// 房产
        /// </summary>		
        public decimal House { get; set; }

        /// <summary>
        /// 汽车
        /// </summary>		
        public decimal Car { get; set; }

        /// <summary>
        /// 其他个人资产
        /// </summary>		
        public decimal Other { get; set; }

        /// <summary>
        /// 其他资产
        /// </summary>		
        public decimal OtherProperty { get; set; }

        /// <summary>
        /// 资产总计
        /// </summary>		
        public decimal TotalProperty { get; set; }

        /// <summary>
        /// 短期贷款
        /// </summary>		
        public decimal ShortTermLoan { get; set; }

        /// <summary>
        /// 中期贷款
        /// </summary>		
        public decimal MediumTermLoans { get; set; }

        /// <summary>
        /// 长期贷款
        /// </summary>		
        public decimal LongTermLoan { get; set; }

        /// <summary>
        /// 其他贷款
        /// </summary>		
        public decimal OtherLoan { get; set; }

        /// <summary>
        /// 临终医疗费用
        /// </summary>		
        public decimal MedicalCosts { get; set; }

        /// <summary>
        /// 预期收入纳税额支出
        /// </summary>		
        public decimal TaxCosts { get; set; }

        /// <summary>
        /// 丧葬费用
        /// </summary>		
        public decimal FuneralExpenses { get; set; }

        /// <summary>
        /// 遗产处置费用
        /// </summary>		
        public decimal HeritageCosts { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>		
        public decimal OtherCosts { get; set; }

        /// <summary>
        /// 其他负债
        /// </summary>		
        public decimal OtherLiabilities { get; set; }

        /// <summary>
        /// 负债总计
        /// </summary>		
        public decimal TotalLiabilities { get; set; }

        /// <summary>
        /// 财务分析
        /// </summary>		
        public string FinanceAnalysis { get; set; }

        /// <summary>
        /// 财产传承规划工具
        /// </summary>		
        public int PlanTool { get; set; }

        /// <summary>
        /// 财产传承规划分析
        /// </summary>		
        public string PlanAnalysis { get; set; }

    }
}