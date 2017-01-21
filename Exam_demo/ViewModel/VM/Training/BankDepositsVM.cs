using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///BankDeposits
    /// </summary>
    public class BankDepositsVM
    {
        public BankDepositsVM()
        {

        }

        /// <summary>
        /// Id
        /// </summary>		
        public int Id { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>		
        public string BankName { get; set; }

        /// <summary>
        /// 活期利率
        /// </summary>		
        public decimal DemandDeposit { get; set; }

        /// <summary>
        /// 3月定期利率
        /// </summary>		
        public decimal ThreeMonth { get; set; }

        /// <summary>
        /// 6月定期利率
        /// </summary>		
        public decimal SixMonth { get; set; }

        /// <summary>
        /// 1年定期利率
        /// </summary>		
        public decimal Year { get; set; }

        /// <summary>
        /// 2年定期利率
        /// </summary>		
        public decimal TwoYear { get; set; }

        /// <summary>
        /// 3年定期利率
        /// </summary>		
        public decimal ThreeYear { get; set; }

        /// <summary>
        /// 5年定期利率
        /// </summary>		
        public decimal FiveYear { get; set; }

    }
}