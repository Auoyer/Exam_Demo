using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Number
    /// </summary>
    public class NumberVM
    {
        public NumberVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 编号类型，枚举
        /// </summary>		
        public int NumberType { get; set; }

        /// <summary>
        /// 编号前缀
        /// </summary>		
        public string Prefix { get; set; }

        /// <summary>
        /// 编号日期
        /// </summary>		
        public DateTime CurrentDate { get; set; }

        /// <summary>
        /// 已使用最大编号
        /// </summary>		
        public long UsedMaxCode { get; set; }

        /// <summary>
        /// 允许最大位数
        /// </summary>
        public int Figure { get; set; }

        /// <summary>
        /// (扩展字段)最后已使用最大编号
        /// </summary>
        public long LastUsedMaxCode { get; set; }

    }
}