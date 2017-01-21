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
    public class FinancialRatiosController : Controller
    {
        //
        // GET: /CompetitionUser/FinancialRatios/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取现金流量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFRList(int ProposalId)
        {          
                
             //CustomFilter search = new CustomFilter();
             //search.ProposalId = ProposalId;
             //var List = SvrFactory.Instance.TrainingSvr.GetLiabilityByProposalId(ProposalId);
            // var List2 = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpensesByProposalId(ProposalId);
             //var list3 = SvrFactory.Instance.TrainingSvr.SelectFinalcialRatiosGetObj(ProposalId);

            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);

             LiabilityVM List = null;
             IncomeAndExpensesVM List2 = null;
             FinancialRatiosVM list3 = null;
             if (proposal != null)
             {
                  List = proposal.LiabilityVM;
                  List2 = proposal.IncomeAndExpensesVM;
                  list3 = proposal.FinancialRatiosVM;
             }
             return Json(new JsonModel(true, "", new { list = List, list2 = List2, list3 = list3 }));           

        }


        /// <summary>
        /// 新增/修改财务现金流量
        /// </summary>
        /// <param name="model">案例实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFR(FinancialRatiosVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                bool result = SvrFactory.Instance.TrainingSvr.UpdateFinalcialRatios(model);
                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.FinancialRatiosVM = model;
                    TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    return Json(new JsonModel(true, "20010", null));//21016 修改成功!!
                }
                else
                {
                    return Json(new JsonModel(false, "20007", null));//20007 修改失败!请联系管理员!
                }
                #endregion
            }
            else
            {
                #region 新增
                int result = SvrFactory.Instance.TrainingSvr.AddFinalcialRatios(model);
                if (result > 0)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = result;
                    proposal.FinancialRatiosVM = model;
                    TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    return Json(new JsonModel(true, "20010", null));//20010 保存成功
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