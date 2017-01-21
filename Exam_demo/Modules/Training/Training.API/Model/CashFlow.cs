using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///CashFlow
    /// </summary>
    [DataContract]
    public class CashFlow
    {
        public CashFlow()
        {

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
        /// 投资赎回（实际发生）
        /// </summary>		
        [DataMember]
        public decimal Redemption { get; set; }

        /// <summary>
        /// 新增投资（实际发生）
        /// </summary>		
        [DataMember]
        public decimal Investment { get; set; }

        /// <summary>
        /// 借入本金
        /// </summary>		
        [DataMember]
        public decimal BorrowCapital { get; set; }

        /// <summary>
        /// 还款本金
        /// </summary>		
        [DataMember]
        public decimal RepaymentCapital { get; set; }
        /// <summary>
        /// 工作收入小计/收支储蓄
        /// </summary>
        [DataMember]
        public decimal WorkIncome { get; set; }
        /// <summary>
        /// 生活支出
        /// </summary>
        [DataMember]
        public decimal LiveExpense { get; set; }
        /// <summary>
        /// 投资收益
        /// </summary>
        [DataMember]
        public decimal InvestIncome { get; set; }

        /// <summary>
        /// 利息支出
        /// </summary>
        [DataMember]
        public decimal InterestExpense { get; set; }

        /// <summary>
        /// 保费支出
        /// </summary>
        [DataMember]
        public decimal InsuranceExpense { get; set; }

    }
}