using LogicInfastructure.Interfaces;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Enums;

namespace UI.Controllers
{
    public class AuthorController : BaseController
    {

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
            using (var authorDM = Factory.GetService<IAuthorDM>())
            {
                var tableVM = authorDM.Get(model);
                return new JsonResult
                {
                    Data = tableVM,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpGet]
        public ActionResult Get(long? id)
        {
            using (var authorDM = Factory.GetService<IAuthorDM>())
            {
                var model = authorDM.Get(id);
                return View(model);
            }           
        }

        [HttpPost]
        public ActionResult Save(AuthorVM model)
        {
            if (ModelState.IsValid)
            {
                using (var authorDM = Factory.GetService<IAuthorDM>())
                {
                    authorDM.Save(model);
                    return RedirectToAction("Index", new { alert = "Author was saved" });
                }
            }

            ViewBag.Alert = new AlertVM { Message = "Failed to save author", Type = AlertType.Danger };
            return View("~/Views/Author/Get.cshtml",model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            using (var authorDM = Factory.GetService<IAuthorDM>())
            {
                authorDM.Delete(id);
                return new EmptyResult();
            }
        }

        [HttpGet]
        public ActionResult GetByTerm(string term)
        {
            using (var authorDM = Factory.GetService<IAuthorDM>())
            {
                var res = authorDM.Get(term);
                return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}