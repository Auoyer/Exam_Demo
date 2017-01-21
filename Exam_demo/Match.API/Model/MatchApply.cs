using System;
using System.Runtime.Serialization;

namespace Match.API
{
    /// <summary>
    ///MatchApply
    /// </summary>
    [DataContract]
    public class MatchApply
    {
        public MatchApply()
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
        /// ApplyUser
        /// </summary>		
        [DataMember]
        public int ApplyUser { get; set; }

        /// <summary>
        /// ApplyStatus 0:未处理，1：同意，2：拒绝
        /// </summary>		
        [DataMember]
        public int ApplyStatus { get; set; }

        /// <summary>
        /// UserName
        /// </summary>		
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// IDNum
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }
    }
}