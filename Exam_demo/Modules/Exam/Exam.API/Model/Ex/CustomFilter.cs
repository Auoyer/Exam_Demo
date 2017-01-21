using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Exam.API
{
    /// <summary>
    /// 通用查询条件过滤器
    /// </summary>
    [DataContract]
    public class CustomFilter
    {
        public CustomFilter()
        {
            IsOverEndTime = false;
            BySchool = false;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [DataMember]
        public Nullable<int> Id { get; set; }
        /// <summary>
        /// 次要ID（查usersummary时传试卷ID）
        /// </summary>
        [DataMember]
        public Nullable<int> Id2 { get; set; }
        [DataMember]
        public Nullable<int> LiburaryId { get; set; }
        [DataMember]
        public Nullable<int> StatusId { get; set; }
        [DataMember]
        public Nullable<int> CharpterID { get; set; }
        [DataMember]
        public Nullable<int> StructType { get; set; }
        [DataMember]
        public List<int> IdList { get; set; }
        [DataMember]
        public string KeyWords { get; set; }
        [DataMember]
        public Nullable<int> ClassId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public Nullable<int> Status { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        [DataMember]
        public Nullable<int> CollegeId { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>
        [DataMember]
        public Nullable<int> CompetitionId { get; set; }

        /// <summary>
        /// 单校查询
        /// </summary>
        [DataMember]
        public bool BySchool { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public Nullable<int> UserId { get; set; }
        /// <summary>
        /// 学生用户Id
        /// </summary>
        [DataMember]
        public Nullable<int> UserId2 { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>
        [DataMember]
        public string StructTypeName { get; set; }

        /// <summary>
        /// 题干
        /// </summary>
        [DataMember]
        public string Context { get; set; }

        /// <summary>
        /// 排序字段名称
        /// </summary>
        [DataMember]
        public string SortName { get; set; }

        /// <summary>
        /// 排序字段状态
        /// </summary>
        [DataMember]
        public bool SortWay { get; set; }

        /// <summary>
        /// 是否加载试卷名称
        /// </summary>
        [DataMember]
        public bool isShow { get; set; }

        /// <summary>
        /// 是否加载试卷名称
        /// </summary>
        [DataMember]
        public Nullable<int> TheoryExamStatus { get; set; }

        /// <summary>
        /// 章节下题型的集合
        /// </summary>

        [DataMember]
        public string Listtypeid { get; set; }

        /// <summary>
        /// 考核评分字段--是否小于当前时间
        /// </summary> 
        [DataMember]
        public bool IsLessThanCurrentDate { get; set; }

        /// <summary>
        /// 是否大于试卷结束时间
        /// </summary>
        [DataMember]
        public bool IsOverEndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool isBool { get; set; }
    }
}
