using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public enum EnableStatusType
    {
        /// <summary>
        /// 1 启用
        /// </summary>
        [Description("启用")]
        EnableStatus = 1,

        /// <summary>
        /// 2 禁用
        /// </summary>
        [Description("禁用")]
        DisableStatus = 2
    }
}