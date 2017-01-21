using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class UnitePaperVM
    {
        public UnitePaperVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>		
        public string ExamPaperName { get; set; }

        /// <summary>
        /// 枚举：认证、理论
        /// </summary>		
        public int LibraryID { get; set; }

        /// <summary>
        /// 枚举：自动组卷、手动组卷
        /// </summary>		
        public int FormType { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 试卷状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>		
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>		
        public DateTime FiniShDate { get; set; }

        /// <summary>
        /// 开始时间字符串
        /// </summary>
        public string strStartDate
        {
            get
            {
                return StartDate.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 结束时间字符串
        /// </summary>
        public string strEndDate
        {
            get
            {
                return EndDate.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 完成日期字符串
        /// </summary>
        public string strFiniShDate
        {
            get
            {
                return FiniShDate.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 总分
        /// </summary>		
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 得分
        /// </summary>		
        public decimal Score { get; set; }

          /// <summary>
        /// 用户名称
        /// </summary>		
        public string UserName { get; set; }


        /// <summary>
        /// 评分状态
        /// </summary>		
        public string StrStatus { get; set; }
        
        /// <summary>
        /// 单个答题人这张卷子的总分（扩展字段）
        /// </summary>
        public decimal UserSumScore { get; set; }

        /// <summary>
        /// 学生端理论考核状态名称
        /// </summary>
        public string TrainExamStateName
        {
            get
            {
                string state = string.Empty;
                if (DateTime.Now > EndDate)
                {
                    //当前时间大于结束时间
                    state = "已结束";
                }
                if (DateTime.Now >= StartDate && DateTime.Now <= EndDate)
                {
                    //当前时间大于开始时间,当前时间小于结束时间
                    state = "已开始";
                }
                if (DateTime.Now < StartDate)
                {
                    state = "未开始";
                }
                return state;
            }
            private set { }
        }
    }
}
