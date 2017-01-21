using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///StuCustomer
    /// </summary>
    [DataContract]
    public class StuCustomer
    {
        public StuCustomer()
        {
            StuCustomerDetail = new List<StuCustomerDetail>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 来源：机会，自定义
        /// </summary>		
        [DataMember]
        public int Source { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        [DataMember]
        public string CustomerNo { get; set; }

        /// <summary>
        /// 潜在客户/已有客户
        /// </summary>		
        [DataMember]
        public int CustomerType { get; set; }

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
        /// 客户背景
        /// </summary>		
        [DataMember]
        public string CustomerStory { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>		
        [DataMember]
        public DateTime UpdateDate { get; set; }

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
        /// 年收入
        /// </summary>		
        [DataMember]
        public decimal? InCome { get; set; }

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
        /// (冗余字段)建议书数量
        /// </summary>
        [DataMember]
        public int ProposalCount { get; set; }

        /// <summary>
        /// (冗余字段)状态：新增，修改，提交
        /// </summary>
       [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// (冗余字段)建议书Id（建议书新增完成时，需更新此字段）
        /// </summary>
        [DataMember]
        public int ProposalId { get; set; }

        /// <summary>
        /// (冗余字段)销售机会/实训考核Id
        /// </summary>
        [DataMember]
        public int TrainExamId { get; set; }

        /// <summary>
        ///（冗余字段）高净资产的客户资产数
        /// </summary>
        [DataMember]
        public bool CustomerHighAssets { get; set; }
     

        /// <summary>
        /// 客户家属信息
        /// </summary>
        [DataMember]
        public List<StuCustomerDetail> StuCustomerDetail { get; set; }

    }
}