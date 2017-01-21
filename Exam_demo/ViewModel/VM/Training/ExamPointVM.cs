using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ExamPoint
    /// </summary>
    public class ExamPointVM
    {
        public ExamPointVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 考核模块Id
        /// </summary>		
        public int ExamModuleId { get; set; }

        /// <summary>
        /// 考核点名称
        /// </summary>		
        public string ExamPointName { get; set; }

        /// <summary>
        /// 考核点类型：客观、主观
        /// </summary>		
        public int ExamPointType { get; set; }

        /// <summary>
        /// 考核点对应表名
        /// </summary>		
        public string TableName { get; set; }

        /// <summary>
        /// 考核点对应列名
        /// </summary>		
        public string FieldName { get; set; }

        /// <summary>
        /// 考核点类型，如:int等
        /// </summary>		
        public string TypeName { get; set; }

        /// <summary>
        /// 标准答案
        /// </summary>		
        public string Answer { get; set; }

    }
}