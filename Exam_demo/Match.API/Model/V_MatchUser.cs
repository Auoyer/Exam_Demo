using System;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    ///V_MatchUser
    /// </summary>
    [DataContract]
    public class V_MatchUser
    {
        public V_MatchUser()
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
        /// GroupId
        /// </summary>		
        [DataMember]
        public int GroupId { get; set; }

        /// <summary>
        /// GroupSouce
        /// </summary>		
        [DataMember]
        public int GroupSouce { get; set; }

        /// <summary>
        /// IsAudit
        /// </summary>		
        [DataMember]
        public int IsAudit { get; set; }

        /// <summary>
        /// UserName
        /// </summary>		
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// IDCard
        /// </summary>		
        [DataMember]
        public string IDCard { get; set; }

        /// <summary>
        /// AccountNo
        /// </summary>		
        [DataMember]
        public string AccountNo { get; set; }

        /// <summary>
        /// CollegeName
        /// </summary>		
        [DataMember]
        public string CollegeName { get; set; }

        /// <summary>
        /// 是否进入复赛，1=入围复赛
        /// </summary>
        [DataMember]
        public int IsFinal { get; set; }
    }
}