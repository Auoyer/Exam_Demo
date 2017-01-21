using System;
using System.Globalization;
using System.Collections.Generic;

namespace VM
{
    /// <summary>
    ///用户试卷得分情况
    /// </summary>
    [Serializable]
    public class PaperUserSummaryVM
    {
        public PaperUserSummaryVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>		
        public int ExamPaperId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int ClassId { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>		
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 用户得分
        /// </summary>		
        public decimal Score { get; set; }

        /// <summary>
        /// 评分状态
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>		
        public DateTime? FinishDate { get; set; }

        /*---------------------------------------------------扩展字段---------------------------------------------------------------------------*/
        /// <summary>
        /// 完成日期字符串
        /// </summary>
        public string strFiniShDate
        {
            get
            {
                if (FinishDate.HasValue)
                {
                    return FinishDate.Value.ToString("yyyy/MM/dd HH:mm", DateTimeFormatInfo.InvariantInfo);
                }
                else
                {
                    return "";
                }
            }
            private set { }
        }

        /// <summary>
        /// 考核名称
        /// </summary>		

        public string ExamPaperName { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>		
      
        public int UId { get; set; }

        /// <summary>
        /// 发布人姓名
        /// </summary>		
        public string UserName { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string SchoolNum { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StrStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 达标分
        /// </summary>
        public int StandardScore 
        { 
            get{
                return Convert.ToInt32((TotalScore * 6)/10);
            } 
        }

        /// <summary>
        /// 试卷状态
        /// </summary>	
        public int Status2 { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>		
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 未评题量统计
        /// </summary>
        public int UnScoredCount { get; set; }

        #region 答题相关扩展信息
        /// <summary>
        /// 答题结果缓存
        /// </summary>
        public List<PaperUserAnswerResultVM> ResultInfo { get; set; }
        /// <summary>
        /// 用户答案缓存
        /// </summary>
        public List<PaperUserAnswerVM> AnswerInfo { get; set; }

        #endregion


        #region 成绩评定扩展相关字段
        ///// <summary>
        ///// 班级名称
        ///// </summary>
        //public string ClassName { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StuUserName { get; set; }
        /// <summary>
        /// 学号工号
        /// </summary>
        public string SchoolNumber { get; set; }
        /// <summary>
        /// 状态名
        /// </summary>
        public string StatusName { get; set; }

        #endregion

        /// <summary>
        /// FinishDate
        /// </summary>		
        public int GroupId { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>		
        public int CompetitionId { get; set; }
    }
}