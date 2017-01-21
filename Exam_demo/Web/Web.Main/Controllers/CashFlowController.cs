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
    public class CashFlowController : Controller
    {
        /// <summary>
        /// 实训考核-现金流量界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取现金流量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCashFlowList(int ProposalId)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            IncomeAndExpensesVM List = null;
            CashFlowVM List2 = new CashFlowVM();
            List2.JudgeVal = false;//加入一个判别
            if (proposal != null)
            {
                List = proposal.IncomeAndExpensesVM;
                List2 = proposal.CashFlowVM;
                if (List2 != null&&List!=null)
                {
                    List2.JudgeVal = true;
                }
             
            }

            return Json(new JsonModel(true, "", new { list = List, list2 = List2 }));
        }


        /// <summary>
        /// 新增/修改现金流量
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CashFlows(CashFlowVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                bool result = SvrFactory.Instance.TrainingSvr.UpdateCashFolw(model);

                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.CashFlowVM = model;
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
                int result = SvrFactory.Instance.TrainingSvr.AddCashFlow(model);

                if (result > 0)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = result;
                    proposal.CashFlowVM = model;

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