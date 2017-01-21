using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server.Factory;
using Utils;
using VM;

namespace Web.Controllers
{
    public class HeritageController : Controller
    {

        /// <summary>
        /// 财产传承界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取财产传承实体
        /// </summary>
        /// <param name="ProposalId">计划书Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetHeritage(int ProposalId)
        {
           // var model = SvrFactory.Instance.TrainingSvr.GetHeritage(ProposalId);
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);

            HeritageVM model = null;
            if (proposal != null)
            {
                 model = proposal.HeritageVM;
            }

            return Json(new JsonModel(true, "", model));
        }


        /// <summary>
        /// 新增/修改财产传承
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddHeritage(HeritageVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
               
                bool result = SvrFactory.Instance.TrainingSvr.UpdateHeritage(model);
               
                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.HeritageVM = model;
                    TrainingCaches.SetProposalCache(proposal.Id, proposal);
                    return Json(new JsonModel(true, "20010", null));//21016 保存成功!!
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
                int result = SvrFactory.Instance.TrainingSvr.AddHeritage(model);

                if (result > 0)
                {
                    HeritageVM c = SvrFactory.Instance.TrainingSvr.SelectHeritageGetObj(model.ProposalId);
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = c.Id;
                    proposal.HeritageVM = model;
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