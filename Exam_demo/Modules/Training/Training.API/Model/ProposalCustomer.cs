using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///建议书客户信息
    /// </summary>
    [DataContract]
    public class ProposalCustomer
    {
        public ProposalCustomer()
        {
            ProposalCustomerDetailList = new List<ProposalCustomerDetail>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        [DataMember]
        public int ProposalId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 姓名拼音
        /// </summary>		
        [DataMember]
        public string PinYin { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>		
        [DataMember]
        public int Age { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>		
        [DataMember]
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        [DataMember]
        public string IDNum { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>		
        [DataMember]
        public string Tel { get; set; }

        /// <summary>
        /// 手机
        /// </summary>		
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>		
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// 职务
        /// </summary>		
        [DataMember]
        public string Position { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>		
        [DataMember]
        public string Company { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>		
        [DataMember]
        public string Address { get; set; }

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
        /// 状态
        /// </summary>	
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 客户详细信息集合
        /// </summary>
        [DataMember]
        public List<ProposalCustomerDetail> ProposalCustomerDetailList { get; set; }

    }
}