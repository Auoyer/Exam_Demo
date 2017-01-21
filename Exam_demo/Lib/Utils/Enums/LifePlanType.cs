using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public enum LifePlanType
    {
        /// <summary>
        /// 1 教育规划
        /// </summary>
        [Description("教育规划")]
        LifeEducationPlan = 1,
        /// <summary>
        /// 2 消费规划
        /// </summary>
        [Description("消费规划")]
        ConsumptionPlan = 2,
         /// <summary>
        /// 3 创业规划
        /// </summary>
        [Description("创业规划")]
        StartAnUndertakingPlan = 3,
        /// <summary>
        /// 4 退休规划
        /// </summary>
        [Description("退休规划")]
        RetirementPlan = 4

    }
}
