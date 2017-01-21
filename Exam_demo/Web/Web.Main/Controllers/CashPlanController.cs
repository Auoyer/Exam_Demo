using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    public class CashPlanController : Controller
    {
        /// <summary>
        /// 实训考核-现金规划界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 获取现金规划
        /// </summary>
        /// <param name="proposalId">建议书Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCashPlanByProposalId(int proposalId)
        {

            #region 用建议书id去查
            ProposalVM proposal = ProposalBLL.GetProposal(proposalId);
            CashPlanVM model = new CashPlanVM();
           
            if (proposal != null) 
            {
                //收支储蓄
                //IncomeAndExpensesVM incomeModel = new IncomeAndExpensesVM();
                //IncomeAndExpensesVM incomeModelvm = proposal.IncomeAndExpensesVM;
                //if (incomeModelvm != null)
                //{
                //    incomeModel = incomeModelvm;
                //}
                CashPlanVM modelvm = proposal.CashPlanVM;
                if (modelvm != null)
                {
                   // model = modelvm;
                    //if (incomeModelvm != null) //判断家月收入
                    //{
                    //    if (modelvm.FamilyMonthExpense == 0) //现金规划里面的家月收入
                    //    {
                    //        //当家月支出不为0的时候赋值
                    //        model.FamilyMonthExpense = incomeModel.FamilyExpense;
                    //    }
                    //}
                    return Json(new JsonModel(true, "", modelvm));

                }
                //else
                //{
                //    if (incomeModelvm != null)
                //    {
                //        model.FamilyMonthExpense = incomeModel.FamilyExpense;
                //    }
                //    return Json(new JsonModel(true, "", model));
                //}

            }
            return Json(new JsonModel(true, "",""));
            #endregion
        }
        /// <summary>
        /// 获取现金规划---根据Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCashPlan(int Id)
        {
            #region 用id去查

            var model = SvrFactory.Instance.TrainingSvr.GetCashPlan(Id);
            if (model == null)
            {
                //先要去收支储蓄表中拿数据
                IncomeAndExpensesVM incomeModel = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpenses(Id);
                //decimal reault = (incomeModel.FamilyExpense + incomeModel.ChildExpense + incomeModel.OtherExpense + incomeModel.InterestExpense + incomeModel.InsuranceExpense + incomeModel.OtherFinanceExpense) / 12;
                model = new CashPlanVM { FamilyMonthExpense = incomeModel.FamilyExpense };

            }
            return Json(new JsonModel(true, "", model));
            #endregion
        }

        /// <summary>
        ///保存现金规划
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCashPlanBy(CashPlanVM model)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
            if (proposal != null)
            {
                if (model.Id > 0)
                {
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateCashPlan(model);
                    if (result)
                    {
                        proposal.CashPlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007", null));
                    }
                }
                else
                {
                    int id = SvrFactory.Instance.TrainingSvr.AddCashPlan(model);
                    if (id != 0)
                    {
                        model.Id = id;
                        proposal.CashPlanVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20006", null));
                    }
                }
            }
            return Json(new JsonModel(true, ""));
            //var model = SvrFactory.Instance.TrainingSvr.GetCashPlanByProposalId(proposalId);
            //return Json(new JsonModel(model));
        }
	}
}