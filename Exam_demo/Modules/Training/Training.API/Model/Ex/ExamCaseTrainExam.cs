using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    /// <summary>
    /// 多表联查
    /// </summary>
    [DataContract]
    public class ExamCaseTrainExam
    {
        public ExamCaseTrainExam()
        {
    
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; } 
        /// <summary>
        /// IDNum
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }
        /// <summary>
        /// FinancialType
        /// </summary>		
        [DataMember]
        public int FinancialTypeId { get; set; } 
        /// <summary>
        /// FinancialType
        /// </summary>		
        [DataMember]
        public decimal AllScore { get; set; }
    }

}
