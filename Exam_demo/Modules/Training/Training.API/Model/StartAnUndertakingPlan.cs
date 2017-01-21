using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///StartAnUndertakingPlan
    /// </summary>
    [DataContract]
    public class StartAnUndertakingPlan
    {
        public StartAnUndertakingPlan()
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
        /// 计划创业年龄
        /// </summary>		
        [DataMember]
        public int StartPlanAge { get; set; }

        /// <summary>
        /// 离创业年限
        /// </summary>		
        [DataMember]
        public int DistanceYear { get; set; }

        /// <summary>
        /// 创业时一次性投入
        /// </summary>		
        [DataMember]
        public decimal CostInput { get; set; }

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
        public int RegularYear { get; set; }

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