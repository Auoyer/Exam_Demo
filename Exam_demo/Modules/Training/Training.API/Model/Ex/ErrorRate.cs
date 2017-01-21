using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    [DataContract]
    public class ErrorRate
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        [DataMember]
        public int ModularId { get; set; }

        /// <summary>
        /// 考核点ID
        /// </summary>
        [DataMember]
        public int AssessmentPoint { get; set; }
        /// <summary>
        /// 错误率
        /// </summary>
        [DataMember]
        public decimal Rate { get; set; }
    }
}
