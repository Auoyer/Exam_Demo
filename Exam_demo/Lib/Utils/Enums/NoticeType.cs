using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public enum NoticeType
    {
        /// <summary>
        /// 系统公告 1
        /// </summary>
        [Description("系统公告")]
        SystemNotice = 1,
        /// <summary>
        /// 竞赛管理员 2
        /// </summary>
        [Description("大赛公告")]
        CompetitionNotice = 2,
        /// <summary>
        ///竞赛评委 3
        /// </summary>
        [Description("温馨提示")]
        KindlyReminder = 3,
        /// <summary>
        /// 竞赛用户  4
        /// </summary>
        [Description("资讯快报")]
        InformationExpress = 4,
    }
}
