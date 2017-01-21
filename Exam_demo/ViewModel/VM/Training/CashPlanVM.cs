using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///CashPlan
    /// </summary>
    public class CashPlanVM
    {
        public CashPlanVM()
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
        /// 家庭月支出
        /// </summary>		
        public decimal FamilyMonthExpense { get; set; }

        /// <summary>
        /// 现金保留规模
        /// </summary>		
        public int RetainCashType { get; set; }

        /// <summary>
        /// 活期存款
        /// </summary>		
        public decimal Deposit { get; set; }

        /// <summary>
        /// 货币市场基金
        /// </summary>		
        public decimal Fund { get; set; }

        /// <summary>
        /// 信用卡
        /// </summary>		
        public decimal CreditCard { get; set; }

        /// <summary>
        /// 现金规划分析
        /// </summary>		
        public string Analysis { get; set; }



      
        


        private decimal _retainCashMultiple;
        /// <summary>
        /// 现金保留规模--数值
        /// </summary>
        public decimal RetainCashMultiple
        {
            get {
                if (_retainCashMultiple == 0)
                {
                    _retainCashMultiple = FamilyMonthExpense * RetainCashType;
                }
                
                return _retainCashMultiple; }

        }

    }
}