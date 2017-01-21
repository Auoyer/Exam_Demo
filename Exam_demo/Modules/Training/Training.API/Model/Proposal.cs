using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///Proposal
    /// </summary>
    [DataContract]
    public class Proposal
    {
        public Proposal()
        {
  
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 实训考核/销售机会Id
        /// </summary>		
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

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
        /// 状态 0：无 1：未提交 2：未审核 3：已审核 4：已删除
        /// </summary>		
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>		
        [DataMember]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        [DataMember]
        public int StuCustomerId { get; set; }
    }
}