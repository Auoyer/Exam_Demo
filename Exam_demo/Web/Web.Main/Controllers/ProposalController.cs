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
    public class ProposalController : Controller
    {
        /// <summary>
        /// 生成理财建议书操作
        /// </summary>
        /// <param name="ProposalId">建议书Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int ProposalId, int StuCustomerId)
        {
            ProposalVM ProposalCache = ProposalBLL.GetProposal(ProposalId);
            if (ProposalCache != null)
            {
                if (ProposalCache.IsCashPlan || ProposalCache.IsLife || ProposalCache.IsInvestmentPlan || ProposalCache.IsTaxPlan
                    || ProposalCache.IsDistributionOfProperty || ProposalCache.IsHeritage)
                {
                    ProposalCache.ProposalNum = NumHelper.Instance.GetNum(NumberType.Proposal);
                    ProposalCache.UpdateDate = DateTime.Now;

                    //更新数据库
                    bool result = SvrFactory.Instance.TrainingSvr.UpdateProposal(ProposalCache);
                    if (result)
                    {
                        if (StuCustomerId > 0)
                        {
                            //建议书生成成功后修改客户信息建议书数量
                            var bo = SvrFactory.Instance.TrainingSvr.UpdateCustomerProposalCount(StuCustomerId, MvcHelper.User.Id);
                            return Json(new JsonModel(true, ""));
                        }
                    }
                    else
                    {
                        return Json(new JsonModel(false, "20007"));//20007 修改失败!请联系管理员!
                    }
                }
                else
                {
                    return Json(new JsonModel(false, "20022"));//20015 您未做任何理财规划，无法生成理财建议书!
                }
            }

            return Json(new JsonModel(false, "20011"));//20011 请先保存客户信息!
        }

        /// <summary>
        /// 获取自主新增客户
        /// </summary>
        /// <param name="StuCustomerId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetStuCustomer(int StuCustomerId)
        {
            var model = SvrFactory.Instance.TrainingSvr.GetStuCustomer(StuCustomerId);
            return Json(new JsonModel(true, string.Empty, model));
        }


    }
}