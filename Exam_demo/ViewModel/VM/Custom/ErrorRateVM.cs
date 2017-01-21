using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    public class ErrorRateVM
    {
        /// <summary>
        /// 模块ID
        /// </summary> 
        public int ModularId { get; set; }
        /// <summary>
        /// 考核模块名称
        /// </summary>
        public string ModularName { get; set; }
        /// <summary>
        /// 考核点ID
        /// </summary> 
        public int AssessmentPoint { get; set; }
        /// <summary>
        /// 考核点名称
        /// </summary>
        public string AssessmentPointName { get; set; }
        /// <summary>
        /// 错误率
        /// </summary> 
        public string Rate { get; set; }
    }
}
