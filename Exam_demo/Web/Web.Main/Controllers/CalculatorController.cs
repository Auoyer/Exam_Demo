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
    /// <summary>
    /// 计算器
    /// </summary>
    public class CalculatorController : Controller
    {
        //
        // GET: /Student/Calculator/
        public ActionResult Index()
        {
            return View();
        }
      
        /// <summary>
        /// FV,当最后一个参数为1时，即在间隔日期开始时
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="nper"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RetailLump(double rate,double nper,double amount)
        {
          
            var result = Microsoft.VisualBasic.Financial.FV(rate/12,nper,amount,0,Microsoft.VisualBasic.DueDate.BegOfPeriod);
            return Json(new JsonModel(true, "", result));
 
        }

        /// <summary>
        /// PMT,当最后一个参数为0时，即在间隔日期结束时
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="nper"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PMTEnd(double rate,double nper,double amount)
        {
            var result = Microsoft.VisualBasic.Financial.Pmt(rate, nper, amount, 0, Microsoft.VisualBasic.DueDate.EndOfPeriod);
            return Json(new JsonModel(true, "", result));
        }

        /// <summary>
        /// FV公共方法--计算未来之
        /// </summary>
        /// <param name="rate">每一期的利率</param>
        /// <param name="nper">所有的期数，一共存多少次</param>
        /// <param name="amount">每期存款的数字</param>
        /// <param name="pv">返回投资的现值</param>
        /// <param name="begOfPeriodType">输入1是指期初付款-指月初付款</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FVCommon(double rate, double nper, double amount, int? begOfPeriodType, double pv=0)
        {
            double result = 0;
            if (pv == 0 && begOfPeriodType == 0)
            {
                result = Microsoft.VisualBasic.Financial.FV(rate, nper, amount);
            }
            else
            {
                if (begOfPeriodType == 0)
                {
                    result = Microsoft.VisualBasic.Financial.FV(rate , nper, amount, pv, Microsoft.VisualBasic.DueDate.EndOfPeriod);
                }
                else
                {
                    result = Microsoft.VisualBasic.Financial.FV(rate, nper, amount, pv, Microsoft.VisualBasic.DueDate.BegOfPeriod);
                }
            }
            return Json(new JsonModel(true, "", result));

        }

        /// <summary>
        /// PV公共方法--返回投资的现值
        /// </summary>
        /// <param name="rate">每一期的利率</param>
        /// <param name="nper">所有的期数，一共存多少次</param>
        /// <param name="pmt">各期所应支付的金额</param>
        /// <param name="fv">未来值</param>
        /// <param name="begOfPeriodType">输入1是指期初付款-指月初付款</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PVCommon(double rate, double nper, double pmt,int? begOfPeriodType, double fv=0)
        {
            double result = 0;

            if (fv == 0 && begOfPeriodType == 0)
            {

                result = Microsoft.VisualBasic.Financial.PV(rate , nper, pmt);

            }
            else
            {
                if (begOfPeriodType == 0)
                {
                    result = Microsoft.VisualBasic.Financial.PV(rate , nper, pmt, fv, Microsoft.VisualBasic.DueDate.EndOfPeriod);
                }
                else
                {
                    result = Microsoft.VisualBasic.Financial.PV(rate , nper, pmt, fv, Microsoft.VisualBasic.DueDate.BegOfPeriod);
                }

            }

                return Json(new JsonModel(true, "", result));
        }


        /// <summary>
        /// PV公共方法--返回投资的现值
        /// </summary>
        /// <param name="rate">每一期的利率</param>
        /// <param name="nper">所有的期数，一共存多少次</param>
        /// <param name="pmt">各期所应支付的金额</param>
        [HttpPost]
        public ActionResult PVCommonSub(double rate, double nper, double pmt)
        {
            double result = 0;
            result = Microsoft.VisualBasic.Financial.PV(rate / 12, nper, pmt);
            return Json(new JsonModel(true, "", result)); ;
        }
	}
}