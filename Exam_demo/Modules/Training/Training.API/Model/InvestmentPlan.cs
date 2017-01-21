using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///InvestmentPlan
    /// </summary>
    [DataContract]
    public class InvestmentPlan
    {
        public InvestmentPlan()
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
        /// 当前客户所处家庭生命周期Id
        /// </summary>		
        [DataMember]
        public int LifeCycleId { get; set; }

        /// <summary>
        /// 投资规划分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

        /// <summary>
        /// 保值层比例
        /// </summary>		
        [DataMember]
        public decimal HoldRate { get; set; }

        /// <summary>
        /// 增值层比例
        /// </summary>		
        [DataMember]
        public decimal IncreaseRate { get; set; }

        /// <summary>
        /// 投机层比例
        /// </summary>		
        [DataMember]
        public decimal SpeculationRate { get; set; }

        /// <summary>
        /// 客户详细信息集合
        /// </summary>
        [DataMember]
        public List<InvestmentPlanProduct> InvestmentPlanProductList { get; set; }

    }
}