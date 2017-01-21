using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 竞赛查询通用类
    /// </summary>
    public class MatchSearch
    {
        /// <summary>
        /// 竞赛Id
        /// </summary>
        public Nullable<int> Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public Nullable<int> CollegeId { get; set; }

        /// <summary>
        /// 学校Id（包括默认学校）
        /// </summary>
        public Nullable<int> CollegeId2 { get; set; }

        /// <summary>
        /// 竞赛类型；1=单项理论赛；2=单项实训赛；3=复合赛
        /// </summary>
        public Nullable<int> Type { get; set; }

        /// <summary>
        /// 是否发布；1=已发布，0=未发布，2=已结束，-1=未结束，-2=已发布和已结束
        /// </summary>
        public Nullable<int> IsRelease { get; set; }

        /// <summary>
        /// 用户的ID
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// ture：已参加、false：未参加
        /// </summary>
        public bool JoinIn { get; set; }

        /// <summary>
        /// 竞赛Id
        /// </summary>
        public int? CompetitionId { get; set; }

        /// <summary>
        /// 竞赛报名用户Id
        /// </summary>
        public int? UserIdForApply { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public int? IDNum { get; set; }

        /// <summary>
        /// 用户的ID
        /// </summary>
        public int? UserId2 { get; set; }
    }
}
