using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    /// 通用查询条件过滤器
    /// </summary>
    [DataContract]
    public class CustomFilter
    {
        [DataMember]
        public Nullable<int> Id { get; set; }
        /// <summary>
        /// 用户Id
        /// 1.用在【实训考核名称统计】时，为教师Id
        /// </summary>
        [DataMember]
        public Nullable<int> UserId { get; set; }

        /// <summary>
        /// 用户Id2
        /// </summary>
        [DataMember]
        public Nullable<int> UserId2 { get; set; }
        /// <summary>
        /// 用户ID集合
        /// </summary>
        [DataMember]
        public List<int> UserIdList { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        [DataMember]
        public Nullable<int> CollegeId { get; set; }

        /// <summary>
        /// 案例状态（-1：启用与屏蔽，1：启用，2：屏蔽，3：逻辑删除）
        /// </summary>
        [DataMember]
        public Nullable<int> CaseStatus { get; set; }

        /// <summary>
        /// 竞赛ID
        /// </summary>
        [DataMember]
        public Nullable<int> CompetitionId { get; set; }

        /// <summary>
        /// 关键字段
        /// </summary>
        [DataMember]
        public List<string> KeyField { get; set; }
        /// <summary>
        /// 理财类型
        /// </summary>
        [DataMember]
        public Nullable<int> FinancialTypeId { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        [DataMember]
        public string IDNum { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户名称Id
        /// </summary>
        [DataMember]
        public Nullable<int> CustomerId { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        [DataMember]
        public Nullable<int> Status { get; set; }

        /// <summary>
        /// 个人考核评分状态
        /// </summary>
        [DataMember]
        public Nullable<int> TrainExamStatus { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public Nullable<int> ScoreStatus { get; set; }


        /// <summary>
        /// 有效时间类型
        /// </summary>
        [DataMember]
        public Nullable<int> SummaryType { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [DataMember]
        public string KeyWords { get; set; }

        /// <summary>
        /// TrainExamId
        /// </summary>
        [DataMember]
        public Nullable<int> TrainExamId { get; set; }

        /// <summary>
        /// 销售机会/实训考核
        /// </summary>
        [DataMember]
        public Nullable<int> ExamTypeId { get; set; }

        /// <summary>
        /// ClassId
        /// </summary>
        [DataMember]
        public Nullable<int> ClassId { get; set; }

        /// <summary>
        /// ExamPointId
        /// </summary>
        [DataMember]
        public Nullable<int> ExamPointId { get; set; }
        /// <summary>
        /// 考核点类型---主观题、客观题
        /// </summary>
        [DataMember]
        public Nullable<int> ExamPointType { get; set; }
        /// <summary>
        /// 选择时间为今天
        /// </summary>
        [DataMember]
        public DateTime ChoseTime { get; set; }


        /// <summary>
        /// 选择的类型，当天/当月/当周
        /// </summary>
        [DataMember]
        public int ChoseType { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        [DataMember]
        public Nullable<int> CustomerType { get; set; }

        /// <summary>
        /// 客户来源类型
        /// </summary>
        [DataMember]
        public Nullable<int> CustomerSourceType { get; set; }


        /// <summary>
        /// 建议书编号
        /// </summary>
        [DataMember]
        public Nullable<int> ProposalId { get; set; }

        /// <summary>
        /// 建议书名称
        /// </summary>
        [DataMember]
        public string ProposalName { get; set; }

        /// <summary>
        /// 考核名称
        /// </summary>
        [DataMember]
        public string CheckName { get; set; }

        /// <summary>
        /// 考核状态
        /// </summary>
        [DataMember]
        public Nullable<int> CheckStatus { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [DataMember]
        public string SortName { get; set; }

        /// <summary>
        /// 排序方向(true为正序asc,false为倒序desc)
        /// </summary>
        [DataMember]
        public bool SortWay { get; set; }

        /// <summary>
        /// 是否只显示(未领取/XXX/YYY等)
        /// </summary>
        [DataMember]
        public bool isShow { get; set; }

        /// <summary>
        /// 消息类型，公告/授课进度/课时安排
        /// </summary>
        [DataMember]
        public Nullable<int> MessageType { get; set; }

        /// <summary>
        /// 考核评分字段--是否小于当前时间
        /// </summary>
        [DataMember]

        public bool IsLessThanCurrentDate { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        [DataMember]
        public Nullable<int> ChapterId { get; set; }

        /// <summary>
        /// 题型来源
        /// </summary>
        [DataMember]
        public Nullable<int> Score { get; set; }

        #region 理财产品
        /// <summary>
        /// 基金类型
        /// </summary>
        [DataMember]
        public List<string> FundType { get; set; }
        #endregion

        /// <summary>
        /// 区分默认学校
        /// </summary>
        [DataMember]
        public bool BySchool { get; set; }
    }
}
