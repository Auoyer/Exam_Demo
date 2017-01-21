using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///DistributionOfProperty
    /// </summary>
    public class DistributionOfPropertyVM
    {
        public DistributionOfPropertyVM()
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
        /// 客户性别
        /// </summary>		
        public int CustomerSex { get; set; }

        /// <summary>
        /// 住址
        /// </summary>		
        public string Address { get; set; }

        /// <summary>
        /// 职业
        /// </summary>		
        public string Position { get; set; }

        /// <summary>
        /// 家庭成员数
        /// </summary>		
        public int FamilyNum { get; set; }

        /// <summary>
        /// 婚姻、财产状况分析
        /// </summary>		
        public string SituationAnalysis { get; set; }

        /// <summary>
        /// 财产分配规划工具
        /// </summary>		
        public int PlanTool { get; set; }

        /// <summary>
        /// 财产分配规划分析
        /// </summary>		
        public string PlanAnalysis { get; set; }


        /// <summary>
        /// 客户详细信息集合
        /// </summary>
        public List<ProposalCustomerDetailVM> ProposalCustomerDetailList { get; set; }

        #region 私有字段
        /// <summary>
        /// 名字
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int CustomerAge { get; set; }


        #endregion
    }
}