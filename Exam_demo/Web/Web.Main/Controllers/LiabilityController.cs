using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    public class LiabilityController : Controller
    {
        //
        // GET: /CompetitionUser/Liability/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveLiability(LiabilityVM model)
        {
          
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
            if (proposal != null)
            {
                if (model.Id > 0)
                {
                    #region 更新
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateTrainLiability(model);
                    if (result)
                    {
                        //更新缓存
                        proposal.LiabilityVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "20010", model));
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

                    int id = SvrFactory.Instance.TrainingSvr.AddTrainLiability(model);

                    if (id != 0)
                    {
                        model.Id = id;
                        //更新缓存
                        proposal.LiabilityVM = model;
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
            else
            {
                return Json(new JsonModel(true, ""));
            }
          
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult LoadLiability(int id)
        {
            LiabilityVM model = SvrFactory.Instance.TrainingSvr.GetLiability(id);
            if (model != null)
            {
                return Json(new JsonModel(true, "", model));
            }
            else
            {
                return Json(new JsonModel(false, "", null));
            }
        }

        /// <summary>
        /// 加载数据---根据建议书ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult LoadLiabilityByProposalId(int ProposalId)
        {
          //  LiabilityVM model = SvrFactory.Instance.TrainingSvr.GetLiabilityByProposalId(ProposalId);
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            LiabilityVM model = new LiabilityVM();
            if (proposal != null)
            {
                model = proposal.LiabilityVM;
            }


            if (model != null)
            {
                return Json(new JsonModel(true, "20010", model));
            }
            else
            {
                return Json(new JsonModel(true, "", null));
            }
        }

     
	}
}