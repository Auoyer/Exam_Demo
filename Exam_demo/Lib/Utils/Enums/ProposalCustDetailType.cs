using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 家属信息类型
    /// </summary>
    public enum ProposalCustDetailType
    {
        /// <summary>
        /// 客户信息家属
        /// </summary>
        [Description("客户信息家属")]
        CustomerFaimly = 1,
        /// <summary>
        /// 财产分配家属
        /// </summary>
        [Description("财产分配家属")]
        FinancialFaimly = 2
    }
}
