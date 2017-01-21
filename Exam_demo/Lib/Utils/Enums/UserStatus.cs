using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 用户状态枚举
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 待审核 1
        /// </summary>
        [Description("待审核")]
        NoCheck = 1,
        /// <summary>
        /// 正常 2
        /// </summary>
        [Description("正常")]
        Normal = 2,
        /// <summary>
        /// 冻结 3
        /// </summary>
        [Description("冻结")]
        Fronzen = 3,
        /// <summary>
        /// 已删除 100
        /// </summary>
        [Description("已删除")]
        Deleted = 100
    }
}
