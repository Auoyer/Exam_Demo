using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///CashPlan
    /// </summary>
    [DataContract]
    public class CashPlan
    {
        public CashPlan()
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
        /// 家庭月支出
        /// </summary>		
        [DataMember]
        public decimal FamilyMonthExpense { get; set; }

        /// <summary>
        /// 现金保留规模
        /// </summary>		
        [DataMember]
        public int RetainCashType { get; set; }

        /// <summary>
        /// 活期存款
        /// </summary>		
        [DataMember]
        public decimal Deposit { get; set; }

        /// <summary>
        /// 货币市场基金
        /// </summary>		
        [DataMember]
        public decimal Fund { get; set; }

        /// <summary>
        /// 信用卡
        /// </summary>		
        [DataMember]
        public decimal CreditCard { get; set; }

        /// <summary>
        /// 现金规划分析
        /// </summary>		
        [DataMember]
        public string Analysis { get; set; }

    }
}