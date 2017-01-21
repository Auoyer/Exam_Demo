using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///ExamModule
    /// </summary>
    [DataContract]
    public class ExamModule
    {
        public ExamModule()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 考核内容Id(枚举)
        /// </summary>		
        [DataMember]
        public int ExamContentId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>		
        [DataMember]
        public string ExamModuleName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        [DataMember]
        public int Sort { get; set; }

    }
}