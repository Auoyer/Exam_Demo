using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 实训查询通用类
    /// </summary>
    public class TrainSearch
    {
        /// <summary>
        /// Id
        /// </summary>
        public Nullable<int> Id { get; set; }
        /// <summary>
        /// 次要ID（查usersummary时传试卷ID）
        /// </summary> 
        public Nullable<int> Id2 { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public Nullable<int> CollegeId { get; set; }

        /// <summary>
        /// 案例状态（-1：启用与屏蔽，1：启用，2：屏蔽，3：逻辑删除）
        /// </summary>
        public Nullable<int> CaseStatus { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>
        public Nullable<int> CompetitionId { get; set; }

        /// <summary>
        /// 用户Id（查询时不包括系统内置）
        /// </summary>
        public Nullable<int> UserId { get; set; }

        /// <summary>
        /// 用户Id2（查询时包括系统内置）
        /// </summary>
        public Nullable<int> UserId2 { get; set; }

        /// <summary>
        /// 有效时间类型
        /// </summary>
        public Nullable<int> SummaryType { get; set; }

        /// <summary>
        /// 用户IDList
        /// </summary>
        public List<int> UserIdList { get; set; }

        /// <summary>
        /// 关键字段
        /// </summary> 
        public List<string> KeyField { get; set; }

        /// <summary>
        /// 理财类型
        /// </summary>
        public Nullable<int> FinancialTypeId { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDNum { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户名称Id
        /// </summary>
        public Nullable<int> CustomerId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public List<int> CreateUserIds { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public Nullable<int> Status { get; set; }

        /// <summary>
        /// 个人考核评分状态
        /// </summary>
        public int TrainExamStatus { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int ScoreStatus { get; set; }

        /// <summary>
        /// TrainExamId
        /// </summary>
        public Nullable<int> TrainExamId { get; set; }

        /// <summary>
        /// 销售机会/实训考核
        /// </summary>
        public Nullable<int> ExamTypeId { get; set; }

        /// <summary>
        /// ClassId
        /// </summary>
        public Nullable<int> ClassId { get; set; }

        /// <summary>
        /// ExamPointId
        /// </summary>
        public Nullable<int> ExamPointId { get; set; }
        /// <summary>
        /// 考核点类型--主观题、客观题
        /// </summary>
        public Nullable<int> ExamPointType { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public Nullable<int> CustomerType { get; set; }

        /// <summary>
        /// 客户来源类型
        /// </summary>
        public Nullable<int> CustomerSourceType { get; set; }

        /// <summary>
        /// 章节
        /// </summary>
        public Nullable<int> CharpterID { get; set; }

        /// <summary>
        /// 建议书编号
        /// </summary>
        public Nullable<int> ProposalId { get; set; }

        /// <summary>
        /// 建议书名称
        /// </summary>
        public string ProposalName { get; set; }

        /// <summary>
        /// 案例Id
        /// </summary>
        public int CaseId { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序方向(true为正序asc,false为倒序desc)
        /// </summary>
        public bool SortWay { get; set; }

        /// <summary>
        /// 考核名称
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// 考核状态
        /// </summary>
        public Nullable<int> CheckStatus { get; set; }

        /// <summary>
        /// 是否只显示(未领取/XXX/YYY等)
        /// </summary>
        public bool isShow { get; set; }

        /// <summary>
        /// 教师公告/授课进度/课时安排
        /// </summary>
        public Nullable<int> MessageType { get; set; }


        /// <summary>
        /// 考核评分字段--是否小于当前时间
        /// </summary> 

        public bool IsLessThanCurrentDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<int> ChapterId { get; set; }

        /// <summary>
        /// 题型来源
        /// </summary>
        public Nullable<int> Score { get; set; }

        #region 理论考核的
        /// <summary>
        /// 分数信息
        /// </summary>
        public bool ScoreInfo { get; set; }
        /// <summary>
        /// 章节名
        /// </summary>
        public bool CharpterList { get; set; }
        /// <summary>
        /// 班级列表
        /// </summary>
        public bool ClassList { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public bool Details { get; set; }
       /// <summary>
       /// 用户问题
       /// </summary>
        public bool UserAnswer { get; set; }
        /// <summary>
        /// 用户问题结果
        /// </summary>
        public bool UserAnswerResult { get; set; }
        /// <summary>
        /// 用户对应的试卷表
        /// </summary>
        public bool UserSummary { get; set; }
        /// <summary>
        /// 理论考核状态
        /// </summary>
        public int TheoryExamStatus { get; set; }
        /// <summary>
        /// 是否大于试卷结束时间
        /// </summary>
        public bool IsOverEndTime { get; set; }

        #endregion

        #region 理财产品
        /// <summary>
        /// 基金类型
        /// </summary>
        public List<string> FundType { get; set; }

        /// <summary>
        /// 考试类型
        /// </summary>
        public Nullable<int> LibraryId { get; set; }
        #endregion

        /// <summary>
        /// 区分默认学校
        /// </summary>
        public bool BySchool { get; set; }
    }
}
