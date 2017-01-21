using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///InsurancePlan
    /// </summary>
    [DataContract]
    public class InsurancePlan
    {
        public InsurancePlan()
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
        /// 寿险需求测算方法Id
        /// </summary>		
        [DataMember]
        public int MethodTypeId { get; set; }

        /// <summary>
        /// 被保险人年龄B
        /// </summary>		
        [DataMember]
        public int Age1 { get; set; }

        /// <summary>
        /// 被保险人年龄C
        /// </summary>		
        [DataMember]
        public int Age2 { get; set; }

        /// <summary>
        /// 预计退休年龄B
        /// </summary>		
        [DataMember]
        public int RetirementAge1 { get; set; }

        /// <summary>
        /// 预计退休年龄C
        /// </summary>		
        [DataMember]
        public int RetirementAge2 { get; set; }

        /// <summary>
        /// 投资报酬率B
        /// </summary>		
        [DataMember]
        public decimal ReturnOnInvestment1 { get; set; }

        /// <summary>
        /// 投资报酬率C
        /// </summary>		
        [DataMember]
        public decimal ReturnOnInvestment2 { get; set; }

        /// <summary>
        /// 通货膨胀率B
        /// </summary>		
        [DataMember]
        public decimal InflationRate1 { get; set; }

        /// <summary>
        /// 通货膨胀率C
        /// </summary>		
        [DataMember]
        public decimal InflationRate2 { get; set; }

        /// <summary>
        /// 收入增长率B
        /// </summary>		
        [DataMember]
        public decimal RevenueGrowth1 { get; set; }

        /// <summary>
        /// 收入增长率C
        /// </summary>		
        [DataMember]
        public decimal RevenueGrowth2 { get; set; }

        /// <summary>
        /// 当前的家庭生活费用B
        /// </summary>		
        [DataMember]
        public decimal MatrimonialFee1 { get; set; }

        /// <summary>
        /// 当前的家庭生活费用C
        /// </summary>		
        [DataMember]
        public decimal MatrimonialFee2 { get; set; }

        /// <summary>
        /// 保险事故发生后支出调整率B
        /// </summary>		
        [DataMember]
        public decimal AfterAccidentRate1 { get; set; }

        /// <summary>
        /// 保险事故发生后支出调整率C
        /// </summary>		
        [DataMember]
        public decimal AfterAccidentRate2 { get; set; }

        /// <summary>
        /// 个人/配偶的个人年收入B
        /// </summary>		
        [DataMember]
        public decimal Income1 { get; set; }

        /// <summary>
        /// 个人/配偶的个人年收入C
        /// </summary>		
        [DataMember]
        public decimal Income2 { get; set; }

        /// <summary>
        /// 紧急备用金现值B
        /// </summary>		
        [DataMember]
        public decimal ReserveFund1 { get; set; }

        /// <summary>
        /// 紧急备用金现值C
        /// </summary>		
        [DataMember]
        public decimal ReserveFund2 { get; set; }

        /// <summary>
        /// 教育金现值B
        /// </summary>		
        [DataMember]
        public decimal EduAmount1 { get; set; }

        /// <summary>
        /// 教育金现值C
        /// </summary>		
        [DataMember]
        public decimal EduAmount2 { get; set; }

        /// <summary>
        /// 养老基金现值B
        /// </summary>		
        [DataMember]
        public decimal PensionFunds1 { get; set; }

        /// <summary>
        /// 养老基金现值C
        /// </summary>		
        [DataMember]
        public decimal PensionFunds2 { get; set; }

        /// <summary>
        /// 临终及丧葬支出现值B
        /// </summary>		
        [DataMember]
        public decimal DeathExpense1 { get; set; }

        /// <summary>
        /// 临终及丧葬支出现值C
        /// </summary>		
        [DataMember]
        public decimal DeathExpense2 { get; set; }

        /// <summary>
        /// 目前贷款余额B
        /// </summary>		
        [DataMember]
        public decimal LoanBalance1 { get; set; }

        /// <summary>
        /// 目前贷款余额C
        /// </summary>		
        [DataMember]
        public decimal LoanBalance2 { get; set; }

        /// <summary>
        /// 家庭生息资产B
        /// </summary>		
        [DataMember]
        public decimal EarningAssets1 { get; set; }

        /// <summary>
        /// 家庭生息资产C
        /// </summary>		
        [DataMember]
        public decimal EarningAssets2 { get; set; }

        /// <summary>
        /// 已有额度B
        /// </summary>		
        [DataMember]
        public decimal InsuranceAmount1 { get; set; }

        /// <summary>
        /// 已有额度C
        /// </summary>		
        [DataMember]
        public decimal InsuranceAmount2 { get; set; }

        /// <summary>
        /// 预算金额B
        /// </summary>		
        [DataMember]
        public decimal BudgetAmount1 { get; set; }

        /// <summary>
        /// 预算金额C
        /// </summary>		
        [DataMember]
        public decimal BudgetAmount2 { get; set; }

        /// <summary>
        /// 补充额度B
        /// </summary>		
        [DataMember]
        public decimal SupplementaryQuota1 { get; set; }

        /// <summary>
        /// 补充额度C
        /// </summary>		
        [DataMember]
        public decimal SupplementaryQuota2 { get; set; }

        /// <summary>
        /// 保险规划分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

        /// <summary>
        /// 配偶名字
        /// </summary>
        [DataMember]
        public string SpouseName { get; set; }
        /// <summary>
        /// 当前个人年支出
        /// </summary>
        [DataMember]
        public decimal Expenditure { get; set; }
    }
}