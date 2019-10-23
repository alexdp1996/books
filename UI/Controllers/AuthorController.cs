using LogicInfastructure.Interfaces;
using System;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Enums;

namespace UI.Controllers
{
    public class AuthorController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
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
                return PartialView("~/Views/Author/Get.cshtml", model);
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
                    var alert = new AlertVM
                    {
                        Message = "Author was saved",
                        Type = AlertType.Success
                    };
                    return new JsonResult { Data = alert, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            throw new ArgumentException("Model is not valid");
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            using (var authorDM = Factory.GetService<IAuthorDM>())
            {
                authorDM.Delete(id);
                var alert = new AlertVM
                {
                    Message = "Author with id " + id + " was deleted",
                    Type = AlertType.Success
                };
                return new JsonResult { Data = alert, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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