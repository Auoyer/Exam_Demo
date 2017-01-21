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
    public class FundProductController : Controller
    {
        /// <summary>
        /// 理财产品-基金界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 理财产品-基金详细界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 获取基金数据
        /// </summary>
        /// <param name="FundType">基金类型</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFundProductList(int? FundSearchType, string keywords, int pageIndex, int pageSize)
        {
            List<string> FungTypeName = new List<string>();
            if (FundSearchType.HasValue)
            {
                switch (FundSearchType.Value)
                {
                    case (int)FundProductType.Currency:
                        FungTypeName.Add(EnumHelper.GetAllEnumDesc(FundProductType.Currency));
                        break;
                    case (int)FundProductType.Stock:
                        FungTypeName.Add(EnumHelper.GetAllEnumDesc(FundProductType.Stock));
                        break;
                    case (int)FundProductType.Bond:
                        FungTypeName.Add(EnumHelper.GetAllEnumDesc(FundProductType.Bond));
                        FungTypeName.Add(EnumHelper.GetAllEnumDesc(FundProductType.Mixture));
                        break;
                }
            }

            TrainSearch search = new TrainSearch
            {
                FundType = FungTypeName,
                KeyWords = keywords,
            };
            var page = SvrFactory.Instance.TrainingSvr.GetFundProductPage(search, pageIndex, pageSize);

            return Json(new JsonModel(true, "", page));

        }

        /// <summary>
        /// 获取基金详细信息
        /// </summary>
        /// <param name="Id">基金Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFundDetail(int Id)
        {
            var model = SvrFactory.Instance.TrainingSvr.GetFundProduct(Id);
            return Json(new JsonModel(true, "", model));
        }


    }
}