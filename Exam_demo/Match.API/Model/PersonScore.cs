using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Match.API
{
    [DataContract]
    public class PersonScore
    {
        /// <summary>
        /// 分数段
        /// </summary>
        /// 
        [DataMember]
        public string ScoreSegment { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        [DataMember]
        public int Persons { get; set; }

        /// <summary>
        /// 最高分
        /// </summary>
        [DataMember]
        public decimal MaxScore { get; set; }
    }
}
