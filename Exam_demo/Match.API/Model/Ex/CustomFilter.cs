using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    /// 通用查询条件过滤器
    /// </summary>
    [DataContract]
    public class CustomFilter
    {
        public CustomFilter()
        {
        }


        /// <summary>
        /// 竞赛Id
        /// </summary>
        [DataMember]
        public Nullable<int> Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        [DataMember]
        public int? CollegeId { get; set; }

        /// <summary>
        /// 学校Id（包括默认学校）
        /// </summary>
        [DataMember]
        public Nullable<int> CollegeId2 { get; set; }

        /// <summary>
        /// 学校Id列表
        /// </summary>
        [DataMember]
        public List<int> CollegeIdList { get; set; }

        /// <summary>
        /// 要查询的关键字内容
        /// </summary>
        [DataMember]
        public string QueryFiled { get; set; }

        /// <summary>
        /// 竞赛进行状态，1=已发布，0=未发布，2=已结束，-1=未结束，-2=已发布，已结束
        /// </summary>
        [DataMember]
        public Nullable<int> IsRelease { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [DataMember]
        public string SortName { get; set; }

        /// <summary>
        /// true 升序 false 降序
        /// </summary>
        [DataMember]
        public bool? SortWay { get; set; }


        /// <summary>
        /// 是否删除；1=竞赛管理员删除，0=正常，2=超管删除
        /// </summary>
        [DataMember]
        public int IsDelete { get; set; }

        /// <summary>
        /// 用户的ID
        /// </summary>
        [DataMember]
        public int? UserId { get; set; }

        /// <summary>
        /// ture：已参加大赛、false：未参加大赛
        /// </summary>
        [DataMember]
        public bool JoinIn { get; set; }

        /// <summary>
        /// 要查询的关键字内容(竞赛成绩用)
        /// </summary>
        [DataMember]
        public string KeyWordForResult { get; set; }

        /// <summary>
        /// 查询参赛人数时候使用
        /// </summary>
        [DataMember]
        public string KeyWordForInMatch { get; set; }

        /// <summary>
        /// 统计查询用（搜索范围：姓名，省份、城市、学校、院系）
        /// </summary>
        [DataMember]
        public string KeyWordForStatistic { get; set; }

        /// <summary>
        /// 要查询的竞赛Id(竞赛成绩用)
        /// </summary>
        [DataMember]
        public int? MatchIdForResult { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>
        [DataMember]
        public int? CompetitionId { get; set; }

        /// <summary>
        /// 竞赛报名用户Id
        /// </summary>
        [DataMember]
        public int? UserIdForApply { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [DataMember]
        public int? IDNum { get; set; }

        /// <summary>
        /// 用户的ID
        /// </summary>
        [DataMember]
        public int? UserId2 { get; set; }

        /// <summary>
        /// 是否官网注册，1=官网注册，0=管理员创建
        /// </summary>
        [DataMember]
        public int? IsPageRegistration { get; set; }
    }
}
