using System;
using System.Text;
using System.Web.Mvc;
using Utils;
using VM;

namespace Web
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var user = MvcHelper.User;

            StringBuilder strLog = new StringBuilder();
            if (user != null)
            {
                strLog.Append("当前登录用户ID：").AppendLine(user.Id.ToString());
            }
            strLog.Append("消息类型：").AppendLine(filterContext.Exception.GetType().Name);
            strLog.Append("消息内容：").AppendLine(filterContext.Exception.Message);
            strLog.Append("引发异常的方法：").AppendLine(filterContext.Exception.TargetSite.ToString());
            strLog.AppendLine("引发异常源：");
            strLog.AppendLine(filterContext.Exception.Source + filterContext.Exception.StackTrace);
            strLog.AppendLine("********************************************************************************************************************************************");

            //记录日志
            LogHelper.Log.WriteError(strLog.ToString());
            //如果是ajax请求
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new JsonModel(false, "21003", null, MvcHelper.LoginUrl),//网站出现异常，请联系管理员!
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                };
            }
            else
            {
                //抛出异常信息
                filterContext.Controller.TempData["ExceptionAttributeMessages"] = strLog.ToString();
                //转向
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}