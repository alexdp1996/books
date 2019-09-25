using Logic;
using System.Linq;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Enums;

namespace UI.Controllers
{
    public class BookController : Controller
    {
        private BookDM BookDM { get; } = new BookDM();

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
            var tableVM = BookDM.Get(model);
            return new JsonResult
            {
                Data = tableVM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult Get(long id = 0)
        {
            var model = BookDM.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(BookEditVM model)
        {
            if (ModelState.IsValid)
            {
                BookDM.Save(model);
                return RedirectToAction("Index", new { alert = "Book was saved" });
            }

            ViewBag.Alert = new AlertVM { Message = "Failed to save book", Type = AlertType.Danger };
            var modelToRender = BookDM.Get(model);
            return View("~/Views/Book/Get.cshtml", modelToRender);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            BookDM.Delete(id);
            return new EmptyResult();
        }
    }
}