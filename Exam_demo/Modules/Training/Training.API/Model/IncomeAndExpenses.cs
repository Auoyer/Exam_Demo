using System;
using System.Runtime.Serialization;

namespace Training.API
{
    /// <summary>
    ///IncomeAndExpenses
    /// </summary>
    [DataContract]
    public class IncomeAndExpenses
    {
        public IncomeAndExpenses()
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
        /// 薪资收入
        /// </summary>		
        [DataMember]
        public decimal JobIncome { get; set; }

        /// <summary>
        /// 养老保险储蓄
        /// </summary>		
        [DataMember]
        public decimal EndowmentInsurance { get; set; }

        /// <summary>
        /// 医疗保险储蓄
        /// </summary>		
        [DataMember]
        public decimal MedicalInsurance { get; set; }

        /// <summary>
        /// 住房公积金储蓄
        /// </summary>		
        [DataMember]
        public decimal HousingFund { get; set; }

        /// <summary>
        /// 其他工作收入
        /// </summary>		
        [DataMember]
        public decimal OtherJobIncome { get; set; }

        /// <summary>
        /// 家计支出
        /// </summary>		
        [DataMember]
        public decimal FamilyExpense { get; set; }

        /// <summary>
        /// 子女教育支出
        /// </summary>		
        [DataMember]
        public decimal ChildExpense { get; set; }

        /// <summary>
        /// 其他支出
        /// </summary>		
        [DataMember]
        public decimal OtherExpense { get; set; }

        /// <summary>
        /// 利息收入
        /// </summary>		
        [DataMember]
        public decimal Interest { get; set; }

        /// <summary>
        /// 资本利得
        /// </summary>		
        [DataMember]
        public decimal CapitalGains { get; set; }

        /// <summary>
        /// 其他理财收入
        /// </summary>		
        [DataMember]
        public decimal OtherIncome { get; set; }

        /// <summary>
        /// 利息支出
        /// </summary>		
        [DataMember]
        public decimal InterestExpense { get; set; }

        /// <summary>
        /// 保障型保费支出
        /// </summary>		
        [DataMember]
        public decimal InsuranceExpense { get; set; }

        /// <summary>
        /// 其他理财支出
        /// </summary>		
        [DataMember]
        public decimal OtherFinanceExpense { get; set; }

    }
}