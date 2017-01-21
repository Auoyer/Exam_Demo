using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///用户操作时间统计
    /// </summary>
    public class UserTimeSummaryVM
    {
        public UserTimeSummaryVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 统计类型，枚举
        /// </summary>		
        public int SummaryType { get; set; }

        /// <summary>
        /// 花费时间(分钟)
        /// </summary>		
        public int UsedTime { get; set; }

    }
}