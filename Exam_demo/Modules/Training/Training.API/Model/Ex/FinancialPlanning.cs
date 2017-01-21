using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Training.API
{
    /// <summary>
    /// 理财规划类
    /// </summary>
    [DataContract]
    public class FinancialPlanning
    {
        public FinancialPlanning()
        {
    
        }

        /// <summary>
        /// 建议书ID
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 建议书编号
        /// </summary>		
        [DataMember]
        public string ProposalNum { get; set; }

        /// <summary>
        /// 建议书名称
        /// </summary>		
        [DataMember]
        public string ProposalName { get; set; }

        /// <summary>
        /// 建议书客户名称
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 建议书客户身份证号
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }

        /// <summary>
        /// 理财类型Id
        /// </summary>		
        [DataMember]
        public int? FinancialTypeId { get; set; }

        /// <summary>
        /// 理财类型名称
        /// </summary>		
        [DataMember]
        public string FinancialTypeName { get; set; }

        /// <summary>
        /// 建议书创建日期
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 建议书更新日期
        /// </summary>		
        [DataMember]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 销售机会结束日期
        /// </summary>		
        [DataMember]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 建议书目前的状态
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 建议书目前的状态名称
        /// </summary>	
        [DataMember]
        public string StatusName { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 销售机会Id
        /// </summary>		
        [DataMember]
        public int TrainExamId { get; set; }
        /// <summary>
        /// 潜在客户ID
        /// </summary>
        [DataMember]
        public int StuCustomerId { get; set; }
        /// <summary>
        /// 获取客户类型1潜在2：已有
        /// </summary>
        [DataMember]
        public int CustomerType { get; set; }
    }
}
