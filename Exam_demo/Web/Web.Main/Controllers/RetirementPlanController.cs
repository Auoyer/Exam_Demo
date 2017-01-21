using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    public class RetirementPlanController : Controller
    {
        //生涯规划--------退休规划
        // GET: /CompetitionUser/RetirementPlan/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据建议书ID获取
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRetirementPlanByProposalId(int ProposalId)
        {
            #region 数据库里面取查询
            //RetirementPlanVM model = SvrFactory.Instance.TrainingSvr.GetRetirementPlanByProposalId(ProposalId);
            //List<ProposalCustomerVM> ProposalCustomerList = new List<ProposalCustomerVM>(); 
            //IncomeAndExpensesVM incomeModel = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpensesByProposalId(ProposalId);
            // LifeEducationPlanVM AccordingModel = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectEPGetObj(ProposalId);
            // ConsumptionPlanVM ConsumptionPlan = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectCPlanGetObj(ProposalId);
            // StartAnUndertakingPlanVM SelectSUPGetObjModel = SvrFactory.Instance.TrainingSvr.GetModelProposalId(ProposalId);
            // CashPlanVM CashPlanModel = SvrFactory.Instance.TrainingSvr.GetCashPlanByProposalId(ProposalId);
            // InsurancePlanVM InsurancePlanModel = SvrFactory.Instance.TrainingSvr.GetInsurancePlanByProposalId(ProposalId);
            //  LiabilityVM Liability = SvrFactory.Instance.TrainingSvr.GetLiabilityByProposalId(ProposalId);
            #endregion


            //教育规划中【每月定期投资金额】
            decimal disposableInput = 0;
            //消费规划中【每月定期投资金额】
            decimal disposableInput2 = 0;
            //创业规划中【每月定期投资金额】）
            decimal disposableInput3 = 0;
            //现金规划中【现金保留规模】栏数值
            decimal retainCashMultiple = 0;
            //获得保险规划中【预算金额】  InsurancePlan
            decimal budgetAmount = 0;


            //先从缓存里面获取所有的。
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            RetirementPlanVM model = new RetirementPlanVM();
            if (proposal != null)
            {
                #region 每月可支配资金  可用资产
                RetirementPlanVM modelvm = proposal.RetirementPlanVM;
                if (modelvm != null)
                {
                    //退休规划赋值
                    model = modelvm;
                }
                #region model赋值的问题
                ////退休规划
                //RetirementPlanVM RetirementPlanModel = new RetirementPlanVM();
                //RetirementPlanVM RetirementPlanModelvm = proposal.RetirementPlanVM;
                //if (RetirementPlanModelvm != null)
                //{
                //    RetirementPlanModel = RetirementPlanModelvm;
                //}
                //客户信息
                ProposalCustomerVM ProposalCustomerList = new ProposalCustomerVM();
                ProposalCustomerVM ProposalCustomerListvm = proposal.ProposalCustomerVM;
                if (ProposalCustomerListvm != null)
                {
                    ProposalCustomerList = ProposalCustomerListvm;
                }

                //同时还要获取收支储蓄表
                IncomeAndExpensesVM incomeModel = new IncomeAndExpensesVM();
                IncomeAndExpensesVM incomeModelvm = proposal.IncomeAndExpensesVM;
                if (incomeModelvm != null)
                {
                    incomeModel = incomeModelvm;
                }
                //教育规划
                LifeEducationPlanVM AccordingModel = new LifeEducationPlanVM();
                LifeEducationPlanVM AccordingModelvm = proposal.LifeEducationPlanVM;
                if (AccordingModelvm != null)
                {
                    AccordingModel = AccordingModelvm;
                }
                //消费规划中【每月定期投资金额】
                ConsumptionPlanVM ConsumptionPlan = new ConsumptionPlanVM();
                ConsumptionPlanVM ConsumptionPlanvm = proposal.ConsumptionPlanVM;
                if (ConsumptionPlanvm != null)
                {
                    ConsumptionPlan = ConsumptionPlanvm;
                }
                //创业规划中【每月定期投资金额】）
                StartAnUndertakingPlanVM SelectSUPGetObjModel = new StartAnUndertakingPlanVM();
                StartAnUndertakingPlanVM SelectSUPGetObjModelvm = proposal.StartAnUndertakingPlanVM;
                if (SelectSUPGetObjModelvm != null)
                {
                    SelectSUPGetObjModel = SelectSUPGetObjModelvm;
                }
                //现金规划中【现金保留规模】栏数值
                CashPlanVM CashPlanModel = new CashPlanVM();
                CashPlanVM CashPlanModelvm = proposal.CashPlanVM;
                if (CashPlanModelvm != null)
                {
                    CashPlanModel = CashPlanModelvm;
                }
                //资产负债
                LiabilityVM Liability = new LiabilityVM();
                LiabilityVM Liabilityvm = proposal.LiabilityVM;
                if (Liabilityvm != null)
                {
                    Liability = Liabilityvm;
                }
                //保险规划
                InsurancePlanVM InsurancePlanModel = new InsurancePlanVM();
                InsurancePlanVM InsurancePlanModelvm = proposal.InsurancePlanVM;
                if (InsurancePlanModelvm != null)
                {
                    InsurancePlanModel = InsurancePlanModelvm;
                }
                #endregion
                //当前的年龄应该来自客户信息表里面
                //TrainSearch ts = new TrainSearch
                //{
                //    ProposalId = ProposalId
                //};
                //
                //SvrFactory.Instance.TrainingSvr.GetProposalCustomerList(ts);
              
                if (ProposalCustomerList != null)
                {
                    model.Age = ProposalCustomerList.Age;
                }

                //同时还要获取收支储蓄表

                if (AccordingModel != null)
                {
                    disposableInput = AccordingModel.DisposableInput;
                }
                //消费规划中【每月定期投资金额】

                if (ConsumptionPlan != null)
                {
                    disposableInput2 = ConsumptionPlan.DisposableInput;
                }

                //创业规划中【每月定期投资金额】）
                if (SelectSUPGetObjModel != null)
                {
                    disposableInput3 = SelectSUPGetObjModel.DisposableInput;
                }
                if (incomeModelvm != null)
                {
                    //每月可支配金额
                    //model.MonthMoney = (incomeModel.FreeMoney / 12) - AccordingModel.MonthlyInvestment - ConsumptionPlan.MonthlyInvestment - SelectSUPGetObjModel.MonthlyInvestment; 测试说要改 (x/12)-y-z-o 教，消 ，创，退（每月定投金额）
                    model.MonthMoney = (incomeModel.FreeMoney / 12) - AccordingModel.MonthlyInvestment - ConsumptionPlan.MonthlyInvestment - SelectSUPGetObjModel.MonthlyInvestment - model.MonthlyInvestment;
                }
                else
                {
                    model.MonthMoney = 0;
                }

                //现金规划中【现金保留规模】栏数值
                if (CashPlanModel != null)
                {
                    retainCashMultiple = CashPlanModel.RetainCashMultiple;
                }
                //获得保险规划中【预算金额】  InsurancePlan
                if (InsurancePlanModel != null)
                {
                    //  InsurancePlan InsurancePlanModel = new InsurancePlan();
                    budgetAmount = InsurancePlanModel.BudgetAmount1 + InsurancePlanModel.BudgetAmount2;
                }
                //然后给可用资产赋值,先拿到净值资产合计
                if (Liabilityvm != null)
                {
                    model.TotalVal = Liability.TotalVal;
                    model.UserableAsset = Liability.TotalVal - retainCashMultiple - disposableInput - disposableInput2 - disposableInput3 - budgetAmount - model.DisposableInput;
                }
                else
                {
                    model.UserableAsset = 0;
                }

                #endregion

            }
                if (model != null)
                {
                    return Json(new JsonModel(true, "", model));
                }
                else
                {
                    return Json(new JsonModel(true, "", null));
                }
            

        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRetirementPlanById(int id)
        {
            var model = SvrFactory.Instance.TrainingSvr.GetRetirementPlanById(id);
            if (model != null)
            {
                return Json(new JsonModel(true, "", model));

            }
            else
            {
                return Json(new JsonModel(true, "", null));
            }

        }

        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRetirementPlan(RetirementPlanVM model)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);//这个如果获取不到建议书全部要报错
            if (proposal != null)
            {
                if (model.Id > 0)
                {
                    //更新
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateRetirementPlan(model);
                    if (result)
                    {
                        //更新缓存
                        proposal.RetirementPlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010",model));
                     
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007", null));
                    }
                }
                else
                {
                    //新增
                    int id = SvrFactory.Instance.TrainingSvr.AddRetirementPlan(model);
                    if (id != 0)
                    {
                        model.Id = id;
                        //更新缓存
                        proposal.RetirementPlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007", null));
                    }
                }
            }
            else
            {
                return Json(new JsonModel(true, ""));
            }
       
        
        }
    }
}