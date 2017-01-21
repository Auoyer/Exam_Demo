using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
   public class TrainExamAssessmentVM
    {
       public TrainExamAssessmentVM()
       {

       }

       /// <summary>
       /// 考核点ID/或者建议书ID共用表
       /// </summary>
       public int Id { get; set; }

       /// <summary>
       /// 用户ID
       /// </summary>
       public int UserId { get; set; }
       /// <summary>
       /// 班级ID
       /// </summary>
     
       public int ClassId { get; set; }
       /// <summary>
       /// 实训考核ID
       /// </summary>
       public int TrainExamId { get; set; }

       /// <summary>
       /// 总分
       /// </summary>
       public decimal TotalScore { get; set; }

       /// <summary>
       /// 主观分
       /// </summary>
       public decimal SubjectiveResults { get; set; }
       /// <summary>
       /// 客观分
       /// </summary>
       public decimal ObjectiveResults { get; set; }

       /// <summary>
       /// 考核名称TrainExamName
       /// </summary>
       public string TrainExamName { get; set; }


       /// <summary>
       /// 开始时间
       /// </summary>		
       public DateTime StartDate { get; set; }

       /// <summary>
       /// 结束时间
       /// </summary>		
       public DateTime EndDate { get; set; }


       /// <summary>
       /// 客户姓名
       /// </summary>		
       public string CustomerName { get; set; }

       /// <summary>
       /// 证件类型
       /// </summary>		
       public int IDType { get; set; }

       /// <summary>
       /// 证件号
       /// </summary>		
       public string IDNum { get; set; }

       /// <summary>
       /// 理财类型Id
       /// </summary>		
       public int FinancialTypeId { get; set; }

       /// <summary>
       /// 创建时间---实训的创建时间
       /// </summary>		
       public DateTime CreateTime { get; set; }
       /// <summary>
       /// 更新时间---建议书的时间
       /// </summary>		
       public DateTime UpdateDate { get; set; }

        #region 扩展字段
       /// <summary>
       /// 班级名称
       /// </summary>
       public string ClassName { get; set; }
       /// <summary>
       /// 用户名称
       /// </summary>
       public string UserName { get; set; }
       /// <summary>
       /// 学号
       /// </summary>
       public string SchoolNum { get; set; }


       /// <summary>
       /// 院系名
       /// </summary>
       public string CollegeName { get; set; }
       /// <summary>
       /// 理财类型实训名称
       /// </summary>
       public string FinancialTypeName { get; set; }

       /// <summary>
       /// 开始时间字符串
       /// </summary>
       public string strStartDate
       {
           get
           {
               return StartDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
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
               return EndDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
           }
           private set { }
       }
        #endregion

       /// <summary>
       /// 跟新时间
       /// </summary>
       public string StrUpdateDate
       {
           get
           {
               return UpdateDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
           }

       }

       /// <summary>
       /// 达标分
       /// </summary>
       public decimal PassScore
       {
           get
           {
               
               return TotalScore * 0.6m;
           }
       }

       /// <summary>
       /// 贵生成绩
       /// </summary>
       public decimal RealSumScore
       {
           get
           {
               return SubjectiveResults + ObjectiveResults;
           }
       }

    }
}
