using System;
using System.Globalization;

namespace VM
{
    public class UnitTrainExamVM
    {
        public UnitTrainExamVM()
        {
    
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型Id
        /// </summary>		
        public int FinancialTypeId { get; set; }

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
        /// 完成日期
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 考试时长
        /// </summary>		
        public int TimeLength { get; set; }

        /// <summary>
        /// 考试总分
        /// </summary>		
        public decimal AllScore { get; set; }


        /// <summary>
        /// 主观题得分
        /// </summary>		
        public decimal SubjectiveResults { get; set; }

        /// <summary>
        /// 客观题得分
        /// </summary>		
        public decimal ObjectiveResults { get; set; }

        /// <summary>
        /// 发布用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 考核类型
        /// </summary>		
        public Nullable<int> ExamTypeId { get; set; }

        /// <summary>
        /// 领取后的客户Id
        /// </summary>
        public Nullable<int> StuCustomerId { get; set; }

        //============================================================

        /// <summary>
        /// 建议书Id
        /// </summary>
        public Nullable<int> ProposalId { get; set; }

        /// <summary>
        /// 案例
        /// </summary>		
        public int CaseId { get; set; }

        /// <summary>
        /// 实训班级
        /// </summary>
        public string PracticeClass { get; set; }

        /// <summary>
        /// 实训班级Id
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 评分状态
        /// </summary>
        /// 
        public int ScoreStatus { get; set; }

        /// <summary>
        /// 发布用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 理财类型名称
        /// </summary>
        public string FinancialTypeName { get; set; }

        /// <summary>
        /// 实训状态
        /// </summary>
        /// 
        public int TrainExamStatus { get; set; }

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
        /// 结束时间字符串
        /// </summary>
        public string strCreateTime
        {
            get
            {
                return CreateTime.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }

        /// <summary>
        /// 学生端销售机会状态名称
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
