using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///RiskIndex
    /// </summary>
    public class RiskIndexVM
    {
        public RiskIndexVM()
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
        /// 年龄指标分数
        /// </summary>		
        public int AgeScore { get; set; }

        /// <summary>
        /// 就业负担
        /// </summary>		
        public int JobScore { get; set; }

        /// <summary>
        /// 家庭负担
        /// </summary>		
        public int FamilyScore { get; set; }

        /// <summary>
        /// 置产状况
        /// </summary>		
        public int HouseScore { get; set; }

        /// <summary>
        /// 投资经验
        /// </summary>		
        public int EXPScore { get; set; }

        /// <summary>
        /// 投资知识
        /// </summary>		
        public int KnowledgeScore { get; set; }

        /// <summary>
        /// 风险承受能力
        /// </summary>		
        public decimal RCIScore { get; set; }

        /// <summary>
        /// 忍受亏损百分比
        /// </summary>		
        public int TolerateScore { get; set; }

        /// <summary>
        /// 首要考虑
        /// </summary>		
        public int ConsiderationScore { get; set; }

        /// <summary>
        /// 认赔动作
        /// </summary>		
        public int LossScore { get; set; }

        /// <summary>
        /// 赔钱心理
        /// </summary>		
        public int MentalityScore { get; set; }

        /// <summary>
        /// 最重要特性
        /// </summary>		
        public int CharacterScore { get; set; }

        /// <summary>
        /// 避免工具
        /// </summary>		
        public int AvoidScore { get; set; }

        /// <summary>
        /// 测评日期
        /// </summary>		
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 风险容忍态度
        /// </summary>		
        public decimal RAIScore { get; set; }

        //=====================================================
        public string UpdateDateStr 
        {
            get
            {
                return UpdateDate.ToString("yyyy/MM/dd");
            }
        }



    }
}