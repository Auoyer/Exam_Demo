using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;
using VM;

namespace Web
{
    /// <summary>
    /// 考核算分
    /// </summary>
    public class AssessmentCalculationBLL
    {
        /// <summary>
        /// 得分计算
        /// </summary>
        /// <param name="ExamDetail"></param>
        /// <param name="Proposal"></param>
        public AssessmentResultsDetailVM AssessmentCalculation(TrainExamDetailVM ExamDetail, ProposalVM Proposal, List<ExamModuleVM> EMList, List<ExamPointVM> EPList)
        {
            AssessmentResultsDetailVM model = new AssessmentResultsDetailVM();
            model.ExamPointType = (int)ExamPointType.Objective;
            model.Score = 0;//默认
            model.Status = (int)IsCorrect.Error;
            model.AssessmentPoint = ExamDetail.ExamPointId;//考核点ID
            int ExamModuleId = EPList.Where(x => x.Id == ExamDetail.ExamPointId).FirstOrDefault().ExamModuleId;
            model.ModularId = EMList.Where(x => x.Id == ExamModuleId).FirstOrDefault().ExamContentId;  //考核模块名称ID                
            switch (ExamDetail.ExamPointId)
            {
                case 1:
                    //客户信息-姓名
                    {
                        if (Proposal.ProposalCustomerVM != null && Proposal.ProposalCustomerVM.CustomerName == ExamDetail.Answer)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 2:
                    {
                        int Age = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out Age);
                        //客户信息-年龄
                        if (Result && Proposal.ProposalCustomerVM != null && Proposal.ProposalCustomerVM.Age == Age)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 3:
                    {
                        //客户信息-证件号码
                        if (Proposal.ProposalCustomerVM != null && Proposal.ProposalCustomerVM.IDNum == ExamDetail.Answer)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 4:
                    {
                        int AgeScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out AgeScore);
                        //客户得分-年龄
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.AgeScore == AgeScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 5:
                    {
                        int JobScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out JobScore);
                        //客户得分-就业状况
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.JobScore == JobScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 6:
                    {
                        int FamilyScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out FamilyScore);
                        //客户得分-家庭负担
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.FamilyScore == FamilyScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 7:
                    {
                        int HouseScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out HouseScore);
                        //客户得分-置产状况
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.HouseScore == HouseScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 8:
                    {
                        int EXPScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out EXPScore);
                        //客户得分-投资经验
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.EXPScore == EXPScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 9:
                    {
                        int KnowledgeScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out KnowledgeScore);
                        //客户得分-投资知识
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.KnowledgeScore == KnowledgeScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 10:
                    {
                        int TolerateScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out TolerateScore);
                        //客户得分-忍受亏损百分比
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.TolerateScore == TolerateScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 11:
                    {
                        int ConsiderationScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out ConsiderationScore);
                        //客户得分-首要考虑
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.ConsiderationScore == ConsiderationScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 12:
                    {
                        int LossScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out LossScore);
                        //客户得分-认赔动作
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.LossScore == Convert.ToInt16(ExamDetail.Answer))
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 13:
                    {
                        int MentalityScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out MentalityScore);
                        //客户得分-赔钱心理
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.MentalityScore == MentalityScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 14:
                    {
                        int CharacterScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out CharacterScore);
                        //客户得分-最重要特性
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.CharacterScore == CharacterScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 15:
                    {
                        int AvoidScore = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out AvoidScore);
                        //客户得分-避免工具
                        if (Result && Proposal.RiskIndexVM != null && Proposal.RiskIndexVM.AvoidScore == AvoidScore)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 16:
                    {
                        decimal Cash = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Cash);
                        //现金
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.Cash == Cash)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 17:
                    {
                        decimal RMBDeposit = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RMBDeposit);
                        //人民币银行活存
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.RMBDeposit == RMBDeposit)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 18:
                    {
                        decimal OtherAsset = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherAsset);
                        //其它流通性资产
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.OtherAsset == OtherAsset)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 19:
                    {
                        decimal RMBFixedDeposit = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RMBFixedDeposit);
                        //其他流动资产
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.RMBFixedDeposit == RMBFixedDeposit)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 20:
                    {
                        decimal ForeignCurrencyFixedDeposit = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ForeignCurrencyFixedDeposit);
                        //人民币银行定存
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.ForeignCurrencyFixedDeposit == ForeignCurrencyFixedDeposit)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 21:
                    {
                        decimal StockInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out StockInvestment);
                        //外币银行定存
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.StockInvestment == StockInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 22:
                    {
                        decimal BondInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out BondInvestment);
                        //股票投资
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.BondInvestment == BondInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 23:
                    {
                        decimal FundInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out FundInvestment);
                        //债券投资
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.FundInvestment == FundInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 24:
                    {
                        decimal IndustryInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out IndustryInvestment);
                        //基金投资
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.IndustryInvestment == IndustryInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 25:
                    {
                        decimal EstateInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EstateInvestment);
                        //实业投资
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.EstateInvestment == EstateInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 26:
                    {
                        decimal PolicyInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out PolicyInvestment);
                        //投资性房地产
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.PolicyInvestment == PolicyInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 27:
                    {
                        decimal OtherInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherInvestment);
                        //保单现金价值
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.OtherInvestment == OtherInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 28:
                    {
                        decimal Estate = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Estate);
                        //自用房地产
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.Estate == Estate)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 29:
                    {
                        decimal Car = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Car);
                        //自用汽车
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.Car == Car)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 30:
                    {
                        decimal Others = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Others);
                        //自用其他资产
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.Others == Others)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 31:
                    {
                        decimal CreditCard = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CreditCard);
                        //信用卡负债
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.CreditCard == CreditCard)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 32:
                    {
                        decimal Microfinance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Microfinance);
                        //小额消费信贷
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.Microfinance == Microfinance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 33:
                    {
                        decimal OtherLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherLoan);
                        //其他消费性负债
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.OtherLoan == OtherLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 34:
                    {
                        decimal FinancialLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out FinancialLoan);
                        //金融投资借款
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.FinancialLoan == FinancialLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 35:
                    {
                        decimal IndustryInvestmentLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out IndustryInvestmentLoan);
                        //实业投资借款
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.IndustryInvestmentLoan == IndustryInvestmentLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 36:
                    {
                        decimal EstateInvestmentLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EstateInvestmentLoan);
                        //投资性房地产按揭贷款
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.EstateInvestmentLoan == EstateInvestmentLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 37:
                    {
                        decimal OtherInvestmentLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherInvestmentLoan);
                        //其他投资性负债
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.OtherInvestmentLoan == OtherInvestmentLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 38:
                    {
                        decimal EstateLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EstateLoan);
                        //自用房地产贷款
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.EstateLoan == EstateLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 39:
                    {
                        decimal CarLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CarLoan);
                        //自用汽车贷款
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.CarLoan == CarLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 40:
                    {
                        decimal OthersLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OthersLoan);
                        //其他自用贷款
                        if (Result && Proposal.LiabilityVM != null && Proposal.LiabilityVM.OthersLoan == OthersLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 41:
                    {
                        decimal JobIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out JobIncome);
                        //薪资收入
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.JobIncome == JobIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 42:
                    {
                        decimal EndowmentInsurance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EndowmentInsurance);
                        //养老保险储蓄
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.EndowmentInsurance == EndowmentInsurance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 43:
                    {
                        decimal MedicalInsurance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out MedicalInsurance);
                        //医疗保险储蓄
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.MedicalInsurance == MedicalInsurance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 44:
                    {
                        decimal HousingFund = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out HousingFund);
                        //住房公积金储蓄
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.HousingFund == HousingFund)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 45:
                    {
                        decimal OtherJobIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherJobIncome);
                        //其他工作收入
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.OtherJobIncome == OtherJobIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 46:
                    {
                        decimal FamilyExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out FamilyExpense);
                        //家计支出
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.FamilyExpense == FamilyExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 47:
                    {
                        decimal ChildExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ChildExpense);
                        //子女教育支出
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.ChildExpense == ChildExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 48:
                    {
                        decimal OtherExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherExpense);
                        //其他支出
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.OtherExpense == OtherExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 49:
                    {
                        decimal Interest = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Interest);
                        //利息收入
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.Interest == Interest)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 50:
                    {
                        decimal CapitalGains = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CapitalGains);
                        //资本利得
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.CapitalGains == CapitalGains)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 51:
                    {
                        decimal OtherIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherIncome);
                        //其他理财收入
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.OtherIncome == OtherIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 52:
                    {
                        decimal InterestExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InterestExpense);
                        //利息支出
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.InterestExpense == InterestExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 53:
                    {
                        decimal InsuranceExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InsuranceExpense);
                        //保障型保费支出
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.InsuranceExpense == InsuranceExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 54:
                    {
                        decimal OtherFinanceExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherFinanceExpense);
                        //其他理财支出
                        if (Result && Proposal.IncomeAndExpensesVM != null && Proposal.IncomeAndExpensesVM.OtherFinanceExpense == OtherFinanceExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 55:
                    {
                        decimal Redemption = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Redemption);
                        //投资现金流量-投资赎回
                        if (Result && Proposal.CashFlowVM != null && Proposal.CashFlowVM.Redemption == Redemption)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 56:
                    {
                        decimal Investment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Investment);
                        //投资现金流量-新增投资
                        if (Result && Proposal.CashFlowVM != null && Proposal.CashFlowVM.Investment == Investment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 57:
                    {
                        decimal BorrowCapital = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out BorrowCapital);
                        //借贷现金流量-借入本金
                        if (Result && Proposal.CashFlowVM != null && Proposal.CashFlowVM.BorrowCapital == BorrowCapital)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 58:
                    {
                        decimal RepaymentCapital = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RepaymentCapital);
                        //借贷现金流量-还款本金
                        if (Result && Proposal.CashFlowVM != null && Proposal.CashFlowVM.RepaymentCapital == RepaymentCapital)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 59:
                    //客户财务情况分析--主观题
                    return null;
                case 60:
                    {
                        decimal FamilyMonthExpense = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out FamilyMonthExpense);
                        //家庭月支出
                        if (Result && Proposal.CashPlanVM != null && Proposal.CashPlanVM.FamilyMonthExpense == FamilyMonthExpense)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 61:
                    return null;
                case 62:
                    {
                        decimal ChildAge = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ChildAge);
                        //子女年龄
                        if (Result && Proposal.LifeEducationPlanVM != null && Proposal.LifeEducationPlanVM.ChildAge == ChildAge)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 63:
                    {
                        decimal InlandEduFee = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InlandEduFee);
                        //国内学费增长率
                        if (Result && Proposal.LifeEducationPlanVM != null && Proposal.LifeEducationPlanVM.InlandEduFee == InlandEduFee)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 64:
                    {
                        decimal ForeignEduFee = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ForeignEduFee);
                        //国外学费增长率
                        if (Result && Proposal.LifeEducationPlanVM != null && Proposal.LifeEducationPlanVM.ForeignEduFee == ForeignEduFee)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 65:
                    #region 教育规划-幼儿园教育求学年龄
                    {
                        //幼儿园教育求学年龄
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal EduAge = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out EduAge);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.NurseryschoolEdu && x.EduAge == EduAge);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 66:
                    #region 教育规划-幼儿园教育求学时间
                    {
                        //幼儿园教育求学时间
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal EduTime = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out EduTime);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.NurseryschoolEdu && x.EduTime == EduTime);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 67:
                    #region 教育规划-幼儿园教育目前学费
                    {
                        //幼儿园教育目前学费
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal Tuition = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out Tuition);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.NurseryschoolEdu && x.Tuition == Tuition);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 68:
                    #region 教育规划-小学教育求学年龄
                    {
                        //小学教育求学年龄
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduAge = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduAge);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.PrimaryschoolEdu && x.EduAge == EduAge);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 69:
                    #region 教育规划-小学教育求学年龄求学时间
                    {
                        //求学年龄求学时间
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduTime = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduTime);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.PrimaryschoolEdu && x.EduTime == EduTime);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 70:
                    #region 教育规划-小学教育求学年龄目前学费
                    {
                        //求学年龄目前学费
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal Tuition = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out Tuition);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.PrimaryschoolEdu && x.Tuition == Tuition);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 71:
                    #region 教育规划-初中教育求学年龄
                    {
                        //初中教育求学年龄
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduAge = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduAge);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.JuniormiddleschoolEdu && x.EduAge == EduAge);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 72:
                    #region 教育规划-初中教育求学时间
                    {
                        //初中教育求学时间
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduTime = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduTime);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.JuniormiddleschoolEdu && x.EduTime == EduTime);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 73:
                    #region 教育规划-初中教育目前学费
                    {
                        //初中教育目前学费
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal Tuition = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out Tuition);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.JuniormiddleschoolEdu && x.Tuition == Tuition);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 74:
                    #region 教育规划-高中教育求学年龄
                    {
                        //高中教育求学年龄
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduAge = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduAge);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.SeniormiddleschoolEdu && x.EduAge == EduAge);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 75:
                    #region 教育规划-高中教育求学时间
                    {
                        //高中教育求学时间
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduTime = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduTime);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.SeniormiddleschoolEdu && x.EduTime == EduTime);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 76:
                    #region 教育规划-高中教育目前学费
                    {
                        //高中教育目前学费
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal Tuition = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out Tuition);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.SeniormiddleschoolEdu && x.Tuition == Tuition);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 77:
                    #region 教育规划-大学教育求学年龄
                    {
                        //大学教育求学年龄
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduAge = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduAge);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.UniversityEdu && x.EduAge == EduAge);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 78:
                    #region 教育规划-大学教育求学时间
                    {
                        //大学教育求学时间
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduTime = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduTime);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.UniversityEdu && x.EduTime == EduTime);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 79:
                    #region 教育规划-大学教育目前学费
                    {
                        //大学教育目前学费
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal Tuition = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out Tuition);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.UniversityEdu && x.Tuition == Tuition);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 80:
                    #region 教育规划-留学教育求学年龄
                    {
                        //留学教育求学年龄
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduAge = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduAge);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.StudyabroadEdu && x.EduAge == EduAge);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 81:
                    #region 教育规划-留学教育求学时间
                    {
                        //留学教育求学时间
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                int EduTime = 0;
                                bool Result = int.TryParse(ExamDetail.Answer, out EduTime);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.StudyabroadEdu && x.EduTime == EduTime);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 82:
                    #region 教育规划-留学教育目前学费
                    {
                        //留学教育目前学费
                        if (Proposal.LifeEducationPlanVM != null)
                        {
                            if (Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList != null && Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count > 0)
                            {
                                decimal Tuition = 0;
                                bool Result = decimal.TryParse(ExamDetail.Answer, out Tuition);
                                if (Result)
                                {
                                    int Count = Proposal.LifeEducationPlanVM.LifeEducationPlanDetailList.Count(x => x.EduStage == (int)EducationStage.StudyabroadEdu && x.Tuition == Tuition);
                                    if (Count > 0)
                                    {
                                        model.Score = ExamDetail.Score;
                                        model.Status = (int)IsCorrect.Correct;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 83:
                    {
                        decimal Insurance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Insurance);
                        //已经准备的教育费用商业保险
                        if (Result && Proposal.LifeEducationPlanVM != null && Proposal.LifeEducationPlanVM.Insurance == Insurance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 84:
                    {
                        decimal Deposit = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Deposit);
                        //已经准备的教育费用储蓄计划
                        if (Result && Proposal.LifeEducationPlanVM != null && Proposal.LifeEducationPlanVM.Deposit == Deposit)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 85:
                    {
                        decimal Other = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Other);
                        //已经准备的教育费用其他安排
                        if (Result && Proposal.LifeEducationPlanVM != null && Proposal.LifeEducationPlanVM.Other == Other)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 86:
                    //理财方案--主观题
                    return null;
                case 87:
                    //教育规划分析--主观题
                    return null;
                case 88:
                    {
                        decimal ShopHouseYear = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ShopHouseYear);
                        //预计购房年限
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.ShopHouseYear == ShopHouseYear)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 89:
                    #region 消费规划-购房总金额
                    {
                        decimal HouseTotalAmount = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out HouseTotalAmount);
                        //消费目标-购房  总金额
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.HouseAllMoney == HouseTotalAmount)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    #endregion
                    break;
                case 90:
                    {
                        decimal HouseDownPaymentPercent = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out HouseDownPaymentPercent);
                        //首付比例
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.HouseDownPaymentPercent == HouseDownPaymentPercent)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 91:
                    {
                        decimal HouseLoanYear = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out HouseLoanYear);
                        //贷款年限
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.HouseLoanYear == HouseLoanYear)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 92:
                    {
                        decimal HouseLoanRate = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out HouseLoanRate);
                        //贷款利率
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.HouseLoanRate == HouseLoanRate)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 93:
                    {
                        decimal ShopCarYear = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ShopCarYear);
                        //购车年限
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.ShopCarYear == ShopCarYear)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 94:
                    {
                        decimal CarPrice = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CarPrice);
                        //预计购车年限
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.CarPrice == CarPrice)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 95:
                    {
                        decimal CarDownPaymentPercent = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CarDownPaymentPercent);
                        //首付比例
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.CarDownPaymentPercent == CarDownPaymentPercent)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 96:
                    {
                        decimal CarLoanYear = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CarLoanYear);
                        //贷款期限
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.CarLoanYear == CarLoanYear)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 97:
                    {
                        decimal CarLoanRate = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CarLoanRate);
                        //贷款利率
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.CarLoanRate == CarLoanRate)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 98:
                    {
                        decimal CarRegFee = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CarRegFee);
                        //上牌费用
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.CarRegFee == CarRegFee)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 99:
                    {
                        decimal VehicleAndVesselTax = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out VehicleAndVesselTax);
                        //车船使用税
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.VehicleAndVesselTax == VehicleAndVesselTax)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 100:
                    {
                        decimal MotorVehicleCommercial = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out MotorVehicleCommercial);
                        //商业保险
                        if (Result && Proposal.ConsumptionPlanVM != null && Proposal.ConsumptionPlanVM.MotorVehicleCommercial == MotorVehicleCommercial)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 101:
                    //理财方案
                    return null;
                case 102:
                    //消费规划分析
                    return null;
                case 103:
                    {
                        decimal StartPlanAge = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out StartPlanAge);
                        //计划创业年龄
                        if (Result && Proposal.StartAnUndertakingPlanVM != null && Proposal.StartAnUndertakingPlanVM.StartPlanAge == StartPlanAge)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 104:
                    {
                        decimal CostInput = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CostInput);
                        //创业时一次性投入
                        if (Result && Proposal.StartAnUndertakingPlanVM != null && Proposal.StartAnUndertakingPlanVM.CostInput == CostInput)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 105:
                    //理财方案
                    return null;
                case 106:
                    //创业规划分析
                    return null;
                case 107:
                    {
                        decimal BeforeInflationRate = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out BeforeInflationRate);
                        //退休前通货膨胀率
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.BeforeInflationRate == BeforeInflationRate)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 108:
                    {
                        decimal AfterInflationRate = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out AfterInflationRate);
                        //退休后通货膨胀率
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.AfterInflationRate == AfterInflationRate)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 109:
                    {
                        decimal RetirementRate = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RetirementRate);
                        //退休后投资收益率
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.RetirementRate == RetirementRate)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 110:
                    {
                        decimal RetirementAge = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RetirementAge);
                        //计划退休年龄
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.RetirementAge == RetirementAge)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 111:
                    {
                        decimal RetirementYears = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RetirementYears);
                        //希望享有的退休生活年限
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.RetirementYears == RetirementYears)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 112:
                    {
                        decimal LivingStandardNow = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out LivingStandardNow);
                        //目前生活水平
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.LivingStandardNow == LivingStandardNow)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 113:
                    #region 退休规划-生活满意度
                    {
                        decimal SatisfactionLivingStandard = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out SatisfactionLivingStandard);
                        //生活满意度
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.Satisfaction == SatisfactionLivingStandard)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    #endregion
                    break;
                case 114:
                    {
                        decimal ConvertProportion = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ConvertProportion);
                        //退休后、退休前生活水平折算比例
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.ConvertProportion == ConvertProportion)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 115:
                    {
                        decimal lineageFee = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out lineageFee);
                        //子女传承费用
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.lineageFee == lineageFee)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 116:
                    {
                        decimal SocialInsurance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out SocialInsurance);
                        //社会保险
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.SocialInsurance == SocialInsurance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 117:
                    {
                        decimal CommercialInsurance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out CommercialInsurance);
                        //商业保险
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.CommercialInsurance == CommercialInsurance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 118:
                    {
                        decimal RentIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RentIncome);
                        //租金收入
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.RentIncome == RentIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 119:
                    {
                        decimal OtherIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherIncome);
                        //其他收入
                        if (Result && Proposal.RetirementPlanVM != null && Proposal.RetirementPlanVM.OtherIncome == OtherIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 120:
                    //理财方案
                    return null;
                case 121:
                    //退休规划分析
                    return null;
                case 122:
                    {
                        //弥补遗属需求的寿险需求-C
                        if (Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.SpouseName == ExamDetail.Answer)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 123:
                    {
                        int Age2 = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out Age2);
                        //被保险人年龄-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.Age2 == Age2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 124:
                    {
                        int RetirementAge1 = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out RetirementAge1);
                        //预计退休年龄-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.RetirementAge1 == RetirementAge1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 125:
                    {
                        int RetirementAge2 = 0;
                        bool Result = int.TryParse(ExamDetail.Answer, out RetirementAge2);
                        //预计退休年龄-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.RetirementAge2 == RetirementAge2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 126:
                    {
                        decimal ReturnOnInvestment1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ReturnOnInvestment1);
                        //投资报酬率-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.ReturnOnInvestment1 == ReturnOnInvestment1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 127:
                    {
                        decimal ReturnOnInvestment2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ReturnOnInvestment2);
                        //投资报酬率-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.ReturnOnInvestment2 == ReturnOnInvestment2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 128:
                    {
                        decimal InflationRate1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InflationRate1);
                        //通货膨胀率-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.InflationRate1 == InflationRate1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 129:
                    {
                        decimal InflationRate2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InflationRate2);
                        //通货膨胀率-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.InflationRate2 == InflationRate2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 130:
                    {
                        decimal RevenueGrowth1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RevenueGrowth1);
                        //收入增长率-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.RevenueGrowth1 == RevenueGrowth1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 131:
                    {
                        decimal RevenueGrowth2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RevenueGrowth2);
                        //收入增长率-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.RevenueGrowth2 == RevenueGrowth2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 132:
                    {
                        decimal MatrimonialFee1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out MatrimonialFee1);
                        //当前的家庭生活费用-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.MatrimonialFee1 == MatrimonialFee1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 133:
                    {
                        decimal MatrimonialFee2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out MatrimonialFee2);
                        //当前的家庭生活费用-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.MatrimonialFee2 == MatrimonialFee2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 134:
                    {

                        decimal AfterAccidentRate1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out AfterAccidentRate1);
                        //保险事故发生后支出调整率-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.AfterAccidentRate1 == AfterAccidentRate1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 135:
                    {
                        decimal AfterAccidentRate2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out AfterAccidentRate2);
                        //保险事故发生后支出调整率-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.AfterAccidentRate2 == AfterAccidentRate2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 136:
                    {
                        decimal Income1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Income1);
                        //配偶的个人年收入-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.Income1 == Income1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 137:
                    {
                        decimal Income2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Income2);
                        //配偶的个人年收入-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.Income2 == Income2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 138:
                    {
                        decimal DeathExpense1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out DeathExpense1);
                        //临终及丧葬支出现值-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.DeathExpense1 == DeathExpense1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 139:
                    {
                        decimal DeathExpense2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out DeathExpense2);
                        //临终及丧葬支出现值-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.DeathExpense2 == DeathExpense2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 140:
                    {
                        decimal LoanBalance1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out LoanBalance1);
                        //目前贷款余额-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.LoanBalance1 == LoanBalance1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 141:
                    {
                        decimal LoanBalance2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out LoanBalance2);
                        //目前贷款余额-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.LoanBalance2 == LoanBalance2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 142:
                    {
                        decimal EarningAssets1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EarningAssets1);
                        //家庭生息资产-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.EarningAssets1 == EarningAssets1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 143:
                    {
                        decimal EarningAssets2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EarningAssets2);
                        //家庭生息资产-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.EarningAssets2 == EarningAssets2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 144:
                    {
                        decimal InsuranceAmount1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InsuranceAmount1);
                        //已有额度-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.InsuranceAmount1 == InsuranceAmount1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 145:
                    {
                        decimal InsuranceAmount2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InsuranceAmount2);
                        //已有额度-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.InsuranceAmount2 == InsuranceAmount2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 146:
                    {
                        decimal BudgetAmount1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out BudgetAmount1);
                        //预算金额-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.BudgetAmount1 == BudgetAmount1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 147:
                    {
                        decimal BudgetAmount2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out BudgetAmount2);
                        //预算金额-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.BudgetAmount2 == BudgetAmount2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 148:
                    {
                        decimal SupplementaryQuota1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out SupplementaryQuota1);
                        //补充额度-B
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.SupplementaryQuota1 == SupplementaryQuota1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 149:
                    {
                        decimal SupplementaryQuota2 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out SupplementaryQuota2);
                        //补充额度-C
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.SupplementaryQuota2 == SupplementaryQuota2)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 150:
                    //保险规划分析
                    return null;
                case 151:
                    {
                        decimal RetirementAge1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RetirementAge1);
                        //预计退休年龄
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.RetirementAge1 == RetirementAge1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 152:
                    {
                        decimal ReturnOnInvestment1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ReturnOnInvestment1);
                        //投资报酬率
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.ReturnOnInvestment1 == ReturnOnInvestment1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 153:
                    {
                        decimal Income1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Income1);
                        //当前个人年收入
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.Income1 == Income1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 154:
                    {
                        decimal RevenueGrowth1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out RevenueGrowth1);
                        //收入增长率
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.RevenueGrowth1 == RevenueGrowth1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 155:
                    {
                        decimal Expenditure = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Expenditure);
                        //当前个人年支出
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.Expenditure == Expenditure)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 156:
                    {
                        decimal InflationRate1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InflationRate1);
                        //年通货膨胀率
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.InflationRate1 == InflationRate1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 157:
                    {
                        decimal InsuranceAmount1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out InsuranceAmount1);
                        //已有额度
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.InsuranceAmount1 == InsuranceAmount1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 158:
                    {
                        decimal BudgetAmount1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out BudgetAmount1);
                        //预算金额
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.BudgetAmount1 == BudgetAmount1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 159:
                    {
                        decimal SupplementaryQuota1 = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out SupplementaryQuota1);
                        //补充额度
                        if (Result && Proposal.InsurancePlanVM != null && Proposal.InsurancePlanVM.SupplementaryQuota1 == SupplementaryQuota1)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 160:
                    //保险规划分析
                    return null;
                case 161:
                    {
                        //ExamDetail.Answer
                        //当前客户所处家庭生命周期
                        if (Proposal.InvestmentPlanVM != null)
                        {
                            List<EnumKeyValue> List = EnumHelper.GetEnumList<LifeCycleType>();
                            var value = List.FirstOrDefault(x => x.Value.Contains(ExamDetail.Answer));
                            if (value != null)
                            {
                                model.Score = ExamDetail.Score;
                                model.Status = (int)IsCorrect.Correct;
                            }
                        }
                    }
                    break;
                case 162:
                    //产品选择
                    return null;
                case 163:
                    //投资规划分析
                    return null;
                case 164:
                    {
                        decimal Salary = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Salary);
                        //工资、薪金所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.Salary == Salary)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 165:
                    {
                        decimal OperatingRevenue = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OperatingRevenue);
                        //个体工商户的生产、经营所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.OperatingRevenue == OperatingRevenue)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 166:
                    {
                        decimal EnterprisesRevenue = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out EnterprisesRevenue);
                        //对企事业单位承包、承租经营所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.EnterprisesRevenue == EnterprisesRevenue)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 167:
                    {
                        decimal ServiceIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ServiceIncome);
                        //劳务报酬所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.ServiceIncome == ServiceIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 168:
                    {
                        decimal Remuneration = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Remuneration);
                        //稿酬所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.Remuneration == Remuneration)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 169:
                    {
                        decimal Loyalities = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Loyalities);
                        //特许权使用费所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.Loyalities == Loyalities)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 170:
                    {
                        decimal Demise = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Demise);
                        //财产转让所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.Demise == Demise)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 171:
                    {
                        decimal IncidentalIncome = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out IncidentalIncome);
                        //偶然所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.IncidentalIncome == IncidentalIncome)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 172:
                    {
                        decimal Interest = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Interest);
                        //利息、红利、股利所得
                        if (Result && Proposal.TaxPlanVM != null && Proposal.TaxPlanVM.Interest == Interest)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 173:
                    //税收筹划分析
                    return null;
                case 174:
                    //婚姻、财产状况分析
                    return null;
                case 175:
                    //财产分配规划分析
                    return null;
                case 176:
                    {
                        decimal Cash = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Cash);
                        //现金
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Cash == Cash)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 177:
                    {
                        decimal Deposit = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Deposit);
                        //银行存款
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Deposit == Deposit)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 178:
                    {
                        decimal LifeInsurance = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out LifeInsurance);
                        //人寿保单赔偿金额
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.LifeInsurance == LifeInsurance)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 179:
                    {
                        decimal OtherCashAccount = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherCashAccount);
                        //其他现金账户
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherCashAccount == OtherCashAccount)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 180:
                    {
                        decimal Stock = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Stock);
                        //股票
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Stock == Stock)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 181:
                    {
                        decimal Bond = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Bond);
                        //债券
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Bond == Bond)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 182:
                    {
                        decimal Fund = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Fund);
                        //基金
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Fund == Fund)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 183:
                    {
                        decimal OtherInvestment = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherInvestment);
                        //其他投资收益
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherInvestment == OtherInvestment)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 184:
                    {
                        decimal Pension = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Pension);
                        //养老金（一次性收入现值）
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Pension == Pension)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 185:
                    {
                        decimal AnnuityRevenue = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out AnnuityRevenue);
                        //配偶/遗孤年金收益（现值）
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.AnnuityRevenue == AnnuityRevenue)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 186:
                    {
                        decimal OtherPension = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherPension);
                        //其他退休基金
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherPension == OtherPension)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 187:
                    {
                        decimal House = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out House);
                        //房产
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.House == House)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 188:
                    {
                        decimal Car = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Car);
                        //汽车
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Car == Car)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 189:
                    {
                        decimal Other = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out Other);
                        //其他个人资产
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.Other == Other)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 190:
                    {
                        decimal OtherProperty = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherProperty);
                        //其他资产
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherProperty == OtherProperty)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 191:
                    {
                        decimal ShortTermLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out ShortTermLoan);
                        //短期贷款
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.ShortTermLoan == ShortTermLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 192:
                    {
                        decimal MediumTermLoans = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out MediumTermLoans);
                        //中期贷款
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.MediumTermLoans == MediumTermLoans)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 193:
                    {
                        decimal LongTermLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out LongTermLoan);
                        //长期贷款
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.LongTermLoan == LongTermLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 194:
                    {
                        decimal OtherLoan = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherLoan);
                        //其他贷款
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherLoan == OtherLoan)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 195:
                    {
                        decimal MedicalCosts = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out MedicalCosts);
                        //临终医疗费用
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.MedicalCosts == MedicalCosts)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 196:
                    {
                        decimal TaxCosts = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out TaxCosts);
                        //预期收入纳税额支出
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.TaxCosts == TaxCosts)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 197:
                    {
                        decimal FuneralExpenses = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out FuneralExpenses);
                        //丧葬费用
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.FuneralExpenses == FuneralExpenses)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 198:
                    {
                        decimal HeritageCosts = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out HeritageCosts);
                        //遗产处置费用
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.HeritageCosts == HeritageCosts)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 199:
                    {
                        decimal OtherCosts = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherCosts);
                        //其他费用
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherCosts == OtherCosts)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 200:
                    {
                        decimal OtherLiabilities = 0;
                        bool Result = decimal.TryParse(ExamDetail.Answer, out OtherLiabilities);
                        //其他负债
                        if (Result && Proposal.HeritageVM != null && Proposal.HeritageVM.OtherLiabilities == OtherLiabilities)
                        {
                            model.Score = ExamDetail.Score;
                            model.Status = (int)IsCorrect.Correct;
                        }
                    }
                    break;
                case 201:
                    //财务分析
                    return null;
                case 202:
                    //财产传承规划分析
                    return null;
                default:
                    return null;
            }
            return model;
        }
    }
}