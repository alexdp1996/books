using Logic;
using System.Linq;
using System.Web.Mvc;
using ViewModels;

namespace UI.Controllers
{
    public class BookController : Controller
    {
        private BookDM BookDM { get; } = new BookDM();

        [HttpGet]
        public ActionResult Index(string alert = null)
        {
            ViewBag.Alert = alert;
            return View();
        }

        [HttpPost]
        public ActionResult GetBooks(DataTableVM model)
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
            ViewBag.Alert = "Failed to save book";
            var dm = new AuthorDM();
            model.Authors = dm.Get(model.AuthorIds).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            BookDM.Delete(id);
            return new EmptyResult();
        }
    }
}