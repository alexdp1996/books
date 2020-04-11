using Shared.Interfaces;
using System.Web.Mvc;
using Unity;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        protected IFactory Factory { get; }

        public BaseController()
        {
            Factory = new Factory();
        }
    }
}