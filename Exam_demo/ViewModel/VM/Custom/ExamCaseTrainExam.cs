using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VM
{
    /// <summary>
    /// 多表联查
    /// </summary> 
    public class ExamCaseTrainExamVM
    {
        public ExamCaseTrainExamVM()
        {
    
        }

        /// <summary>
        /// Id
        /// </summary>		 
        public int Id { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>	 
        public string CustomerName { get; set; } 
        /// <summary>
        /// IDNum
        /// </summary>	 
        public string IDNum { get; set; }
        /// <summary>
        /// FinancialType
        /// </summary>	 
        public int FinancialTypeId { get; set; }
        /// <summary>
        /// FinancialTypeName
        /// </summary>	 
        public string FinancialTypeName { get; set; } 
        /// <summary>
        /// FinancialType
        /// </summary>	 
        public decimal AllScore { get; set; }
    }

}
