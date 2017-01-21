using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///TrainExamClass
    /// </summary>
    public class TrainExamClassVM
    {
        public TrainExamClassVM()
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
        /// 班级Id
        /// </summary>		
        public int ClassId { get; set; }

        /// <summary>
        /// 班级
        /// </summary>		
        public string ClassName { get; set; }

    }
}