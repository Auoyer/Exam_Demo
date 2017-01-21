using System;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    ///参赛人员表
    /// </summary>
    [DataContract]
    public class CompetitionUser
    {
        public CompetitionUser()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// CompetitionId
        /// </summary>		
        [DataMember]
        public int CompetitionId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 分组Id
        /// </summary>		
        [DataMember]
        public int GroupId { get; set; }

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

    }
}