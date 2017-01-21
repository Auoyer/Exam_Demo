using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ProposalCustomerDetail
    /// </summary>
    public class ProposalCustomerDetailVM
    {
        public ProposalCustomerDetailVM()
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
        /// 客户信息家属/财产分配家属
        /// </summary>		
        public int Type { get; set; }

        /// <summary>
        /// 家属姓名
        /// </summary>		
        public string DependentName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>		
        public int Age { get; set; }

        /// <summary>
        /// 与客户关系
        /// </summary>		
        public string Relation { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>		
        public decimal InCome { get; set; }

    }
}