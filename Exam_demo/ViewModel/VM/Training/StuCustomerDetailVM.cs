using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///StuCustomerDetail
    /// </summary>
    public class StuCustomerDetailVM
    {
        public StuCustomerDetailVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>		
        public int CustomerId { get; set; }

        /// <summary>
        /// 家属姓名
        /// </summary>		
        public string DependentName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>		
        public int Age { get; set; }

        /// <summary>
        /// 与客户关系
        /// </summary>		
        public string Relation { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>		
        public decimal? InCome { get; set; }

    }
}