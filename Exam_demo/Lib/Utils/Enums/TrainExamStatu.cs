using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 考核实训状态
    /// </summary>
    public enum TrainExamStatu
    {
        /// <summary>
        /// 1 待评分
        /// </summary>
        [Description("待评分")]
        WaitGrade = 1,
        /// <summary>
        /// 2 已评分
        /// </summary>
        [Description("已评分")]
        AlreadGrade = 2,

        /// <summary>
        /// 3 不显示
        /// </summary>
        [Description("不显示")]
        NoShow = 3
    }
}
