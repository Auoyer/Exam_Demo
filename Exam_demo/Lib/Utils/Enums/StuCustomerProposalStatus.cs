using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 潜在客户/已有客户对应的建议书状态
    /// </summary>
    public enum StuCustomerProposalStatus
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Edit = 2,
        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Submit = 3,
        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Delete = 4
    }
}
