using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TrainExamDetail
    /// </summary>
    public class TrainExamDetailVM
    {
        public TrainExamDetailVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 实训考核/销售机会Id
        /// </summary>		
        public int TrainExamId { get; set; }

        /// <summary>
        /// 考核点Id
        /// </summary>		
        public int ExamPointId { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModularId { get; set; }

        /// <summary>
        /// 考核点类型Id---主观题、客观题
        /// </summary>
        public int ExamPointType { get; set; }
    }
}