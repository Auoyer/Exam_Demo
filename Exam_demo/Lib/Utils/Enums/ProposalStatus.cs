using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 建议书目前的状态
    /// </summary>
    public enum ProposalStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,
        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        UnCommitted = 1,
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        UnAudited = 2,
        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Audited = 3,

        /// <summary>
        /// 假3删除
        /// </summary>
        [Description("已删除")]
        deletes = 4
    }
}
