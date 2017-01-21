using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using VM;

namespace Web.Controllers
{
    public class InvestmentPlanController : Controller
    {
        //
        // GET: /CompetitionUser/InvestmentPlan/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 修改跟新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveInvestmentPlan(InvestmentPlanVM model)
        {
            //构建缓存类
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
            //string errCode = "";
            bool result = false;
            if (proposal != null)
            {
                //修改
                if (model.Id > 0)
                {
                    result = SvrFactory.Instance.TrainingSvr.UpdateInvestmentPlan(model);
                    if (result)
                    {
                        //更新缓存
                        proposal.InvestmentPlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007", null));//20007 修改失败!请联系管理员!
                    }


                }
                else
                {
                    //添加
                    int id = SvrFactory.Instance.TrainingSvr.AddInvestmentPlan(model);
                    if (id != 0)
                    {
                        model.Id = id;
                        //更新缓存
                        proposal.InvestmentPlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20006", null));//20006 新增失败!请联系管理员!
                    }
                }
            }
            else
            {
                return Json(new JsonModel(true, ""));
            }
        }


        /// <summary>
        /// 获取建议书
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult LoadInvestmentPlan(int ProposalId)
        {
            //  InvestmentPlanVM InvestmentPlanModel = SvrFactory.Instance.TrainingSvr.GetInvestmentPlanByProposalId(ProposalId);
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            InvestmentPlanVM InvestmentPlanModel = new InvestmentPlanVM();
            if (proposal != null)
            {
                InvestmentPlanModel = proposal.InvestmentPlanVM;
            }

            if (InvestmentPlanModel != null)
            {
                if (InvestmentPlanModel.InvestmentPlanProductList != null)
                {
                    InvestmentPlanModel.InvestmentPlanProductList.ForEach(
                        x =>
                        {
                            if (x.DemandDepositsBank != 0)
                            {
                                x.BankView = TrainingCaches.BankDepositsList.Where(y => y.Id == x.DemandDepositsBank).FirstOrDefault().BankName;
                            }
                            else
                            {
                                x.BankView = "";
                            }
                            if (x.TimeDepositBank != 0)
                            {
                                x.BankTimeView = TrainingCaches.BankDepositsList.Where(y => y.Id == x.TimeDepositBank).FirstOrDefault().BankName;
                            }
                            else
                            {
                                x.BankView = "";
                            }

                        });
                }

                return Json(new JsonModel(true, "", InvestmentPlanModel));
            }
            else
            {
                return Json(new JsonModel(true, "", null));//20008 加载失败!请联系管理员!
            }

        }

        /// <summary>
        /// 获取规划值--且显示值
        /// </summary>
        /// <param name="PlanType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RequestInsetmentVal(int PlanType, int ProposalId)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            decimal InsetemntMoney = 0;
            if (proposal != null)
            {
                if (PlanType == (int)LifePlanType.LifeEducationPlan)
                {
                    LifeEducationPlanVM lifeModelvm = proposal.LifeEducationPlanVM;
                    if (lifeModelvm != null)
                    {
                        InsetemntMoney = lifeModelvm.ReturnOnInvestment; //应该是收益率。不是钱
                    }
                    else
                    {
                        InsetemntMoney = 0;
                    }
                }
                else if (PlanType == (int)LifePlanType.ConsumptionPlan)
                {
                    ConsumptionPlanVM ConModelvm = proposal.ConsumptionPlanVM;
                    if (ConModelvm != null)
                    {
                        InsetemntMoney = ConModelvm.ReturnOnInvestment;
                    }
                    else
                    {
                        InsetemntMoney = 0;
                    }
                }

                else if (PlanType == (int)LifePlanType.StartAnUndertakingPlan)
                {
                    StartAnUndertakingPlanVM starModelvm = proposal.StartAnUndertakingPlanVM;
                    if (starModelvm != null)
                    {
                        InsetemntMoney = starModelvm.ReturnOnInvestmentRate;
                    }
                    else
                    {
                        InsetemntMoney = 0;
                    }
                }

                else if (PlanType == (int)LifePlanType.RetirementPlan)
                {
                    RetirementPlanVM reModelvm = proposal.RetirementPlanVM;
                    if (reModelvm != null)
                    {
                        InsetemntMoney = reModelvm.ReturnOnInvestmentRate;
                    }
                    else
                    {
                        InsetemntMoney = 0;
                    }
                }

                return Json(new JsonModel(true, "", new { TargetAmount = InsetemntMoney }));
            }
            return Json(new JsonModel(true, "", null));//数据加载失败没有建议书ID
        }


        /// <summary>
        /// 计算银行存款汇率
        /// </summary>
        /// <param name="BankType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBankDepositsList(int BankType)
        {
                BankDepositsVM model = TrainingCaches.BankDepositsList.Where(x => x.Id == BankType).FirstOrDefault();
                if (model != null)
                {
                    return Json(new JsonModel(true, "", model));
                }
          
        
            return Json(new JsonModel(true, "", null));//数据加载错误
            
        }

    }
}