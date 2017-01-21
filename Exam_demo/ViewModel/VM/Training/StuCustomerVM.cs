using System;
using System.Collections.Generic;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///潜在客户/已有客户
    /// </summary>
    public class StuCustomerVM
    {
        public StuCustomerVM()
        {
            StuCustomerDetail = new List<StuCustomerDetailVM>();
        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>		
        public int UserId { get; set; }

        /// <summary>
        /// 来源：机会，自定义
        /// </summary>		
        public int Source { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerNo { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>		
        public string CustomerName { get; set; }

        /// <summary>
        /// 潜在客户/已有客户
        /// </summary>		
        public int CustomerType { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>		
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>		
        public string IDNum { get; set; }

        /// <summary>
        /// 客户背景
        /// </summary>		
        public string CustomerStory { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>		
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 姓名拼音
        /// </summary>		
        public string PinYin { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>		
        public int Age { get; set; }

        /// <summary>
        /// 年收入
        /// </summary>		
        public decimal? InCome { get; set; }

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
        /// (冗余字段)建议书数量
        /// </summary>
        public int ProposalCount { get; set; }

        /// <summary>
        /// (冗余字段)状态：新增，修改，提交
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// (冗余字段)建议书Id（建议书新增完成时，需更新此字段）
        /// </summary>
        public int ProposalId { get; set; }

        /// <summary>
        /// (冗余字段)销售机会/实训考核Id
        /// </summary>
        public int TrainExamId { get; set; }

        /// <summary>
        /// 客户家属信息
        /// </summary>
        public List<StuCustomerDetailVM> StuCustomerDetail { get; set; }

        //=========================================================================

        /// <summary>
        /// 客户来源类型名称
        /// </summary>
        public string CustomerSourceName { get; set; }

        /// <summary>
        /// 更新日期字符串
        /// </summary>
        public string strUpdateDate
        {
            get
            {
                return UpdateDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
            }
            private set { }
        }
        /// <summary>
        /// 是否是高净值客户
        /// </summary>
        public bool CustomerHighAssets { get; set; }

    }
}