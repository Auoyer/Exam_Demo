using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    public class IncomeAndExpensesController : Controller
    {
        //财务分析-----收支储蓄
        // GET: /CompetitionUser/IncomeAndExpenses/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveIncomeAndExpenses(IncomeAndExpensesVM model)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
            if (proposal != null)
            {
                if (model.Id > 0)
                {
                    #region 更新
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateTrainIncomeAndExpenses(model);
                    if (result)
                    {
                        proposal.IncomeAndExpensesVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007"));//20007 修改失败!请联系管理员!
                    }
                    #endregion
                }
                else
                {
                    #region 新增

                    int id = SvrFactory.Instance.TrainingSvr.AddTrainIncomeAndExpenses(model);
                    if (id != 0)
                    {
                        model.Id = id;
                        proposal.IncomeAndExpensesVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20006", null));//20006 新增失败!请联系管理员!
                    }
                    #endregion
                }
            }
            return Json(new JsonModel(true, ""));
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult LoadIncomeAndExpenses(int id)
        {
            IncomeAndExpensesVM model = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpenses(id);
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
        public ActionResult LoadIncomeAndExpensesByProposalId(int ProposalId)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            IncomeAndExpensesVM model = new IncomeAndExpensesVM();
            if (proposal != null)
            {
                model = proposal.IncomeAndExpensesVM;
            }
          //  IncomeAndExpensesVM model = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpensesByProposalId(ProposalId);
            if (model != null)
            {
                return Json(new JsonModel(true, "", model));
            }
            else
            {
                return Json(new JsonModel(true, "", null));//20008 加载失败!请联系管理员!
            }
        }

	}
}