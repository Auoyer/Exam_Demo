using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///Calendar
    /// </summary>
    public class CalendarVM
    {
        public CalendarVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 学生客户Id
        /// </summary>		
        public int StuCustomerId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 服务类型(枚举)
        /// </summary>		
        public int ServiceType { get; set; }

        /// <summary>
        /// 计划内容
        /// </summary>		
        public string Context { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>		
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 显示时间
        /// </summary>
        public string StrOrDate
        {
            get
            {
                return OrderDate.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            }
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// 用来接收日历插件获取的时间
        /// </summary>
        public string GetOrDate { get; set; }

        /// <summary>
        /// 显示服务类型
        /// </summary>
        public string StrType { get; set; }


    }

    /// <summary>
    /// 日程插件显示时需要显示的字段
    /// </summary>
    public class CalendarJsonVM
    {
        /// <summary>
        /// 标题，即日程安排
        /// </summary>
        public string title { get; set; }

        public DateTime start { get; set; }

        public string strStart
        {
            get
            {
                return start.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

    }
}