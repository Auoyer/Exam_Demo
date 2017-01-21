using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///Heritage
    /// </summary>
    [DataContract]
    public class Heritage
    {
        public Heritage()
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
        /// 现金
        /// </summary>		
        [DataMember]
        public decimal Cash { get; set; }

        /// <summary>
        /// 银行存款
        /// </summary>		
        [DataMember]
        public decimal Deposit { get; set; }

        /// <summary>
        /// 人寿保单赔偿金额
        /// </summary>		
        [DataMember]
        public decimal LifeInsurance { get; set; }

        /// <summary>
        /// 其他现金账户
        /// </summary>		
        [DataMember]
        public decimal OtherCashAccount { get; set; }

        /// <summary>
        /// 股票
        /// </summary>		
        [DataMember]
        public decimal Stock { get; set; }

        /// <summary>
        /// 债券
        /// </summary>		
        [DataMember]
        public decimal Bond { get; set; }

        /// <summary>
        /// 基金
        /// </summary>		
        [DataMember]
        public decimal Fund { get; set; }

        /// <summary>
        /// 其他投资收益
        /// </summary>		
        [DataMember]
        public decimal OtherInvestment { get; set; }

        /// <summary>
        /// 养老金（一次性收入现值）
        /// </summary>		
        [DataMember]
        public decimal Pension { get; set; }

        /// <summary>
        /// 配偶/遗孤年金收益（现值）
        /// </summary>		
        [DataMember]
        public decimal AnnuityRevenue { get; set; }

        /// <summary>
        /// 其他退休基金
        /// </summary>		
        [DataMember]
        public decimal OtherPension { get; set; }

        /// <summary>
        /// 房产
        /// </summary>		
        [DataMember]
        public decimal House { get; set; }

        /// <summary>
        /// 汽车
        /// </summary>		
        [DataMember]
        public decimal Car { get; set; }

        /// <summary>
        /// 其他个人资产
        /// </summary>		
        [DataMember]
        public decimal Other { get; set; }

        /// <summary>
        /// 其他资产
        /// </summary>		
        [DataMember]
        public decimal OtherProperty { get; set; }

        /// <summary>
        /// 资产总计
        /// </summary>		
        [DataMember]
        public decimal TotalProperty { get; set; }

        /// <summary>
        /// 短期贷款
        /// </summary>		
        [DataMember]
        public decimal ShortTermLoan { get; set; }

        /// <summary>
        /// 中期贷款
        /// </summary>		
        [DataMember]
        public decimal MediumTermLoans { get; set; }

        /// <summary>
        /// 长期贷款
        /// </summary>		
        [DataMember]
        public decimal LongTermLoan { get; set; }

        /// <summary>
        /// 其他贷款
        /// </summary>		
        [DataMember]
        public decimal OtherLoan { get; set; }

        /// <summary>
        /// 临终医疗费用
        /// </summary>		
        [DataMember]
        public decimal MedicalCosts { get; set; }

        /// <summary>
        /// 预期收入纳税额支出
        /// </summary>		
        [DataMember]
        public decimal TaxCosts { get; set; }

        /// <summary>
        /// 丧葬费用
        /// </summary>		
        [DataMember]
        public decimal FuneralExpenses { get; set; }

        /// <summary>
        /// 遗产处置费用
        /// </summary>		
        [DataMember]
        public decimal HeritageCosts { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>		
        [DataMember]
        public decimal OtherCosts { get; set; }

        /// <summary>
        /// 其他负债
        /// </summary>		
        [DataMember]
        public decimal OtherLiabilities { get; set; }

        /// <summary>
        /// 负债总计
        /// </summary>		
        [DataMember]
        public decimal TotalLiabilities { get; set; }

        /// <summary>
        /// 财务分析
        /// </summary>		
        [DataMember]
        public string FinanceAnalysis { get; set; }

        /// <summary>
        /// 财产传承规划工具
        /// </summary>		
        [DataMember]
        public int PlanTool { get; set; }

        /// <summary>
        /// 财产传承规划分析
        /// </summary>		
        [DataMember]
        public string PlanAnalysis { get; set; }

    }
}