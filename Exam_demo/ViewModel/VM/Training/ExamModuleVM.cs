using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ExamModule
    /// </summary>
    public class ExamModuleVM
    {
        public ExamModuleVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 考核内容Id(枚举)
        /// </summary>		
        public int ExamContentId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>		
        public string ExamModuleName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int Sort { get; set; }

    }
}