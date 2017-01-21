using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///RiskIndex
    /// </summary>
    [DataContract]
    public class RiskIndex
    {
        public RiskIndex()
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
        /// 年龄指标分数
        /// </summary>		
        [DataMember]
        public int AgeScore { get; set; }

        /// <summary>
        /// 就业负担
        /// </summary>		
        [DataMember]
        public int JobScore { get; set; }

        /// <summary>
        /// 家庭负担
        /// </summary>		
        [DataMember]
        public int FamilyScore { get; set; }

        /// <summary>
        /// 置产状况
        /// </summary>		
        [DataMember]
        public int HouseScore { get; set; }

        /// <summary>
        /// 投资经验
        /// </summary>		
        [DataMember]
        public int EXPScore { get; set; }

        /// <summary>
        /// 投资知识
        /// </summary>		
        [DataMember]
        public int KnowledgeScore { get; set; }

        /// <summary>
        /// 风险承受能力
        /// </summary>		
        [DataMember]
        public decimal RCIScore { get; set; }

        /// <summary>
        /// 忍受亏损百分比
        /// </summary>		
        [DataMember]
        public int TolerateScore { get; set; }

        /// <summary>
        /// 首要考虑
        /// </summary>		
        [DataMember]
        public int ConsiderationScore { get; set; }

        /// <summary>
        /// 认赔动作
        /// </summary>		
        [DataMember]
        public int LossScore { get; set; }

        /// <summary>
        /// 赔钱心理
        /// </summary>		
        [DataMember]
        public int MentalityScore { get; set; }

        /// <summary>
        /// 最重要特性
        /// </summary>		
        [DataMember]
        public int CharacterScore { get; set; }

        /// <summary>
        /// 避免工具
        /// </summary>		
        [DataMember]
        public int AvoidScore { get; set; }

        /// <summary>
        /// 测评日期
        /// </summary>		
        [DataMember]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 风险容忍态度
        /// </summary>		
        [DataMember]
        public decimal RAIScore { get; set; }

    }
}