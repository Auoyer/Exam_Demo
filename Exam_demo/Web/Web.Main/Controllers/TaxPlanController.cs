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
    public class TaxPlanController : Controller
    {
        //
        // GET: /CompetitionUser/TaxPlan/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取税收筹划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTaxPlanObj(int ProposalId)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            TaxPlanVM EPGList = null;
            if (proposal != null)
            {
                EPGList = proposal.TaxPlanVM;
            }
            return Json(new JsonModel(true, "", new { list = EPGList }));
        }


        /// <summary>
        /// 新增/修改税收筹划
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddTaxPlan(TaxPlanVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                //税收筹划
                bool result = SvrFactory.Instance.TrainingSvr.UpdateTaxPlan(model);

                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.TaxPlanVM = model;
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
                int result = SvrFactory.Instance.TrainingSvr.AddTaxPlan(model);

                if (result > 0)
                {
                    TaxPlanVM c = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectTaxPlanGetObj(model.ProposalId);
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = c.Id;
                    proposal.TaxPlanVM = model;
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