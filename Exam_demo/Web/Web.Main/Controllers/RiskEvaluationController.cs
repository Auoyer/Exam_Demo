using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    public class RiskEvaluationController : Controller
    {
        //
        // GET: /CompetitionUser/RiskEvaluation/
        public ActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult GetRiskEvaluationInfo(int ProposalId)
        {
            RiskIndexVM model = null;
            ProposalVM Proposal = ProposalBLL.GetProposal(ProposalId);
            if (Proposal != null)
            {
                model = Proposal.RiskIndexVM;
            }
            return Json(new JsonModel(true, "", model));
        }
         
        public ActionResult SaveRiskIndex(RiskIndexVM Model)
        {
            Model.UpdateDate = System.DateTime.Now; 
            if (Model.Id > 0)
            {
                bool result = SvrFactory.Instance.TrainingSvr.UpdateRiskIndexInfo(Model); 
                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(Model.ProposalId);//获取不到证明程序有问题，正常情况更新时必然有缓存
                    if (proposal != null)
                    {
                        proposal.UpdateDate = Model.UpdateDate;
                        proposal.RiskIndexVM = Model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    }
                    return Json(new JsonModel(true,"",Model));
                }
                else
                {
                    return Json(new JsonModel(false, "20007", null)); 
                }
            }
            else
            {
                int i = SvrFactory.Instance.TrainingSvr.CreateRiskIndexInfo(Model);
                if (i>0)
                {
                    Model.Id = i;
                    ProposalVM proposal = ProposalBLL.GetProposal(Model.ProposalId);//获取不到证明程序有问题，正常情况更新时必然有缓存
                    if (proposal != null)
                    {
                        proposal.UpdateDate = Model.UpdateDate;
                        proposal.RiskIndexVM = Model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    }
                    return Json(new JsonModel(true,"",Model));
                }
                else
                {
                    return Json(new JsonModel(false, "20006", null));
                }
            }
        }

        /// <summary>
        /// 评测结果
        /// </summary>
        /// <returns></returns> 
        public ActionResult EvaluationResult()
        {
            return View();
        }
	}
}