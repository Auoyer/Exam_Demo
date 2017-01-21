using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///CashFlow
    /// </summary>
    public class CashFlowVM
    {
        public CashFlowVM()
        {

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
        /// 投资赎回（实际发生）
        /// </summary>		
        public decimal Redemption { get; set; }

        /// <summary>
        /// 新增投资（实际发生）
        /// </summary>		
        public decimal Investment { get; set; }

        /// <summary>
        /// 借入本金
        /// </summary>		
        public decimal BorrowCapital { get; set; }

        /// <summary>
        /// 还款本金
        /// </summary>		
        public decimal RepaymentCapital { get; set; }

        #region 收支储蓄表中的冗余字段
        /// <summary>
        /// 工作收入小计/收支储蓄
        /// </summary>
        public decimal WorkIncome { get; set; }
        /// <summary>
        /// 生活支出小计/收支储蓄
        /// </summary>
        public decimal LiveExpense { get; set; }
        /// <summary>
        /// 投资收益
        /// </summary>
        public decimal InvestIncome { get; set; }
        /// <summary>
        /// 利息支出
        /// </summary>
        public decimal InterestExpense { get; set; }
        /// <summary>
        /// 保费支出
        /// </summary>
        public decimal InsuranceExpense { get; set; }
        #endregion

        /// <summary>
        /// 判断收支储蓄表是否有值
        /// </summary>
        public bool JudgeVal { get; set; }

    }
}