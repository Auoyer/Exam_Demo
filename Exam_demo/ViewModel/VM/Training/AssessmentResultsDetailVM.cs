using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 考核明细
    /// </summary>
    public class AssessmentResultsDetailVM
    {
        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 考核结果主表
        /// </summary>		
        public int AssessmentResultsId { get; set; }
        /// <summary>
        /// 考核点类型--客观主观
        /// </summary>
        public int ExamPointType { get; set; }

        /// <summary>
        /// 模块名称--对应的模块类型
        /// </summary>		
        public int ModularId { get; set; }

        /// <summary>
        /// 考核点
        /// </summary>		
        public int AssessmentPoint { get; set; }

        /// <summary>
        /// 状态
        /// </summary>		
        public int Status { get; set; }

        /// <summary>
        /// 得分
        /// </summary>		
        public decimal Score { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModularName { get; set; }
        /// <summary>
        /// 考核点名称
        /// </summary>
        public string AssessmentPointName { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        public int ExamPointId {
            get { return AssessmentPoint; }
        }

    }
}
