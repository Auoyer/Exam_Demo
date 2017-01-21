using System;
using System.Globalization;

namespace DataSync
{
    /// <summary>
    /// 基金主体信息表
    /// </summary>
    public class FUND_MainInfo
    {
        /// <summary>
        /// 基金ID
        /// </summary>
        public int FUNDID { get; set; }

        /// <summary>
        /// 基金类型
        /// </summary>
        public string CATEGORY { get; set; }

        /// <summary>
        /// 基金公司
        /// </summary>
        public string FUNDCOMPANYNAME { get; set; }

    }
}
