using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///IncomeAndExpenses
    /// </summary>
    public class IncomeAndExpensesVM
    {
        public IncomeAndExpensesVM()
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
        /// 薪资收入
        /// </summary>		
        public decimal JobIncome { get; set; }

        /// <summary>
        /// 养老保险储蓄
        /// </summary>		
        public decimal EndowmentInsurance { get; set; }

        /// <summary>
        /// 医疗保险储蓄
        /// </summary>		
        public decimal MedicalInsurance { get; set; }

        /// <summary>
        /// 住房公积金储蓄
        /// </summary>		
        public decimal HousingFund { get; set; }

        /// <summary>
        /// 其他工作收入
        /// </summary>		
        public decimal OtherJobIncome { get; set; }

        /// <summary>
        /// 家计支出
        /// </summary>		
        public decimal FamilyExpense { get; set; }

        /// <summary>
        /// 子女教育支出
        /// </summary>		
        public decimal ChildExpense { get; set; }

        /// <summary>
        /// 其他支出
        /// </summary>		
        public decimal OtherExpense { get; set; }

        /// <summary>
        /// 利息收入
        /// </summary>		
        public decimal Interest { get; set; }

        /// <summary>
        /// 资本利得
        /// </summary>		
        public decimal CapitalGains { get; set; }

        /// <summary>
        /// 其他理财收入
        /// </summary>		
        public decimal OtherIncome { get; set; }

        /// <summary>
        /// 利息支出
        /// </summary>		
        public decimal InterestExpense { get; set; }

        /// <summary>
        /// 保障型保费支出
        /// </summary>		
        public decimal InsuranceExpense { get; set; }

        /// <summary>
        /// 其他理财支出
        /// </summary>		
        public decimal OtherFinanceExpense { get; set; }


        private decimal _totalDeposit;
        /// <summary>
        /// 储蓄合计
        /// </summary>
        public decimal TotalDeposit
        {
            get
            {
                if (_totalDeposit == 0)
                {
                    _totalDeposit = ((JobIncome + EndowmentInsurance + MedicalInsurance + HousingFund + OtherJobIncome) - (FamilyExpense + ChildExpense + OtherExpense)) +
                        ((Interest + CapitalGains + OtherIncome) - (InterestExpense + InsuranceExpense + OtherFinanceExpense));
                }

                return _totalDeposit;
            }

        }


        private decimal _FreeMoney;
        /// <summary>
        /// 自由储蓄
        /// </summary>
        public decimal FreeMoney
        {
            get
            {
                if (_FreeMoney == 0)
                {
                    _FreeMoney = TotalDeposit - (EndowmentInsurance + HousingFund);
                }
                return _FreeMoney;

            }
        }

        private decimal _familyMonthExpense;
        /// <summary>
        /// 家庭月支出
        /// </summary>
        public decimal FamilyMonthExpense
        {
            get
            {
                if (_familyMonthExpense == 0)
                {
                    _familyMonthExpense = (FamilyExpense + ChildExpense + OtherExpense + InterestExpense + InsuranceExpense + OtherFinanceExpense) / 12;

                }


                return _familyMonthExpense;
            }
        }

        private decimal _workIncome01;
        /// <summary>
        /// 工作收入小计
        /// </summary>
        public decimal WorkIncome01
        {
            get
            {
                _workIncome01 = (JobIncome + EndowmentInsurance + MedicalInsurance + HousingFund + OtherJobIncome);
                return _workIncome01;
            }
        }


        private decimal _investmentIncome01;
        /// <summary>
        /// 理财收入小计
        /// </summary>
        public decimal InvestmentIncome01
        {
            get {
                _investmentIncome01 = (Interest + CapitalGains + OtherIncome);
                return _investmentIncome01; }
        }


        private decimal _liveExpense01;
        /// <summary>
        /// 生活支出小计
        /// </summary>
        public decimal LiveExpense01
        {
            get {
                _liveExpense01 = (FamilyExpense + ChildExpense + OtherExpense);
                return _liveExpense01; }
        }

        private decimal _investmentExpense01;
        /// <summary>
        /// 理财支出小计
        /// </summary>
        public decimal InvestmentExpense01
        {
            get {
                _investmentExpense01 = (InterestExpense + InsuranceExpense + OtherFinanceExpense);
                return _investmentExpense01; }
         
        }
    }
}