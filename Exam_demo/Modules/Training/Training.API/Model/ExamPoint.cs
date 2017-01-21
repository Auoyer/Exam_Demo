using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ExamPoint
    /// </summary>
    [DataContract]
    public class ExamPoint
    {
        public ExamPoint()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 考核模块Id
        /// </summary>		
        [DataMember]
        public int ExamModuleId { get; set; }

        /// <summary>
        /// 考核点名称
        /// </summary>		
        [DataMember]
        public string ExamPointName { get; set; }

        /// <summary>
        /// 考核点类型：客观、主观
        /// </summary>		
        [DataMember]
        public int ExamPointType { get; set; }

        /// <summary>
        /// 考核点对应表名
        /// </summary>		
        [DataMember]
        public string TableName { get; set; }

        /// <summary>
        /// 考核点对应列名
        /// </summary>		
        [DataMember]
        public string FieldName { get; set; }

        /// <summary>
        /// 考核点类型，如:int等
        /// </summary>		
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// 标准答案
        /// </summary>		
        [DataMember]
        public string Answer { get; set; }

    }
}