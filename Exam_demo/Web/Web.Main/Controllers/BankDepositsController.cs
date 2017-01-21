using Server.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM;

namespace Web.Controllers
{
    public class BankDepositsController : Controller
    {

        /// <summary>
        /// 银行储蓄界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取银行储蓄
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBankDepositsList(string keywords, int pageIndex, int pageSize)
        {
            var list = TrainingCaches.BankDepositsList
                                     .Where(x => (string.IsNullOrEmpty(keywords) || x.BankName.IndexOf(keywords) > -1))
                                     .ToList();

            int total = list.Count;
            var rtnList = list.Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToList();
            PagedList<BankDepositsVM> page = new PagedList<BankDepositsVM>(rtnList, pageIndex, pageSize, total);

            return Json(new JsonModel(true, "", page));
        }
	}
}