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
    public class LifeEducationPlanController : Controller
    {
        //
        // GET: /CompetitionUser/LifeEducationPlan/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取教育规划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLifeEducationPlanList(int ProposalId)
        {
            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);
            LifeEducationPlanVM EPGList = null;
            if (proposal != null)
            {
                EPGList = proposal.LifeEducationPlanVM;
            }
            return Json(new JsonModel(true, "", new { list = EPGList }));
        }

        /// <summary>
        /// 获取多表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetmoneyList(int ProposalId)
        {
            ////获取收支存储表
            //var IncomeAndExpenses = SvrFactory.Instance.TrainingSvr.GetIncomeAndExpensesByProposalId(ProposalId);
            ////获取消费规划
            //var ConsumptionPlan = SvrFactory.Instance.TrainingSvr.AdoptProposalIdSelectCPlanGetObj(ProposalId);
            ////获取创业规划
            //var StartAnUndertakingPlan = SvrFactory.Instance.TrainingSvr.GetModelProposalId(ProposalId);
            ////获取退休规划
            //var RetirementPlan = SvrFactory.Instance.TrainingSvr.GetRetirementPlanByProposalId(ProposalId);
            ////资产负债表
            //var Liability = SvrFactory.Instance.TrainingSvr.GetLiabilityByProposalId(ProposalId);
            ////现金规划
            //var CashPlan = SvrFactory.Instance.TrainingSvr.GetCashPlanByProposalId(ProposalId);
            ////保险规划(待实现)
            //var InsurancePlan = SvrFactory.Instance.TrainingSvr.GetRetirementPlanByProposalId(ProposalId);
            ////教育规划
            //Object LifeEducationPlan = null;

            ProposalVM proposal = ProposalBLL.GetProposal(ProposalId);

            IncomeAndExpensesVM IncomeAndExpenses = null;//获取收支存储表
            ConsumptionPlanVM ConsumptionPlan = null;//获取收支存储表
            LifeEducationPlanVM LifeEducationPlan = null;//获取教育规划
            RetirementPlanVM RetirementPlan = null;//获取退休规划
            LiabilityVM Liability = null;//资产负债表
            CashPlanVM CashPlan = null;//现金规划
            InsurancePlanVM InsurancePlan = null;//保险规划(待实现)
            StartAnUndertakingPlanVM StartAnUndertakingPlan = null;//创业规划
            if (proposal != null)
            {
                IncomeAndExpenses = proposal.IncomeAndExpensesVM;//获取收支存储表
                ConsumptionPlan = proposal.ConsumptionPlanVM;//获取收支存储表
                LifeEducationPlan = proposal.LifeEducationPlanVM;//获取教育规划
                RetirementPlan = proposal.RetirementPlanVM;//获取退休规划
                Liability = proposal.LiabilityVM;//资产负债表
                CashPlan = proposal.CashPlanVM;//现金规划
                InsurancePlan = proposal.InsurancePlanVM;//保险规划(待实现)
                StartAnUndertakingPlan = proposal.StartAnUndertakingPlanVM;//创业规划
            }

            return Json(new JsonModel(true, "", new { IAE = IncomeAndExpenses, CP = ConsumptionPlan, SUP = StartAnUndertakingPlan, LEP = LifeEducationPlan, RP = RetirementPlan, L = Liability, CP2 = CashPlan, IPS = InsurancePlan }));
        }

        /// <summary>
        /// 新增/修改教育规划
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddLifeEducationPlans(LifeEducationPlanVM model)
        {
            if (model.Id > 0)
            {
                #region 更新
                //教育规划
                bool result = SvrFactory.Instance.TrainingSvr.UpdateEP(model);
                //教育规划详细信息
                if (result)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    proposal.LifeEducationPlanVM = model;
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
                int result = SvrFactory.Instance.TrainingSvr.AddEP(model);

                if (result > 0)
                {
                    ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
                    model.Id = result;
                    proposal.LifeEducationPlanVM = model;
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
        ///// 新增/修改教育规划详细信息
        ///// </summary>
        ///// <param name="model">实体</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult AddLifeEducationPlanDetail(LifeEducationPlanDetailVM model)
        //{
        //    if (model.Id > 0)
        //    {
        //        #region 更新
        //        //教育规划
        //        bool result = SvrFactory.Instance.TrainingSvr.UpdateEPD(model);
        //        //教育规划详细信息
        //        if (result)
        //        {
        //            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
        //            proposal.LifeEducationPlanDetailVM = model;
        //            return Json(new JsonModel(false, "21016", null));//21016 修改成功!!
        //        }
        //        else
        //        {
        //            return Json(new JsonModel(false, "20007", null));//20007 修改失败!请联系管理员!
        //        }
        //        #endregion
        //    }
        //    else
        //    {
        //        #region 新增
        //        int result = SvrFactory.Instance.TrainingSvr.AddEPD(model);
        //        if (result > 0)
        //        {
        //            LifeEducationPlanDetailVM c = SvrFactory.Instance.TrainingSvr.SelectEPDGetObj(model.ProposalId);
        //            ProposalVM proposal = ProposalBLL.GetProposal(model.ProposalId);
        //            model.Id = c.Id;
        //            proposal.LifeEducationPlanDetailVM = model;
        //            return Json(new JsonModel(false, "20010", null));//20010 保存成功!
        //        }
        //        else
        //        {
        //            return Json(new JsonModel(false, "21015", null));//21015 保存失败!请联系管理员!!
        //        }
        //        #endregion
        //    }
        //}

        ///// <summary>
        ///// 删除教育规划详细信息
        ///// </summary>
        ///// <param name="model">实体</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult DeleteLifeEducationPlanDetail(int Id)
        //{
        //    #region 更新
        //    //教育规划
        //    bool result = SvrFactory.Instance.TrainingSvr.DeleteEPD(Id);
        //    //教育规划详细信息
        //    if (result)
        //    {
        //        return Json(new JsonModel(true, ""));
        //    }
        //    else
        //    {
        //        return Json(new JsonModel(false, "20007", null));//20007 修改失败!请联系管理员!
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
                var result = Microsoft.VisualBasic.Financial.FV(rate, nper, pmt, pv, Microsoft.VisualBasic.DueDate.EndOfPeriod);
                return Json(new JsonModel(true, "", result));
            }
            else
            {
                var result = Microsoft.VisualBasic.Financial.FV(rate, nper, pmt, pv, Microsoft.VisualBasic.DueDate.BegOfPeriod);
                return Json(new JsonModel(true, "", result));
            }

        }
    }
}