using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
   
    public class InsurancePlanController : Controller
    {
        //生涯规划------保险规划
        // GET: /CompetitionUser/InsurancePlan/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveInsurancePlan(InsurancePlanVM model)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);//这个如果获取不到建议书全部要报错
            if (proposal != null)
            {
                //修改
                if (model.Id > 0)
                {
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateInsurancePlan(model);
                    if (result)
                    {
                        //更新缓存
                        proposal.InsurancePlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));//20010 保存成功!
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007", null));//20007 修改失败!请联系管理员!
                    }


                }
                else
                {
                    //添加

                    int id = SvrFactory.Instance.TrainingSvr.AddInsurancePlan(model);
                    if (id != 0)
                    {
                        model.Id = id;
                        //更新缓存
                        proposal.InsurancePlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));//20010 保存成功!
                    }
                    else
                    {
                        return Json(new JsonModel(false, "21015", null));//21015 保存失败!请联系管理员!!
                    }
                }
            }
            else
            {
                return Json(new JsonModel(true, "",null));
            }
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult LoadInsurancePlanById(int id)
        {

            InsurancePlanVM model = SvrFactory.Instance.TrainingSvr.GetInsurancePlanById(id);
            if (model != null)
            {
                return Json(new JsonModel(true, "", model));
            }
            else
            {
                return Json(new JsonModel(false, "", null));//20008 加载失败!请联系管理员!
            }
        }

        /// <summary>
        /// 根据建议书ID查询
        /// </summary>
        /// <param name="ProposalId"></param>
        /// <returns></returns>
        public ActionResult LoadInsurancePlanByProposalId(int ProposalId)
        {
            #region 从数据库查
            //RetirementPlanVM RetirementPlanModel = SvrFactory.Instance.TrainingSvr.GetRetirementPlanByProposalId(ProposalId);
            //InsurancePlanVM model = SvrFactory.Instance.TrainingSvr.GetInsurancePlanByProposalId(ProposalId);
            // List<ProposalCustomerVM> ProposalCustomerList = new List<ProposalCustomerVM>(); 
            //SvrFactory.Instance.TrainingSvr.GetProposalCustomerList(ts);
            //IncomeAndExpensesVM incomeModel = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpensesByProposalId(ProposalId);
            // LifeEducationPlanVM AccordingModel = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectEPGetObj(ProposalId);
            //List<LifeEducationPlanDetailVM> LifeEducationPlanDetailList = proposal.LifeEducationPlanDetailVM;
            //ConsumptionPlanVM ConsumptionPlan = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectCPlanGetObj(ProposalId);
            //StartAnUndertakingPlanVM SelectSUPGetObjModel = SvrFactory.Instance.TrainingSvr.GetModelProposalId(ProposalId);
            //   CashPlanVM CashPlanModel = SvrFactory.Instance.TrainingSvr.GetCashPlanByProposalId(ProposalId);
            //  var InsurancePlanModel = SvrFactory.Instance.TrainingSvr.GetInsurancePlanByProposalId(ProposalId);
            //LiabilityVM Liability = SvrFactory.Instance.TrainingSvr.GetLiabilityByProposalId(ProposalId);
            
            #endregion

            //教育规划中【每月定期投资金额】
            decimal disposableInput = 0;
            //教育规划中上学前总学费
            decimal sumTotalTuition = 0;
            //消费规划中【每月定期投资金额】
            decimal disposableInput2 = 0;
            //创业规划中【每月定期投资金额】）
            decimal disposableInput3 = 0;
            //现金规划中【现金保留规模】栏数值
            decimal retainCashMultiple = 0;
            //获得保险规划中【预算金额】  InsurancePlan
            decimal budgetAmount = 0;

            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);//这个如果获取不到建议书全部要报错
            //获取当前的insuranceplan
            InsurancePlanVM model = new InsurancePlanVM();
            if (proposal != null)
            {
           
                //先从缓存中获取保险规划
                InsurancePlanVM modelvm = proposal.InsurancePlanVM;
                if (modelvm != null)
                {
                    model = modelvm;
                }

                #region model赋值的问题
                //退休规划
                RetirementPlanVM RetirementPlanModel = new RetirementPlanVM();
                RetirementPlanVM RetirementPlanModelvm = proposal.RetirementPlanVM;
                if (RetirementPlanModelvm != null)
                {
                    RetirementPlanModel = RetirementPlanModelvm;
                }
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
                #endregion

                // 【自由储蓄】有数据，则自动生成
                #region 每月可支配资金  可用资产

                //当前的年龄应该来自客户信息表里面
                //TrainSearch ts = new TrainSearch
                //{
                //    ProposalId = ProposalId
                //};
            
          
                if (ProposalCustomerList != null )
                {
                    model.Age = ProposalCustomerList.Age;
                    model.InsureName = ProposalCustomerList.CustomerName;
                }

           
            
                //教育规划
                if (AccordingModel != null)
                {
                    disposableInput = AccordingModel.DisposableInput;

                    if (AccordingModel.LifeEducationPlanDetailList != null && AccordingModel.LifeEducationPlanDetailList.Count > 0)
                    {
                        foreach (var item in AccordingModel.LifeEducationPlanDetailList)
                        {
                            sumTotalTuition += item.TotalTuition;
                        }
                    }
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
                    model.MonthMoney = (incomeModel.FreeMoney / 12) - AccordingModel.MonthlyInvestment - ConsumptionPlan.MonthlyInvestment - SelectSUPGetObjModel.MonthlyInvestment - RetirementPlanModel.MonthlyInvestment;

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
            
              
                if (model != null)
                {
                    budgetAmount = model.BudgetAmount1 + model.BudgetAmount2;
                }
                //然后给可用资产赋值,先拿到净值资产合计
                if (Liabilityvm != null)
                {
                    model.TotalVal = Liability.TotalVal;
                    model.UserableAsset = Liability.TotalVal - retainCashMultiple - disposableInput - disposableInput2 - disposableInput3 - RetirementPlanModel.DisposableInput - budgetAmount;
                }
                else
                {
                    model.UserableAsset = 0;
                }

                #endregion

                #region 自由数据计算
                //	紧急备用金现值===数据来源现金规划保留规模
                //if (model.ReserveFund1 == 0)
                //{
                //    model.ReserveFund1 = CashPlanModel.RetainCashMultiple;
                //}
                //if (model.ReserveFund2 == 0)
                //{
                //    model.ReserveFund2 = CashPlanModel.RetainCashMultiple;
                //}


                ////	教育金现值===数据来源教育规划或输
                //if (model.EduAmount1 == 0)
                //{
                //    model.EduAmount1 = sumTotalTuition;
                //}
                //if (model.EduAmount2 == 0)
                //{
                //    model.EduAmount2 = sumTotalTuition;
                //}
                ////	养老基金现值===数据来源退休规划或输入TotalAmount
                //if (model.PensionFunds1 == 0)
                //{
                //    model.PensionFunds1 = RetirementPlanModel.TotalAmount;
                //}
                //if (model.PensionFunds2 == 0)
                //{
                //    model.PensionFunds2 = RetirementPlanModel.TotalAmount;
                //}
                //	遗属需求法应有的寿险保额=∑(家庭未来生活费用缺口现值,紧急备用金现值，教育金现值，养老基金现值，临终及丧葬支出现值，目前贷款余额)-家庭生息资


                #endregion
            }

            if (model != null)
            {
                if (model.MethodTypeId == 0)
                {
                    //第一次加载的时候
                    model.MethodTypeId = 1;
                }
                return Json(new JsonModel(true, "", model));
            }
            else
            {
                return Json(new JsonModel(true, "", null));//20008 加载失败!请联系管理员!
            }
        }

	}
}