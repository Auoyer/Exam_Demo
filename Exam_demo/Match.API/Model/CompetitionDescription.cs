using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    /// <summary>
    ///大赛说明
    /// </summary>
    [DataContract]
    public class CompetitionDescription
    {
        public CompetitionDescription()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 大赛说明设置
        /// </summary>		
        [DataMember]
        public string ComDesSettings { get; set; }

        /// <summary>
        /// 活动日程设置
        /// </summary>		
        [DataMember]
        public string EventSchedule { get; set; }

        /// <summary>
        /// 问题解答设置
        /// </summary>		
        [DataMember]
        public string TroubleShooting { get; set; }

        /// <summary>
        /// CollegeId
        /// </summary>		
        [DataMember]
        public int CollegeId { get; set; }

    }
}
