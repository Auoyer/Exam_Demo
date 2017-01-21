using System.ComponentModel;

namespace Utils
{
    /// <summary>
    /// 用户类型枚举
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 超管 1
        /// </summary>
        [Description("超管")]
        SA = 1,
        /// <summary>
        /// 竞赛管理员 2
        /// </summary>
        [Description("竞赛管理员")]
        CA = 2,
        /// <summary>
        ///竞赛评委 3
        /// </summary>
        [Description("竞赛评委")]
        CJ = 3,
        /// <summary>
        /// 竞赛用户  4
        /// </summary>
        [Description("竞赛用户")]
        CU = 4,
        /// <summary>
        /// 游客 0（预留）
        /// </summary>
        [Description("游客")]
        Guest = 0
    }
}
