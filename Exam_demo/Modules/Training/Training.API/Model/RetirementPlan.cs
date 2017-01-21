using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///RetirementPlan
    /// </summary>
    [DataContract]
    public class RetirementPlan
    {
        public RetirementPlan()
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
        /// 当前年龄
        /// </summary>		
        [DataMember]
        public int Age { get; set; }

        /// <summary>
        /// 退休前通货膨胀率
        /// </summary>		
        [DataMember]
        public decimal BeforeInflationRate { get; set; }

        /// <summary>
        /// 退休后通货膨胀率
        /// </summary>		
        [DataMember]
        public decimal AfterInflationRate { get; set; }

        /// <summary>
        /// 退休后投资收益率
        /// </summary>		
        [DataMember]
        public decimal RetirementRate { get; set; }

        /// <summary>
        /// 社保工资增长率
        /// </summary>		
        [DataMember]
        public decimal? SociaSecurityRate { get; set; }

        /// <summary>
        /// 租金增长率
        /// </summary>		
        [DataMember]
        public decimal? RentRate { get; set; }

        /// <summary>
        /// 其他收入增长率
        /// </summary>		
        [DataMember]
        public decimal? OtherRate { get; set; }

        /// <summary>
        /// 计划退休年龄
        /// </summary>		
        [DataMember]
        public int RetirementAge { get; set; }

        /// <summary>
        /// 希望享有退休生活年限
        /// </summary>		
        [DataMember]
        public int RetirementYears { get; set; }

        /// <summary>
        /// 目前生活水平
        /// </summary>		
        [DataMember]
        public decimal LivingStandardNow { get; set; }

        /// <summary>
        /// 生活满意度
        /// </summary>		
        [DataMember]
        public int Satisfaction { get; set; }

        /// <summary>
        /// 满意生活水平
        /// </summary>		
        [DataMember]
        public decimal SatisfactionLivingStandard { get; set; }

        /// <summary>
        /// 退休后、退休前生活水平折算比例
        /// </summary>		
        [DataMember]
        public int ConvertProportion { get; set; }

        /// <summary>
        /// 子女传承费用
        /// </summary>		
        [DataMember]
        public decimal lineageFee { get; set; }

        /// <summary>
        /// 退休时生活水平
        /// </summary>		
        [DataMember]
        public decimal RetirementLivingStandard { get; set; }

        /// <summary>
        /// 退休后生活水平
        /// </summary>		
        [DataMember]
        public decimal AfterLivingStandard { get; set; }

        /// <summary>
        /// 社会保险
        /// </summary>		
        [DataMember]
        public decimal SocialInsurance { get; set; }

        /// <summary>
        /// 商业保险
        /// </summary>		
        [DataMember]
        public decimal CommercialInsurance { get; set; }

        /// <summary>
        /// 租金收入
        /// </summary>		
        [DataMember]
        public decimal RentIncome { get; set; }

        /// <summary>
        /// 其他收入
        /// </summary>		
        [DataMember]
        public decimal OtherIncome { get; set; }

        /// <summary>
        /// 小计
        /// </summary>		
        [DataMember]
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// 退休时需准备的现金总金额
        /// </summary>		
        [DataMember]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        [DataMember]
        public decimal ReturnOnInvestmentRate { get; set; }

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
        public decimal RegularYear { get; set; }

        /// <summary>
        /// 此方案能实现的目标金额
        /// </summary>		
        [DataMember]
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// 创业规划分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

    }
}