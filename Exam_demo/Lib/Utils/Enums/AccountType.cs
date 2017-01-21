using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 帐号类型
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// 试用帐号1
        /// </summary>
        [Description("试用帐号")]
        TA=1,

        /// <summary>
        /// 普通帐号2
        /// </summary>
        [Description("普通帐号")]
        NA=2
    }
}
