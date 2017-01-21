using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public  class CalendarSearch
    {
        public CalendarSearch() {
           
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Nullable<int> UserId { get; set; }

        /// <summary>
        /// 选择的时间，默认为当天
        /// </summary>
        public DateTime ChoseTime { get; set; }

        /// <summary>
        /// 选择的类型，当天/当月/当周
        /// </summary>
        public int ChoseType { get; set; }

    }
}
