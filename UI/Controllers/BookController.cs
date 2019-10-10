using LogicInfastructure.Interfaces;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Enums;

namespace UI.Controllers
{
    public class BookController : BaseController
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
        public ActionResult GetBooks(DataTableRequestVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                var tableVM = bookDM.Get(model);
                return new JsonResult
                {
                    Data = tableVM,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpGet]
        public ActionResult Get(long id = 0)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                var model = bookDM.Get(id);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Save(BookEditVM model)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                if (ModelState.IsValid)
                {
                    bookDM.Save(model);
                    return RedirectToAction("Index", new { alert = "Book was saved" });
                }

                ViewBag.Alert = new AlertVM { Message = "Failed to save book", Type = AlertType.Danger };
                var modelToRender = bookDM.Get(model);
                return View("~/Views/Book/Get.cshtml", modelToRender);
            }
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            using (var bookDM = Factory.GetService<IBookDM>())
            {
                bookDM.Delete(id);
                return new EmptyResult();
            }
        }
    }
}