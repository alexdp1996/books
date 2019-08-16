using Logic;
using System.Web.Mvc;
using ViewModels;

namespace UI.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorDM AuthorDM { get; } = new AuthorDM();

        [HttpGet]
        public ActionResult Index(string alert = null)
        {
            ViewBag.Alert = alert;
            return View();
        }

        [HttpPost]
        public ActionResult GetAuthors(DataTableVM model)
        {
            var tableVM = AuthorDM.Get(model);
            return new JsonResult
            {
                Data = tableVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult Get(long id = 0)
        {
            var model = AuthorDM.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(AuthorVM model)
        {
            if (ModelState.IsValid)
            {
                AuthorDM.Save(model);
                return RedirectToAction("Index", new { alert = "Author was saved" });
            }
            ViewBag.Alert = "Failed to save author";
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            AuthorDM.Delete(id);
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult GetByTerm(string term)
        {
            var res = AuthorDM.Get(term);
            return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}