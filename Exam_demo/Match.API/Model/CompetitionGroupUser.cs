using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    /// <summary>
    /// 竞赛用户分组信息
    /// </summary>
    [DataContract]
    public class CompetitionGroupUser
    {
        public CompetitionGroupUser()
        {

        }

        /// <summary>
        /// 学校ID
        /// </summary>
        [DataMember]
        public int CollegeId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [DataMember]
        public int GroupId { get; set; }

        /// <summary>
        /// 用户姓名s
        /// </summary>
        [DataMember]
        public string UserNames { get; set; }

        /// <summary>
        /// 赛组来源（1批量导入、2手动导入、3报名审核）
        /// </summary>
        [DataMember]
        public int GroupSouce { get; set; }

        /// <summary>
        /// 审核状态（0待审核、1已审核、2已拒绝）
        /// </summary>
        [DataMember]
        public int IsAudit { get; set; }


        /// <summary>
        /// 队伍人数
        /// </summary>
        [DataMember]
        public int TeamNumber { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public int UserId { get; set; }
    }
}
