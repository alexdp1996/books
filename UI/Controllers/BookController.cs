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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookVM model)
        {
            if (ModelState.IsValid)
            {
                BookDM.Add(model);
                return RedirectToAction("Index", new { alert = "Book was created" });
            }
            ViewBag.Alert = "Failed to create book";
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var model = BookDM.Get(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = BookDM.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookEditVM model)
        {
            if (ModelState.IsValid)
            {
                BookDM.Update(model);
                return RedirectToAction("Index", new { alert = "Book was edited" });
            }
            ViewBag.Alert = "Failed to edit book";
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