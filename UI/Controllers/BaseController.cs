using Shared.Interfaces;
using System.Web.Mvc;
using Unity;
using ViewModels;
using ViewModels.Enums;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        protected IFactory Factory { get; }

        public BaseController()
        {
            Factory = new Factory();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                var alert = new AlertVM
                {
                    Message = filterContext.Exception.Message,
                    Type = AlertType.Danger
                };
                filterContext.Result = new JsonResult { Data = alert, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}