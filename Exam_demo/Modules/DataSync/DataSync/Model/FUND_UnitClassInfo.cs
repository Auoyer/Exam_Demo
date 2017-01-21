using System;
using System.Globalization;

namespace DataSync
{
    /// <summary>
    /// 基金份额类别信息表
    /// </summary>
    public class FUND_UnitClassInfo
    {
        /// <summary>
        /// 基金ID
        /// </summary>
        public int FUNDID { get; set; }

        /// <summary>
        /// 基金名称
        /// </summary>
        public string SHORTNAME { get; set; }

        /// <summary>
        /// 基金代码
        /// </summary>
        public string SYMBOL { get; set; }

    }
}
