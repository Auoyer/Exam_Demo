using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Server.Factory;
using Utils;
using VM;

namespace Web.Controllers
{
    public class StartAnUndertakingPlanController : Controller
    {
        //
        // GET: /CompetitionUser/StartAnUndertakingPlan/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取创业规划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSUPList(int ProposalId)
        {         

            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            StartAnUndertakingPlanVM List = null ;
            ProposalCustomerVM List2 = null;
            if (proposal != null)
            {
                 List = proposal.StartAnUndertakingPlanVM;
                 List2 = proposal.ProposalCustomerVM;
            }
            return Json(new JsonModel(true, "", new { list = List, list2 = List2 }));

        }

        /// <summary>
        /// 获取多表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetmoneyList2(int ProposalId)
        {
            if (ProposalId > 0)
            {

                ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);

                IncomeAndExpensesVM IncomeAndExpenses = null;//获取收支存储表
                ConsumptionPlanVM ConsumptionPlan = null;//获取收支存储表
                LifeEducationPlanVM LifeEducationPlan = null;//获取教育规划
                RetirementPlanVM RetirementPlan = null;//获取退休规划
                LiabilityVM Liability = null;//资产负债表
                CashPlanVM CashPlan = null;//现金规划
                InsurancePlanVM InsurancePlan = null;//保险规划(待实现)
                StartAnUndertakingPlanVM StartAnUndertakingPlan = null;//创业规划
                if (proposal != null)
                {
                    IncomeAndExpenses = proposal.IncomeAndExpensesVM;//获取收支存储表
                    ConsumptionPlan = proposal.ConsumptionPlanVM;//获取收支存储表
                    LifeEducationPlan = proposal.LifeEducationPlanVM;//获取教育规划
                    RetirementPlan = proposal.RetirementPlanVM;//获取退休规划
                    Liability = proposal.LiabilityVM;//资产负债表
                    CashPlan = proposal.CashPlanVM;//现金规划
                    InsurancePlan = proposal.InsurancePlanVM;//保险规划(待实现)
                    StartAnUndertakingPlan = proposal.StartAnUndertakingPlanVM;//创业规划
                }

                return Json(new JsonModel(true, "", new { IAE = IncomeAndExpenses, CP = ConsumptionPlan, LEP = LifeEducationPlan, SUP = StartAnUndertakingPlan, RP = RetirementPlan, L = Liability, CP2 = CashPlan, IPS = InsurancePlan }));

            }

            return Json(false);
        }

        /// <summary>
        /// 新增/修改创业规划
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddSUP(StartAnUndertakingPlanVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                bool result = SvrFactory.Instance.TrainingSvr.UpdateSUP(model);
                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.StartAnUndertakingPlanVM = model;
                    TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    return Json(new JsonModel(true, "20010", null));//21016 修改成功!!
                }
                else
                {
                    return Json(new JsonModel(false, "21015", null));//20007 修改失败!请联系管理员!
                }
                #endregion
            }
            else
            {
                #region 新增
                int result = SvrFactory.Instance.TrainingSvr.AddSUP(model);
                if (result > 0)
                {
                    StartAnUndertakingPlanVM c = SvrFactory.Instance.TrainingSvr.GetModelProposalId(model.ProposalId);
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = c.Id;
                    proposal.StartAnUndertakingPlanVM = model;
                    TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    return Json(new JsonModel(true, "20010", null));//20010 保存成功!
                }
                else
                {
                    return Json(new JsonModel(false, "21015", null));//21015 保存失败!请联系管理员!!
                }
                #endregion
            }
        }
	}
}