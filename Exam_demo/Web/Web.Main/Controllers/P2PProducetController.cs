using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server.Factory;
using VM;

namespace Web.Controllers
{
    public class P2PProducetController : Controller
    {
        //
        // GET: /CompetitionUser/P2PProducet/
        public ActionResult Index()
        {
            //P2PHelper p2pHelper = new P2PHelper();
            //var result = p2pHelper.AddBlukP2PProduce();

            return View();
        }
        
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetP2PProduceList(string keywords, int pageIndex, int pageSize, int? FundType)
        {
            var list = TrainingCaches.CurP2PCache();
            if (!string.IsNullOrEmpty(keywords))
            {
                 list = list.Where(x => x.P2PName.Contains(keywords) || x.InvestmentField.Contains(keywords)).ToList();
            }
            int total = list.Count;
            var rtnList = list.Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToList();
            PagedList<P2PProductVM> result = new PagedList<P2PProductVM>(rtnList,pageIndex,pageSize,total);
            return Json(new JsonModel(true, "", result));

        }
        [HttpPost]
        public ActionResult GetP2PProduceListByData(string keywords, int pageIndex, int pageSize, int? FundType)
        {
            int totalCount =0;
            PagedList<P2PProductVM> list = SvrFactory.Instance.TrainingSvr.GetP2PProductList(keywords, pageIndex, pageSize, out totalCount);

            return Json(new JsonModel(true, "", list));
        }


        [HttpPost]
        public ActionResult TestDoGet()
        {
            return Json(new JsonModel(true, ""));
        }

    }
}