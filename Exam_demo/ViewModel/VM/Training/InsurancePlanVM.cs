using System;
using System.Globalization;

namespace VM
{
    /// <summary>
    ///InsurancePlan
    /// </summary>
    public class InsurancePlanVM
    {
        public InsurancePlanVM()
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
        /// 寿险需求测算方法Id
        /// </summary>		
        public int MethodTypeId { get; set; }

        /// <summary>
        /// 被保险人年龄B
        /// </summary>		
        public int Age1 { get; set; }

        /// <summary>
        /// 被保险人年龄C
        /// </summary>		
        public int Age2 { get; set; }

        /// <summary>
        /// 预计退休年龄B
        /// </summary>		
        public int RetirementAge1 { get; set; }

        /// <summary>
        /// 预计退休年龄C
        /// </summary>		
        public int RetirementAge2 { get; set; }

        /// <summary>
        /// 投资报酬率B
        /// </summary>		
        public decimal ReturnOnInvestment1 { get; set; }

        /// <summary>
        /// 投资报酬率C
        /// </summary>		
        public decimal ReturnOnInvestment2 { get; set; }

        /// <summary>
        /// 通货膨胀率B
        /// </summary>		
        public decimal InflationRate1 { get; set; }

        /// <summary>
        /// 通货膨胀率C
        /// </summary>		
        public decimal InflationRate2 { get; set; }

        /// <summary>
        /// 收入增长率B
        /// </summary>		
        public decimal RevenueGrowth1 { get; set; }

        /// <summary>
        /// 收入增长率C
        /// </summary>		
        public decimal RevenueGrowth2 { get; set; }

        /// <summary>
        /// 当前的家庭生活费用B
        /// </summary>		
        public decimal MatrimonialFee1 { get; set; }

        /// <summary>
        /// 当前的家庭生活费用C
        /// </summary>		
        public decimal MatrimonialFee2 { get; set; }

        /// <summary>
        /// 保险事故发生后支出调整率B
        /// </summary>		
        public decimal AfterAccidentRate1 { get; set; }

        /// <summary>
        /// 保险事故发生后支出调整率C
        /// </summary>		
        public decimal AfterAccidentRate2 { get; set; }
        /// <summary>
        /// 个人年支出
        /// </summary>
        public decimal Expenditure { get; set; }

        /// <summary>
        /// 个人/配偶的个人年收入B
        /// </summary>		
        public decimal Income1 { get; set; }

        /// <summary>
        /// 个人/配偶的个人年收入C
        /// </summary>		
        public decimal Income2 { get; set; }

        /// <summary>
        /// 紧急备用金现值B
        /// </summary>		
        public decimal ReserveFund1 { get; set; }

        /// <summary>
        /// 紧急备用金现值C
        /// </summary>		
        public decimal ReserveFund2 { get; set; }

        /// <summary>
        /// 教育金现值B
        /// </summary>		
        public decimal EduAmount1 { get; set; }

        /// <summary>
        /// 教育金现值C
        /// </summary>		
        public decimal EduAmount2 { get; set; }

        /// <summary>
        /// 养老基金现值B
        /// </summary>		
        public decimal PensionFunds1 { get; set; }

        /// <summary>
        /// 养老基金现值C
        /// </summary>		
        public decimal PensionFunds2 { get; set; }

        /// <summary>
        /// 临终及丧葬支出现值B
        /// </summary>		
        public decimal DeathExpense1 { get; set; }

        /// <summary>
        /// 临终及丧葬支出现值C
        /// </summary>		
        public decimal DeathExpense2 { get; set; }

        /// <summary>
        /// 目前贷款余额B
        /// </summary>		
        public decimal LoanBalance1 { get; set; }

        /// <summary>
        /// 目前贷款余额C
        /// </summary>		
        public decimal LoanBalance2 { get; set; }

        /// <summary>
        /// 家庭生息资产B
        /// </summary>		
        public decimal EarningAssets1 { get; set; }

        /// <summary>
        /// 家庭生息资产C
        /// </summary>		
        public decimal EarningAssets2 { get; set; }

        /// <summary>
        /// 已有额度B
        /// </summary>		
        public decimal InsuranceAmount1 { get; set; }

        /// <summary>
        /// 已有额度C
        /// </summary>		
        public decimal InsuranceAmount2 { get; set; }

        /// <summary>
        /// 预算金额B
        /// </summary>		
        public decimal BudgetAmount1 { get; set; }

        /// <summary>
        /// 预算金额C
        /// </summary>		
        public decimal BudgetAmount2 { get; set; }

        /// <summary>
        /// 补充额度B
        /// </summary>		
        public decimal SupplementaryQuota1 { get; set; }

        /// <summary>
        /// 补充额度C
        /// </summary>		
        public decimal SupplementaryQuota2 { get; set; }

        /// <summary>
        /// 保险规划分析
        /// </summary>		
        public string Analysis { get; set; }


        #region 显示字段---遗属法则算法
        /// <summary>
        /// 投保人年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 计划退休年龄
        /// </summary>		
        public int RetirementAge { get; set; }


        private decimal _monthMoney;
        /// <summary>
        /// 每月可支配金额
        /// </summary>
        public decimal MonthMoney
        {
            get { return _monthMoney; }
            set { _monthMoney = value; }
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

        private decimal _userableAsset;
        /// <summary>
        /// 可用资产
        /// </summary>
        public decimal UserableAsset
        {
            get { return _userableAsset; }
            set { _userableAsset = value; }
        }

        /// <summary>
        /// 投保人姓名
        /// </summary>
        public string InsureName { get; set; }

        /// <summary>
        /// 配偶名字
        /// </summary>
        public string SpouseName { get; set; }

        private decimal _familyExpensesPay1;
        /// <summary>
        ///家庭生活费用实质报酬率1
        /// </summary>
        public decimal FamilyExpensesPay1
        {
            get
            {
                if (_familyExpensesPay1 == 0)
                {
                    _familyExpensesPay1 =Convert.ToDecimal(((((ReturnOnInvestment1/100 + 1) / (InflationRate1/100 + 1)) - 1)*100).ToString("0.00"));
                }


                return _familyExpensesPay1;
            }
            set { _familyExpensesPay1 = value; }
        }

        private decimal _familyExpensesPay2;
        /// <summary>
        ///家庭生活费用实质报酬率2
        /// </summary>
        public decimal FamilyExpensesPay2
        {
            get
            {
                if (_familyExpensesPay2 == 0)
                {
                    _familyExpensesPay2 = Convert.ToDecimal(((((ReturnOnInvestment2/100 + 1) / (InflationRate2/100 + 1)) - 1)*100).ToString("0.00"));
                }

                return _familyExpensesPay2;
            }
            set { _familyExpensesPay2 = value; }
        }



        private decimal _familyIncomePay1;
        /// <summary>
        /// 家庭收入实质报酬率1
        /// </summary>
        public decimal FamilyIncomePay1
        {
            get
            {
                if (_familyIncomePay1 == 0)
                {
                    _familyIncomePay1 = Convert.ToDecimal(((((ReturnOnInvestment1/100 + 1) / (RevenueGrowth1/100 + 1)) - 1)*100).ToString("0.00"));
                }

                return _familyIncomePay1;
            }
            set { _familyIncomePay1 = value; }
        }


        private decimal _familyIncomePay2;
        /// <summary>
        ///  家庭收入实质报酬率2
        /// </summary>
        public decimal FamilyIncomePay2
        {
            get
            {
                if (_familyIncomePay2 == 0)
                {
                    _familyIncomePay2 = Convert.ToDecimal(((((ReturnOnInvestment2/100 + 1) / (RevenueGrowth2/100 + 1)) - 1)*100).ToString("0.00"));
                }
                return _familyIncomePay2;
            }
            set { _familyIncomePay2 = value; }
        }


        private int _familyFutureSaving1;
        /// <summary>
        /// 家庭未来生活费准备年数1
        /// </summary>
        public int FamilyFutureSaving1
        {
            get
            {
                if (_familyFutureSaving1 == 0)
                {
                    _familyFutureSaving1 = RetirementAge2 - Age2;
                }

                return _familyFutureSaving1;
            }
            set { _familyFutureSaving1 = value; }
        }


        private int _familyFutureSaving2;
        /// <summary>
        /// 家庭未来生活费准备年数2
        /// </summary>
        public int FamilyFutureSaving2
        {
            get
            {
                if (_familyFutureSaving2 == 0)
                {
                    _familyFutureSaving2 = RetirementAge1 - Age1;
                }

                return _familyFutureSaving2;
            }
            set { _familyFutureSaving2 = value; }
        }


        private decimal _adjustMatrimonialFee1;
        /// <summary>
        /// 调整后家庭生活费用1
        /// </summary>
        public decimal AdjustMatrimonialFee1
        {
            get
            {
                if (_adjustMatrimonialFee1 == 0)
                {
                    _adjustMatrimonialFee1 = MatrimonialFee1 * AfterAccidentRate1/100;
                }

                return _adjustMatrimonialFee1;
            }
            set { _adjustMatrimonialFee1 = value; }
        }


        private decimal _adjustMatrimonialFee2;
        /// <summary>
        /// 调整后家庭生活费用2
        /// </summary>
        public decimal AdjustMatrimonialFee2
        {
            get
            {
                if (_adjustMatrimonialFee2 == 0)
                {
                    _adjustMatrimonialFee2 = MatrimonialFee2 * AfterAccidentRate2/100;
                }

                return _adjustMatrimonialFee2;
            }
            set { _adjustMatrimonialFee2 = value; }
        }

        //
        private decimal _matrimonialFeeNow1;
        /// <summary>
        /// 家庭生活费用现值1
        /// </summary>
        public decimal MatrimonialFeeNow1
        {
            get
            {
                if (_matrimonialFeeNow1 == 0)
                {
                    //PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
                    double familyExpensesPay01 = Convert.ToDouble(FamilyExpensesPay1);
                    double familyFutureSaving01 = Convert.ToDouble(FamilyFutureSaving1);
                    double adjustMatrimonialFee01 = Convert.ToDouble(AdjustMatrimonialFee1);

                    _matrimonialFeeNow1 = Convert.ToDecimal(Microsoft.VisualBasic.Financial.PV(familyExpensesPay01/100, familyFutureSaving01, -adjustMatrimonialFee01, 0, Microsoft.VisualBasic.DueDate.BegOfPeriod));
                }

                return _matrimonialFeeNow1;
            }
            set { _matrimonialFeeNow1 = value; }
        }


        private double _matrimonialFeeNow2;
        /// <summary>
        /// 家庭生活费用现值2
        /// </summary>
        public double MatrimonialFeeNow2
        {
            get
            {
                if (_matrimonialFeeNow2 == 0)
                {
                    //PV(家庭生活费用实质报酬率,家庭未来生活费准备年数,-调整后家庭生活费用, 0, 1)
                    double familyExpensesPay02 = Convert.ToDouble(FamilyExpensesPay2);
                    double familyFutureSaving02 = Convert.ToDouble(FamilyFutureSaving2);
                    double adjustMatrimonialFee02 = Convert.ToDouble(AdjustMatrimonialFee2);

                    _matrimonialFeeNow2 = Microsoft.VisualBasic.Financial.PV(familyExpensesPay02/100, familyFutureSaving02, -adjustMatrimonialFee02, 0, Microsoft.VisualBasic.DueDate.BegOfPeriod);
                }

                return _matrimonialFeeNow2;
            }
            set { _matrimonialFeeNow2 = value; }
        }



        private decimal _spouseMonthIncome1;
        /// <summary>
        /// 配偶的个人收入现值/元1
        /// </summary>
        public decimal SpouseMonthIncome1
        {
            get
            {
                if (_spouseMonthIncome1 == 0)
                {
                    //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入)  B16= PV(B8, B10, -B15) 输出；C16= PV(C8, C10, C15) 输出
                    double FamilyIncomePay01 = Convert.ToDouble(FamilyIncomePay1);
                    double familyFutureSaving01 = Convert.ToDouble(FamilyFutureSaving1);
                    double income01 = Convert.ToDouble(Income1);

                    _spouseMonthIncome1 = Convert.ToDecimal(Microsoft.VisualBasic.Financial.PV(FamilyIncomePay01 / 100, familyFutureSaving01, -income01));

                }

                return _spouseMonthIncome1;
            }
            set { _spouseMonthIncome1 = value; }
        }


        private decimal _spouseMonthIncome2;
        /// <summary>
        /// 配偶的个人收入现值/元2
        /// </summary>
        public decimal SpouseMonthIncome2
        {
            get
            {
                if (_spouseMonthIncome2 == 0)
                {
                    //	配偶的个人收入现值=PV(家庭收入实质报酬率,家庭未来生活费准备年数,配偶的个人年收入)  B16= PV(B8, B10, -B15) 输出；C16= PV(C8, C10, C15) 输出
                    double FamilyIncomePay02 = Convert.ToDouble(FamilyIncomePay2);
                    double familyFutureSaving02 = Convert.ToDouble(FamilyFutureSaving2);
                    double income02 = Convert.ToDouble(Income2);

                    _spouseMonthIncome2 = Convert.ToDecimal(Microsoft.VisualBasic.Financial.PV(FamilyIncomePay02 / 100, familyFutureSaving02, -income02));
                }

                return _spouseMonthIncome2;
            }
            set { _spouseMonthIncome2 = value; }
        }


        private decimal _familyLiveOverdraft1;
        /// <summary>
        /// 家庭未来生活费用缺口现值/元1
        /// </summary>
        public decimal FamilyLiveOverdraft1
        {
            get
            {
                if (_familyLiveOverdraft1 == 0)
                {
                    //	家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
                    _familyLiveOverdraft1 = SpouseMonthIncome1 - MatrimonialFeeNow1;
                }

                return _familyLiveOverdraft1;
            }
            set { _familyLiveOverdraft1 = value; }
        }

        private double _familyLiveOverdraft2;
        /// <summary>
        /// 家庭未来生活费用缺口现值/元2
        /// </summary>
        public double FamilyLiveOverdraft2
        {
            get
            {
                if (_familyLiveOverdraft2 == 0)
                {
                    //	家庭未来生活费用缺口现值=配偶的个人收入现值-家庭生活费用现值 
                    _familyLiveOverdraft2 =Convert.ToDouble(SpouseMonthIncome2) - MatrimonialFeeNow2;
                }

                return _familyLiveOverdraft2;
            }
            set { _familyLiveOverdraft2 = value; }
        }



        private decimal _relativeFinancial1;

        /// <summary>
        /// 遗属需求法应有的寿险保额1
        /// </summary>
        public decimal RelativeFinancial1
        {
            get
            {
                //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资产
                if (_relativeFinancial1 == 0)
                {
                    _relativeFinancial1 = (FamilyLiveOverdraft1 + ReserveFund1 + EduAmount1 + PensionFunds1 + DeathExpense1 + LoanBalance1) - EarningAssets1;
                }


                return _relativeFinancial1;
            }
            set { _relativeFinancial1 = value; }
        }



        private double _relativeFinancial2;

        /// <summary>
        /// 	遗属需求法应有的寿险保额2
        /// </summary>
        public double RelativeFinancial2
        {
            get
            {
                if (_relativeFinancial2 == 0)
                {
                    _relativeFinancial2 = (FamilyLiveOverdraft2 +Convert.ToDouble(ReserveFund2) +Convert.ToDouble(EduAmount2) +Convert.ToDouble(PensionFunds2) +Convert.ToDouble(DeathExpense2) +Convert.ToDouble(LoanBalance2)) -Convert.ToDouble(EarningAssets2);
                }

                return _relativeFinancial2;
            }
            set { _relativeFinancial2 = value; }
        }

        private decimal _insureNeedCash1;
        /// <summary>
        /// 保险需求额度/元1
        /// </summary>
        public decimal InsureNeedCash1
        {
            get
            {
                if (RelativeFinancial1 != 0)
                {
                    _insureNeedCash1 = RelativeFinancial1;
                }
                else if (FutureAnnuityIncome!=0)
                {
                    _insureNeedCash1 = FutureAnnuityIncome;//需求额度=个人未来净收入的年金现值/元；
                }

                return _insureNeedCash1;
            }
            set { _insureNeedCash1 = value; }
        }

        private double _insureNeedCash2;
        /// <summary>
        /// 保险需求额度/元2
        /// </summary>
        public double InsureNeedCash2
        {
            get
            {
                if (RelativeFinancial2 != 0)
                {
                    _insureNeedCash2 =RelativeFinancial2;
                }

                return _insureNeedCash2;
            }
            set { _insureNeedCash2 = value; }
        }


        private decimal _gapCash1;
        /// <summary>
        /// 缺口额度/元1
        /// </summary>
        public decimal GapCash1
        {
            get
            {
                if (InsureNeedCash1 != 0)
                {
                    if (MethodTypeId == 1)
                    {
                        // 缺口额度=保险需求额度-已有额度
                        _gapCash1 = InsureNeedCash1 - InsuranceAmount1;
                    }
                    else
                    {
                        _gapCash1 = FutureAnnuityIncome - InsuranceAmount1;
                    }
                }

                return _gapCash1;
            }
            set { _gapCash1 = value; }
        }

        private double _gapCash2;
        /// <summary>
        /// 缺口额度/元2
        /// </summary>
        public double GapCash2
        {
            get
            {
                if (InsureNeedCash2 != 0)
                {
                    _gapCash2 = InsureNeedCash2 -Convert.ToDouble(InsuranceAmount2);
                }


                return _gapCash2;
            }
            set { _gapCash2 = value; }
        }


        private decimal _balanceCash1;
        /// <summary>
        /// 欠缺额度/元1
        /// </summary>
        public decimal BalanceCash1
        {
            get
            {
                if (GapCash1 != 0)
                {
                    //欠缺额度= 缺口额度-预算额度-补充额度 
                    _balanceCash1 = GapCash1 - BudgetAmount1 - SupplementaryQuota1;
                }

                return _balanceCash1;
            }
            set { _balanceCash1 = value; }
        }


        private double _balanceCash2;
        /// <summary>
        /// 欠缺额度/元2 
        /// </summary>
        public double BalanceCash2
        {
            get
            {
                if (GapCash2 != 0)
                {
                    //欠缺额度= 缺口额度-预算额度-补充额度 
                    _balanceCash2 = GapCash2 -Convert.ToDouble(BudgetAmount2) -Convert.ToDouble(SupplementaryQuota2);
                }

                return _balanceCash2;
            }
            set { _balanceCash2 = value; }
        }


        #endregion


        #region 显示字段---生命法则算法

        private int _predictRetirementAgeLIfe;
        /// <summary>
        /// 离退休年数/年
        /// </summary>
        public int PredictRetirementAgeLIfe
        {
            //预计退休年龄-当前年龄

            get {
                if (RetirementAge1 != 0)
                {
                    _predictRetirementAgeLIfe = RetirementAge1 - Age;
                }
                return _predictRetirementAgeLIfe; }
            set { _predictRetirementAgeLIfe = value; }
        }


        private decimal _futureIncomeLife;
        /// <summary>
        /// 未来工作期间收入现值/元
        /// </summary>
        public decimal FutureIncomeLife
        {
            //未来工作期间收入现值=PV( (1+投资报酬率)/(1+收入增长率)－1，离退休年数，- 当前个人年收入) 
            get {
                if (RevenueGrowth1 != 0)
                {
                    double rate = Convert.ToDouble(((((ReturnOnInvestment1 / 100 + 1) / (RevenueGrowth1 / 100 + 1)) - 1) * 100).ToString("0.00"));
                    double nper =Convert.ToDouble(PredictRetirementAgeLIfe);
                    double pmt = -Convert.ToDouble(Income1);
                  _futureIncomeLife= Convert.ToDecimal(Microsoft.VisualBasic.Financial.PV(rate/100, nper, pmt));
                }
                
                return _futureIncomeLife; }
            set { _futureIncomeLife = value; }
        }

     

        private decimal _futureExpend;
         /// <summary>
        /// 未来工作期间支出现值/元
         /// </summary>
        public decimal FutureExpend
        {
            ////未来工作期间支出现值=PV( ((1+投资报酬率)/(1+年通货膨胀率))－1,离退休年数，- 当前个人年支出)
            get {
                if (InflationRate1 != 0)
                {
                    double rate = Convert.ToDouble(((((ReturnOnInvestment1/100 + 1) / (InflationRate1/100 + 1)) - 1)*100).ToString("0.00"));
                    double nper = Convert.ToDouble(PredictRetirementAgeLIfe);
                    double pmt = -Convert.ToDouble(Expenditure);
                    _futureExpend = Convert.ToDecimal(Microsoft.VisualBasic.Financial.PV(rate/100, nper, pmt));
                }
                
                return _futureExpend; }
            set { _futureExpend = value; }
        }

        //个人未来净收入的年金现值/元
        private decimal _futureAnnuityIncome;
        /// <summary>
        /// 个人未来净收入的年金现值/元 =未来工作期间收入现值-未来工作期间支出现值
        /// </summary>
        public decimal FutureAnnuityIncome
        {
            get {
                if (RevenueGrowth1 != 0 && InflationRate1 != 0)
                {
                    _futureAnnuityIncome = FutureIncomeLife - FutureExpend;
                }
                
                return _futureAnnuityIncome; }
            set { _futureAnnuityIncome = value; }
        }

        /// <summary>
        /// 获取保险规划【预算金额】
        /// </summary>
        public decimal BudgetAmount
        {
            get
            {
                return BudgetAmount1 + BudgetAmount2;
            }
        }

        #endregion





    }
}