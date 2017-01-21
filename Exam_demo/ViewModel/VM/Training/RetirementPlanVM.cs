using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///RetirementPlan
    /// </summary>
    public class RetirementPlanVM
    {
        public RetirementPlanVM()
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
        /// 当前年龄
        /// </summary>		
        public int Age { get; set; }

        /// <summary>
        /// 退休前通货膨胀率
        /// </summary>		
        public decimal BeforeInflationRate { get; set; }

        /// <summary>
        /// 退休后通货膨胀率
        /// </summary>		
        public decimal AfterInflationRate { get; set; }

        /// <summary>
        /// 退休后投资收益率
        /// </summary>		
        public decimal RetirementRate { get; set; }

        /// <summary>
        /// 社保工资增长率
        /// </summary>		
        public decimal? SociaSecurityRate { get; set; }

        /// <summary>
        /// 租金增长率
        /// </summary>		
        public decimal? RentRate { get; set; }

        /// <summary>
        /// 其他收入增长率
        /// </summary>		
        public decimal? OtherRate { get; set; }

        /// <summary>
        /// 计划退休年龄
        /// </summary>		
        public int RetirementAge { get; set; }

        /// <summary>
        /// 希望享有退休生活年限
        /// </summary>		
        public int RetirementYears { get; set; }

        /// <summary>
        /// 目前生活水平
        /// </summary>		
        public decimal LivingStandardNow { get; set; }

        /// <summary>
        /// 生活满意度
        /// </summary>		
        public int Satisfaction { get; set; }

        /// <summary>
        /// 满意生活水平
        /// </summary>		
        public decimal SatisfactionLivingStandard { get; set; }

        /// <summary>
        /// 退休后、退休前生活水平折算比例
        /// </summary>		
        public int ConvertProportion { get; set; }

        /// <summary>
        /// 子女传承费用
        /// </summary>		
        public decimal lineageFee { get; set; }

        private decimal _retirementLivingStandard = 0;
        /// <summary>
        /// 退休时生活水平
        /// </summary>		
        public decimal RetirementLivingStandard
        {
            get
            {
                if (_retirementLivingStandard == 0)
                {
                    double BeforeInflationRate1 = Convert.ToDouble(BeforeInflationRate / 1200);
                    double RetirementAge1 = Convert.ToDouble(RetirementAge - Age);
                    double SatisfactionLivingStandard1 = Convert.ToDouble(SatisfactionLivingStandard);

                    _retirementLivingStandard = Convert.ToDecimal(
                        Microsoft.VisualBasic.Financial.FV(BeforeInflationRate1, RetirementAge1, 0, -SatisfactionLivingStandard1, Microsoft.VisualBasic.DueDate.BegOfPeriod));
                }
                return _retirementLivingStandard;
            }
            set { _retirementLivingStandard = value; }
        }
        /// <summary>
        /// 退休后生活水平
        /// </summary>		
        public decimal AfterLivingStandard { get; set; }

        /// <summary>
        /// 社会保险
        /// </summary>		
        public decimal SocialInsurance { get; set; }

        /// <summary>
        /// 商业保险
        /// </summary>		
        public decimal CommercialInsurance { get; set; }

        /// <summary>
        /// 租金收入
        /// </summary>		
        public decimal RentIncome { get; set; }

        /// <summary>
        /// 其他收入
        /// </summary>		
        public decimal OtherIncome { get; set; }

        private decimal totalIncome = 0;
        /// <summary>
        /// 小计
        /// </summary>		
        public decimal TotalIncome
        {
            get
            {
                if (totalIncome == 0)
                {
                    totalIncome = SocialInsurance + CommercialInsurance + RentIncome + OtherIncome;
                }
                return totalIncome;
            }


            set { totalIncome = value; }
        }




        private decimal _totalAmount;
        /// <summary>
        /// 退休时需准备的现金总金额
        /// </summary>	
        public decimal TotalAmount
        {
            get
            {
                if (_totalAmount == 0)
                {
                    double RetirementRate1 = Convert.ToDouble((((RetirementRate/100 - AfterInflationRate/100)) / (1 + AfterInflationRate/100)) / 12);
                    double RetirementYears1 = Convert.ToDouble(RetirementYears * 12);
                    double TotalIncome1 = Convert.ToDouble(TotalIncome - AfterLivingStandard);
                    double lineageFee1 = Convert.ToDouble(lineageFee);
                    _totalAmount =Convert.ToDecimal(Microsoft.VisualBasic.Financial.PV(RetirementRate1, RetirementYears1, TotalIncome1, lineageFee1, Microsoft.VisualBasic.DueDate.BegOfPeriod));
                }
                return _totalAmount;
            }
            set { _totalAmount = value; }
        }



        /// <summary>
        /// 预计投资收益率
        /// </summary>		
        public decimal ReturnOnInvestmentRate { get; set; }

        /// <summary>
        /// 一次性投入金额
        /// </summary>		
        public decimal DisposableInput { get; set; }

        /// <summary>
        /// 每月定期投资金额
        /// </summary>		
        public decimal MonthlyInvestment { get; set; }

        /// <summary>
        /// 定期定额投资年限
        /// </summary>		
        public decimal RegularYear { get; set; }


        private double _targetAmount;
        /// <summary>
        /// 此方案能实现的目标金额
        /// </summary>		

        public double TargetAmount
        {
            get
            {
                if (_targetAmount == 0)
                {
                    double ReturnOnInvestmentRate1 = (Convert.ToDouble(ReturnOnInvestmentRate / 1200));
                    double RegularYear1 = (Convert.ToDouble(RegularYear * 12));
                    double MonthlyInvestment1 = Convert.ToDouble(MonthlyInvestment);
                    double DisposableInput1 = Convert.ToDouble(DisposableInput);                
                    _targetAmount = Microsoft.VisualBasic.Financial.FV(ReturnOnInvestmentRate1, RegularYear1, -MonthlyInvestment1, -DisposableInput1, Microsoft.VisualBasic.DueDate.EndOfPeriod);
                }

                return _targetAmount;
            }
            set { _targetAmount = value; }
        }


        /// <summary>
        /// 创业规划分析
        /// </summary>		
        public string Analysis { get; set; }


        private decimal _monthMoney;
        /// <summary>
        /// 每月可支配金额
        /// </summary>
        public decimal MonthMoney
        {
            get { return _monthMoney; }
            set { _monthMoney = value; }
        }

        private decimal _userableAsset;
        /// <summary>
        /// 可用资产
        /// </summary>
        public decimal UserableAsset
        {
            get { return _userableAsset; }
            set { _userableAsset = value; }
        }


        private decimal _totalVal = 0;
        /// <summary>
        /// 净值合计
        /// </summary>
        public decimal TotalVal
        {
            get { return _totalVal; }
            set { _totalVal = value; }
        }


    }
}