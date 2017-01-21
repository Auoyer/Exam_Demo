using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///ProposalCustomer
    /// </summary>
    public class ProposalCustomerVM
    {
        public ProposalCustomerVM()
        {
            ProposalCustomerDetailList = new List<ProposalCustomerDetailVM>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 建议书Id
        /// </summary>		
        public int ProposalId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 姓名拼音
        /// </summary>		
        public string PinYin { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>		
        public int Age { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>		
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        public string IDNum { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>		
        public string Tel { get; set; }

        /// <summary>
        /// 手机
        /// </summary>		
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>		
        public string Email { get; set; }

        /// <summary>
        /// 职务
        /// </summary>		
        public string Position { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>		
        public string Company { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>		
        public string Address { get; set; }

        /// <summary>
        /// 客户详细信息集合
        /// </summary>
        public List<ProposalCustomerDetailVM> ProposalCustomerDetailList { get; set; }

    }
}