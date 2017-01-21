using System;
using System.Globalization;

namespace DataSync
{
    /// <summary>
    /// 费率变动文件
    /// </summary>
    public class Fund_FeesChange
    {
        /// <summary>
        /// 托管费率
        /// </summary>
        public string HostingFees { get; set; }

        /// <summary>
        /// 最低申购份额
        /// </summary>
        public string PurchaseShares { get; set; }

    }
}
