using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 帐号状态
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// 失效
        /// </summary>
        [Description("失效")]
        Invalid=1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal=2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del=3
    }
}
