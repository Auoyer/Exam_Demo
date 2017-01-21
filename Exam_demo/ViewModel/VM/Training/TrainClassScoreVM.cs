using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 统计班级每月得分
    /// </summary>
    public class TrainClassScoreVM
    {
        /// <summary>
        /// 班级Id
        /// </summary>
      
        public int ClassId { get; set; }

        /// <summary>
        /// 平均分
        /// </summary>
 
        public decimal Average { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
  
        public DateTime EndDate { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        public string StrEndDate {
            get {
              return   EndDate.ToString("yyyy-MM", DateTimeFormatInfo.InvariantInfo);
            }
            set { }
        }
    }


    public class ClassScoreCache
    {
        public ClassScoreCache() {
            UpdateTime = DateTime.Now.ToString("yyyy-MM", DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TrainClassScoreVM> ClassScore { get; set; }
    }

    /// <summary>
    /// 传出Highcharts需要的数据
    /// </summary>
    public class Highcharts
    {
        /// <summary>
        /// 数据名称
        /// </summary>
        public string Name { get; set; }

        
        //数据list
        public List<XYData> XYData { get; set; }
    }

    public class XYData{
        /// <summary>
        /// x轴数据
        /// </summary>
        public string XData { get; set; }

        /// <summary>
        /// y轴数据
        /// </summary>
        public decimal YData { get; set; }
    }

}
