using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 客户来源
    /// </summary>
    public enum CustomerSourceType
    {
        /// <summary>
        /// 销售机会
        /// </summary>
        [Description("销售机会")]
        SalesOpportunities = 1,

        /// <summary>
        /// 自主增加
        /// </summary>
        [Description("自主增加")]
        MyselfAdd = 2
    }
}