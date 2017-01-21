using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    //财产分配
    public class DistributionOfPropertyController : Controller
    {
        /// <summary>
        /// 实训考核-财产分配界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存财产分配数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDistributionOfProperty(DistributionOfPropertyVM model)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
            if (proposal != null)
            {
                if (model.Id > 0)
                {
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateDistributionOfProperty(model);
                    if (result)
                    {
                        proposal.DistributionOfPropertyVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, ""));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007", null));
                    }
                }
                else
                {
                    int id = SvrFactory.Instance.TrainingSvr.AddDistributionOfProperty(model);
                    if (id != 0)
                    {
                        model.Id = id;
                        proposal.DistributionOfPropertyVM = model;
                        TrainingCaches.SetProposalCache(proposal.Id, proposal);
                        return Json(new JsonModel(true, "", model));
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20006", null));
                    }
                }
            }
            return Json(new JsonModel(true, ""));
        }


        /// <summary>
        /// 获取财产分配---根据建议书Id
        /// </summary>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDistributionOfPropertyByProposalId(int proposalId)
        {
  
            #region 用建议书id去查
            // CashPlanVM model = SvrFactory.Instance.TrainingSvr.GetCashPlanByProposalId(proposalId);
            ProposalVM proposal = ProposalBLL.GetProposal(proposalId);
            //先要获取到客户名字和年龄
            DistributionOfPropertyVM model = new DistributionOfPropertyVM();
            if (proposal != null)
            {
                //姓名年龄 --客户信息
                ProposalCustomerVM CustomerModel = new ProposalCustomerVM();
                ProposalCustomerVM CustomerModelvm = proposal.ProposalCustomerVM;
                if (CustomerModelvm != null)
                {
                    CustomerModel = CustomerModelvm;
                }
                DistributionOfPropertyVM modelvm = proposal.DistributionOfPropertyVM;
                if (modelvm != null)
                {
                    model = modelvm;
                    model.CustomerName = CustomerModel.CustomerName;
                    model.CustomerAge = CustomerModel.Age;
                    return Json(new JsonModel(true, "", model));

                }
                else
                {
                    model.CustomerName = CustomerModel.CustomerName;
                    model.CustomerAge = CustomerModel.Age;
                    return Json(new JsonModel(true, "", model));
                }
             
            }
            
                return Json(new JsonModel(true, "", null));
           
           
            #endregion
        }




	}



     
}