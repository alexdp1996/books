using Logic;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Enums;

namespace UI.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorDM AuthorDM { get; } = new AuthorDM();

        [HttpGet]
        public ActionResult Index(string alert = null)
        {
            if (alert != null)
            {
                ViewBag.Alert = new AlertVM { Message = alert, Type = AlertType.Success };
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetAuthors(DataTableRequestVM model)
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

            ViewBag.Alert = new AlertVM { Message = "Failed to save author", Type = AlertType.Danger };
            return View("~/Views/Author/Get.cshtml",model);
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