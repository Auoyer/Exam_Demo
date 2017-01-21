using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ExamCase
    /// </summary>
    public class ExamCaseVM
    {
        public ExamCaseVM()
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
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>		
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型
        /// </summary>		
        public int FinancialTypeId { get; set; }

        /// <summary>
        /// 理财类型
        /// </summary>		
        public string strFinancialType { get; set; }

        /// <summary>
        /// 客户背景
        /// </summary>		
        public string CustomerStory { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 用户name
        /// </summary>		
        public string strUserName { get; set; }

        /// <summary>
        /// (扩展字段)理财类型
        /// </summary>		
        public string FinancialTypeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public string strCreateTime {
            get
            {
                return CreateTime.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            set { }
        
        }



        ///// <summary>
        ///// (扩展字段)实训考核/销售机会详细信息
        ///// </summary>
        //public TrainExamDetailVM TrainExamDetailVM { get; set; }

        ///// <summary>
        ///// (扩展字段)实训考核/销售机会发布班级
        ///// </summary>
        //public TrainExamClassVM TrainExamClassVM { get; set; }
    }
}