using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///DistributionOfProperty
    /// </summary>
    [DataContract]
    public class DistributionOfProperty
    {
        public DistributionOfProperty()
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
        /// 客户性别
        /// </summary>		
        [DataMember]
        public int CustomerSex { get; set; }

        /// <summary>
        /// 住址
        /// </summary>		
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 职业
        /// </summary>		
        [DataMember]
        public string Position { get; set; }

        /// <summary>
        /// 家庭成员数
        /// </summary>		
        [DataMember]
        public int FamilyNum { get; set; }

        /// <summary>
        /// 婚姻、财产状况分析
        /// </summary>		
        [DataMember]
        public string SituationAnalysis { get; set; }

        /// <summary>
        /// 财产分配规划工具
        /// </summary>		
        [DataMember]
        public int PlanTool { get; set; }

        /// <summary>
        /// 财产分配规划分析
        /// </summary>		
        [DataMember]
        public string PlanAnalysis { get; set; }

        /// <summary>
        /// 客户详细信息集合
        /// </summary>
          [DataMember]
        public List<ProposalCustomerDetail> ProposalCustomerDetailList { get; set; }


    }
}