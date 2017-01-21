using System.ComponentModel;

namespace Utils
{
    public enum CustomerType
    {
        /// <summary>
        /// 潜在客户
        /// </summary>
        [Description("潜在客户")]
        PotentialCustomer = 1,

        /// <summary>
        /// 已有客户
        /// </summary>
        [Description("已有客户")]
        ExistCustomer = 2,
    }
}