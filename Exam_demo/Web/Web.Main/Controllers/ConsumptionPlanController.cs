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
    public class ConsumptionPlanController : Controller
    {
        /// <summary>
        /// 实训考核-消费规划界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取消费规划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetConsumptionPlanObj(int ProposalId)
        {
            if (ProposalId > 0)
            {               
                //消费规划
               // var EPGList = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectCPlanGetObj(ProposalId);
                ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);

                ConsumptionPlanVM EPGList = null;
                if(proposal!=null){
                     EPGList = proposal.ConsumptionPlanVM;
                }

                return Json(new JsonModel(true, "", new { list = EPGList }));
            }

            return Json(false);
        }


        /// <summary>
        /// 新增/修改消费规划
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddConsumptionPlan(ConsumptionPlanVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                
                bool result = SvrFactory.Instance.TrainingSvr.UpdateConsumptionPlan(model);
               
                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.ConsumptionPlanVM = model;
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
                int result = SvrFactory.Instance.TrainingSvr.AddConsumptionPlan(model);

                if (result > 0)
                {
                    ConsumptionPlanVM c = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectCPlanGetObj(model.ProposalId);
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = result;
                    proposal.ConsumptionPlanVM = model;
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

     
        ///// <summary>
        ///// 删除消费规划详细信息
        ///// </summary>
        ///// <param name="model">实体</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult DeleteConsumptionPlanDetail(int Id)
        //{
        //    #region 
            
        //    bool result = SvrFactory.Instance.TrainingSvr.DeleteEPD(Id);
           
        //    if (result)
        //    {
        //        return Json(new JsonModel(true, "",null));
        //    }
        //    else
        //    {
        //        return Json(new JsonModel(false, "20005", null));//20005 删除失败!请联系管理员!
        //    }
        //    #endregion
        //}

        /// <summary>
        /// FV,当最后一个参数为0时，即在间隔日期结束时
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="nper"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RetailLump(double rate, double nper, double pmt, double pv, double type)
        {
            if (type == 0)
            {
                var result = Microsoft.VisualBasic.Financial.Pmt(rate , nper, pmt, pv, Microsoft.VisualBasic.DueDate.EndOfPeriod);
                return Json(new JsonModel(true, "", result));
            }
            else
            {
                var result = Microsoft.VisualBasic.Financial.Pmt(rate, nper, pmt, pv, Microsoft.VisualBasic.DueDate.BegOfPeriod);
                return Json(new JsonModel(true, "", result));
            }

        }
	}
}